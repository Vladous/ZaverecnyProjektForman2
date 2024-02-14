using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZaverecnyProjektForman2.Data;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Controllers
{
    /// <summary>
    /// Kontroler pro správu pojištěnců.
    /// </summary>
    public class InsuredsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Inicializace nové instance <see cref="InsuredsController"/> třídy.
        /// </summary>
        /// <param name="context">Kontext databáze.</param>
        /// <param name="userManager">Správa uživatelů.</param>
        public InsuredsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Přidáno
        }
        /// <summary>
        /// Zobrazí seznam pojištěnců.
        /// </summary>
        /// <param name="sortOrder">Parametr řazení.</param>
        /// <param name="nameFilter">Filtr jména.</param>
        /// <param name="surnameFilter">Filtr příjmení.</param>
        /// <param name="page">Číslo aktuální stránky.</param>
        /// <param name="pageSize">Velikost stránky.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: Insureds
        public async Task<IActionResult> Index(string sortOrder, string nameFilter, string surnameFilter, int page = 1, int pageSize = 10)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["SurnameSortParm"] = sortOrder == "Surname" ? "surname_desc" : "Surname";
            ViewData["BirthSortParm"] = sortOrder == "Birth" ? "birth_desc" : "Birth";
            ViewData["ContractsSortParm"] = sortOrder == "Contracts" ? "contracts_desc" : "Contracts";
            ViewData["NameFilterApplied"] = !string.IsNullOrEmpty(nameFilter);
            ViewData["SurnameFilterApplied"] = !string.IsNullOrEmpty(surnameFilter);
            var insureds = from s in _context.Insureds
                           .Include(i => i.InsuranceContracts)
                           .Include(i => i.InsuranceEvents)
                           select s;

            switch (sortOrder)
            {
                case "NameDesc":
                    insureds = insureds.OrderByDescending(s => s.Name);
                    break;
                case "SurnameAsc":
                    insureds = insureds.OrderBy(s => s.Surname);
                    break;
                case "SurnameDesc":
                    insureds = insureds.OrderByDescending(s => s.Surname);
                    break;
                case "BirthAsc":
                    insureds = insureds.OrderBy(s => s.Birth);
                    break;
                case "BirthDesc":
                    insureds = insureds.OrderByDescending(s => s.Birth);
                    break;
                case "ContractsAsc":
                    insureds = insureds.OrderBy(s => s.InsuranceContracts.Count);
                    break;
                case "ContractsAscDesc":
                    insureds = insureds.OrderByDescending(s => s.InsuranceContracts.Count);
                    break;
                case "EventsAsc":
                    insureds = insureds.OrderBy(s => s.InsuranceEvents.Count);
                    break;
                case "EventsAscDesc":
                    insureds = insureds.OrderByDescending(s => s.InsuranceEvents.Count);
                    break;
                default:
                    insureds = insureds.OrderBy(s => s.Name);
                    break;
            }

            if (!String.IsNullOrEmpty(nameFilter))
            {
                insureds = insureds.Where(s => s.Name.Contains(nameFilter));
            }
            if (!String.IsNullOrEmpty(surnameFilter))
            {
                insureds = insureds.Where(s => s.Surname.Contains(surnameFilter));
            }

            // Počítání celkového počtu záznamů po aplikaci filtrů
            var count = await insureds.CountAsync();

            // Výpočet celkového počtu stránek
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Získání stránkových dat
            var items = await insureds
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            // Předání dat do view
            var viewModel = new InsuredsIndexViewModel
            {
                Items = items,
                SortOrder = sortOrder,
                NameFilter = nameFilter,
                SurnameFilter = surnameFilter,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };



            return View(viewModel);
        }
        /// <summary>
        /// Zobrazí detaily konkrétního pojištěnce.
        /// </summary>
        /// <param name="id">Identifikátor pojištěnce.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: Insureds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await _context.Insureds
            .Include(i => i.InsuranceContracts)
            .ThenInclude(ic => ic.Insurance) // Přidáno pro zahrnutí detailů o pojištění
            .Include(i => i.InsuranceEvents)
            .ThenInclude(ic => ic.Insurance) // Přidáno pro zahrnutí detailů o pojistných událostech
            //.Include(i => i.UserCreated) // Přidáno pro zahrnutí informací o uživateli, který vytvořil záznam
            //.Include(i => i.UserLastChanged) // Přidáno pro zahrnutí informací o uživateli, který naposledy změnil záznam
            .FirstOrDefaultAsync(m => m.Id == id);


            if (insured == null)
            {
                return NotFound();
            }

            return View(insured);
        }
        /// <summary>
        /// Zobrazí formulář pro vytvoření nového pojištěnce.
        /// </summary>
        /// <returns>Akce vrací <see cref="IActionResult"/>.</returns>
        // GET: Insureds/Create
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Zpracuje data formuláře pro vytvoření nového pojištěnce.
        /// </summary>
        /// <param name="insured">Data pojištěnce.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: Insureds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Birth,Email,Street,HouseNumber,City,PostNumber,Phone,CreationDate,LastChange")] Insured insured)
        {
            if (ModelState.IsValid)
            {
                // Nastavení aktuálního data
                insured.CreationDate = DateTime.Now;
                insured.LastChange = DateTime.Now;
                // Získání aktuálně přihlášeného uživatele
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var currentUser = new UserInfo
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        RegistrationDate = user.RegistrationDate
                    };

                    insured.UserCreated = currentUser;
                    insured.UserLastChanged = currentUser;
                }

                _context.Add(insured);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insured);
        }
        /// <summary>
        /// Zobrazí formulář pro úpravu pojištěnce.
        /// </summary>
        /// <param name="id">Identifikátor pojištěnce.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: Insureds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await _context.Insureds.FindAsync(id);
            if (insured == null)
            {
                return NotFound();
            }
            return View(insured);
        }
        /// <summary>
        /// Zpracuje data formuláře pro úpravu pojištěnce.
        /// </summary>
        /// <param name="id">Identifikátor pojištěnce.</param>
        /// <param name="insured">Aktualizovaná data pojištěnce.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: Insureds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Birth,Email,Street,HouseNumber,City,PostNumber,Phone,CreationDate,LastChange")] Insured insured)
        {
            if (id != insured.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                // Nastavení aktuálního data
                insured.LastChange = DateTime.Now;
                // Získání aktuálně přihlášeného uživatele
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var currentUser = new UserInfo
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        RegistrationDate = user.RegistrationDate
                    };





                    insured.UserLastChanged = currentUser;
                }
                try
                {
                    _context.Update(insured);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuredExists(insured.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(insured);
        }
        /// <summary>
        /// Zobrazí potvrzení pro smazání pojištěnce.
        /// </summary>
        /// <param name="id">Identifikátor pojištěnce.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: Insureds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insured = await _context.Insureds
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insured == null)
            {
                return NotFound();
            }

            return View(insured);
        }
        /// <summary>
        /// Zpracuje smazání pojištěnce.
        /// </summary>
        /// <param name="id">Identifikátor pojištěnce.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: Insureds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insured = await _context.Insureds.FindAsync(id);
            if (insured != null)
            {
                _context.Insureds.Remove(insured);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Kontroluje, zda pojištěnec existuje.
        /// </summary>
        /// <param name="id">Identifikátor pojištěnce.</param>
        /// <returns>Pravda, pokud pojištěnec existuje; jinak nepravda.</returns>
        private bool InsuredExists(int id)
        {
            return _context.Insureds.Any(e => e.Id == id);
        }
    }
}
