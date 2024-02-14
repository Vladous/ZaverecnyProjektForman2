using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Controllers
{
    /// <summary>
    /// Kontroler pro správu účtů uživatelů.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        /// <summary>
        /// Inicializace nové instance kontroleru <see cref="AccountController"/>.
        /// </summary>
        /// <param name="userManager">Správce uživatelů.</param>
        /// <param name="signInManager">Správce přihlášení.</param>
        /// <param name="roleManager">Správce rolí.</param>
        public AccountController
        (
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        /// <summary>
        /// Přesměrování na lokální URL nebo na hlavní stránku, pokud URL není lokální.
        /// </summary>
        /// <param name="returnUrl">URL pro přesměrování.</param>
        /// <returns>Akce pro přesměrování.</returns>
        // Přsměrování při nepovoleném přístupu
        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        /// <summary>
        /// Zobrazí formulář pro přihlášení.
        /// </summary>
        /// <param name="returnUrl">URL pro návrat po přihlášení.</param>
        /// <returns>View pro přihlášení.</returns>
        //
        // Přihlášení uživatele
        //
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        /// <summary>
        /// Zpracuje přihlášení uživatele.
        /// </summary>
        /// <param name="model">Model přihlášení.</param>
        /// <param name="returnUrl">URL pro návrat po přihlášení.</param>
        /// <returns>Akce pro přesměrování nebo zobrazení chyby.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                    return RedirectToLocal(returnUrl);

                ModelState.AddModelError("Login error", "Neplatné přihlašovací údaje.");
                return View(model);
            }
            // Pokud byly odeslány neplatné údaje, vrátíme uživatele k přihlašovacímu formuláři
            return View(model);
        }
        /// <summary>
        /// Zobrazí formulář pro registraci uživatele.
        /// </summary>
        /// <param name="returnUrl">URL pro návrat po registraci.</param>
        /// <returns>View pro registraci.</returns>
        //
        // Registrace uživatele
        //
        [HttpGet]
        public async Task<ActionResult> Register(string? returnUrl = null)
        {
            // Získání seznamu všech rolí
            var user = await userManager.GetUserAsync(User);
            var userRoles = await userManager.GetRolesAsync(user);
            var userHighestRoleLevel = userRoles
                .Select(role => RoleExtensions.RoleHierarchy.ContainsKey(role) ? RoleExtensions.RoleHierarchy[role] : 0)
                .DefaultIfEmpty(0)
                .Max();
            var availableRoles = RoleExtensions.RoleHierarchy
                .Where(kvp => kvp.Value <= userHighestRoleLevel) // Filtrace rolí na stejné nebo nižší úrovni
                .Select(kvp => kvp.Key) // Výběr názvů rolí
                .ToList(); // Konverze výsledku na seznam
            var allRoles = roleManager.Roles.ToList();
            // Předání seznamu rolí do ViewBag (nebo ViewData) pro použití ve view
            ViewBag.Roles = new SelectList(availableRoles);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        /// <summary>
        /// Zpracuje registraci nového uživatele.
        /// </summary>
        /// <param name="model">Model registrace.</param>
        /// <param name="returnUrl">URL pro návrat po registraci.</param>
        /// <returns>Akce pro přesměrování nebo zobrazení chyby.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                // Přidání aktuálního data registrace
                user.RegistrationDate = DateTime.UtcNow;
                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleAssignResult = await userManager.AddToRoleAsync(user, model.Role);
                    if (roleAssignResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // Znovu načtení seznamu rolí pro ViewBag, pokud je ModelState neplatný nebo pokud registrace selhala
            var roles = await roleManager.Roles.ToListAsync();
            // Umožňuje zachovat vybranou roli při chybě
            ViewBag.Roles = new SelectList(roles, "Name", "Name", model.Role);
            return View(model);
        }
        /// <summary>
        /// Odhlásí uživatele.
        /// </summary>
        /// <returns>Akce pro přesměrování.</returns>
        //
        // Odhlášení uživatele
        //
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToLocal(null);
        }
        /// <summary>
        /// Odstraní účet uživatele.
        /// </summary>
        /// <param name="userId">ID uživatele k odstranění.</param>
        /// <returns>Akce pro přesměrování.</returns>
        //
        // Odstranění uživatele
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = UserRoles.Admin)] // Přístup pouze pro administrátory
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                if (User.Identity?.Name == user.UserName)
                {
                    await signInManager.SignOutAsync(); // Odhlášení uživatele
                }
                // Přesměrování na hlavní stránku
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Zpracování chyb
            }
            return View(); // nebo přesměrování
        }
        /// <summary>
        /// Zobrazí detaily uživatele.
        /// </summary>
        /// <returns>View s detaily uživatele.</returns>
        public async Task<IActionResult> UserDetails()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await userManager.GetRolesAsync(user);
            var currentUserRole = roles.FirstOrDefault(); // Předpokládáme, že uživatel má jednu roli
            var model = new DetailUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = currentUserRole,
                RegistrationDate = user.RegistrationDate
            };
            return View(model);
        }
        /// <summary>
        /// Zobrazí formulář pro editaci uživatele.
        /// </summary>
        /// <param name="userId">ID uživatele pro editaci.</param>
        /// <returns>View s formulářem pro editaci.</returns>
        [HttpGet]
        public async Task<IActionResult> UserEdit(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return View("Error");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            var allRoles = roleManager.Roles.ToList();
            ViewBag.Roles = new SelectList(allRoles, "Name", "Name");
            var userRoles = await userManager.GetRolesAsync(user);
            var currentUserRole = userRoles.FirstOrDefault(); // Předpokládáme, že uživatel má jednu roli
            var model = new EditUserViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                // Nezobrazujeme heslo, pouze pole pro jeho změnu
                Role = currentUserRole
            };
            return View(model);
        }
        /// <summary>
        /// Zpracuje editaci uživatele.
        /// </summary>
        /// <param name="model">Model s údaji pro editaci.</param>
        /// <returns>Akce pro přesměrování nebo zobrazení chyby.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var allRoles = await roleManager.Roles.ToListAsync();
                ViewBag.Roles = new SelectList(allRoles, "Name", "Name");
                return View(model);
            }
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return View("Error");
            }
            // Aktualizace emailu nebo jiných informací
            user.Email = model.Email;
            // Další úpravy user objektu...
            var updateResult = await userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            // Změna hesla, pokud bylo zadáno nové a aktuální heslo je správně zadáno
            if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.Password))
            {
                // Ověření aktuálního hesla
                var passwordCheck = await userManager.CheckPasswordAsync(user, model.Password);
                if (passwordCheck)
                {
                    var passwordChangeResult = await userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                    if (!passwordChangeResult.Succeeded)
                    {
                        foreach (var error in passwordChangeResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Aktuální heslo je nesprávné.");
                    return View(model);
                }
            }
            // Nezapomeňte znovu nastavit ViewBag.Roles i v případě, že vše proběhne úspěšně a před přesměrováním
            var roles = await roleManager.Roles.ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            // Úspěšná aktualizace
            return RedirectToAction("Index", "Home"); // nebo jiný vhodný redirect
        }
    }
}