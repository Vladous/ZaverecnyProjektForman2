using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AccountController
        (
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager
        )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }
        // Přsměrování při nepovoleném přístupu
        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
        }
        //
        // Přihlášení uživatele
        //
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
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
        //
        // Registrace uživatele
        //
        public async Task<ActionResult> Register(string? returnUrl = null)
        {
            // Získání seznamu rolí - příklad
            var user = await userManager.GetUserAsync(User);
            var roles = new Dictionary<string, int> { { "admin", 3 }, { "manager", 2 }, { "viewer", 1 } };

            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var currentUserRole = userRoles.FirstOrDefault(); // Předpokládáme, že uživatel má jednu roli

                if (!string.IsNullOrEmpty(currentUserRole) && roles.ContainsKey(currentUserRole))
                {
                    var availableRoles = roles
                        .Where(r => r.Value <= roles[currentUserRole])
                        .Select(r => r.Key)
                        .ToList();

                    ViewBag.Roles = availableRoles;
                }
                else
                {
                    // Uživatel nemá roli nebo roli, která není v seznamu
                    ViewBag.Roles = new List<string>();
                }
            }
            else
            {
                // Uživatel není přihlášen, zobrazit všechny role nebo žádné
                ViewBag.Roles = roles.Keys.ToList(); // nebo new List<string>() pro žádné role
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                //IdentityUser user = new IdentityUser { UserName = model.Email, Email = model.Email };
                ApplicationUser user = new ApplicationUser { UserName = model.UserName, Email = model.Email };
                // Přidání aktuálního data registrace
                user.RegistrationDate = DateTime.UtcNow;
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Přidělení role
                    var roleAssignResult = await userManager.AddToRoleAsync(user, model.Role);
                    if (roleAssignResult.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToLocal(returnUrl);
                    }
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }

            return View(model);
        }
        //
        // Odhlášení uživatele
        //
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToLocal(null);
        }
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
                Role = currentUserRole
            };

            return View(model);
        }

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
