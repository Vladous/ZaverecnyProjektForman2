using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ZaverecnyProjektForman2.Data;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Controllers
{
    /// <summary>
    /// Kontroler pro správu typů pojištění
    /// </summary>
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Inicializuje novou instanci <see cref="InsurancesController"/> třídy.
        /// </summary>
        /// <param name="context">Kontext databáze.</param>
        /// <param name="userManager">Správa uživatelů.</param>
        public InsurancesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Přidáno
        }
        /// <summary>
        /// Zobrazuje seznam typů pojištění.
        /// </summary>
        /// <param name="sortOrder">Parametr pro řazení.</param>
        /// <param name="typFilter">Filtr pro typ pojištění.</param>
        /// <param name="page">Číslo aktuální stránky.</param>
        /// <param name="pageSize">Velikost stránky.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: Insurances
        public async Task<IActionResult> Index(string sortOrder, string typFilter, int page = 1, int pageSize = 10)
        {
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["SurnameSortParm"] = sortOrder == "Surname" ? "surname_desc" : "Surname";
            ViewData["TypFilterApplied"] = !string.IsNullOrEmpty(typFilter);
            var insurances = from s in _context.Insurances
                             select s;
            switch (sortOrder)
            {
                case "SurnameAsc":
                    insurances = insurances.OrderBy(s => s.Type);
                    break;
                case "SurnameDesc":
                    insurances = insurances.OrderByDescending(s => s.Type);
                    break;
                default:
                    insurances = insurances.OrderBy(s => s.Id);
                    break;
            }

            if (!String.IsNullOrEmpty(typFilter))
            {
                insurances = insurances.Where(s => s.Type.Contains(typFilter));
            }

            // Získání počtu záznamů pro výpočet celkového počtu stránek
            var totalRecords = await insurances.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            // Stránkování dat
            var pagedInsurances = await insurances
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            // ViewModel pro View
            var viewModel = new InsurancesIndexViewModel
            {
                Insurances = pagedInsurances,
                SortOrder = sortOrder,
                TypFilter = typFilter,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize
            };
            return View(viewModel);
        }
        /// <summary>
        /// Zobrazuje detaily konkrétního typu pojištění.
        /// </summary>
        /// <param name="id">Identifikátor typu pojištění.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: Insurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insurance = await _context.Insurances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }
            return View(insurance);
        }
        /// <summary>
        /// Zobrazuje formulář pro vytvoření nového typu pojištění.
        /// </summary>
        /// <returns>Akce vrací <see cref="IActionResult"/>.</returns>
        // GET: Insurances/Create
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Zpracuje data formuláře pro vytvoření nového typu pojištění.
        /// </summary>
        /// <param name="insurance">Data typu pojištění.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                // Nastavení aktuálního data
                insurance.CreationDate = DateTime.Now;
                insurance.LastChange = DateTime.Now;
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
                    insurance.UserCreated = currentUser;
                    insurance.UserLastChanged = currentUser;
                }
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Typ pojištění úspěšně přidán"; // Uložení zprávy o úspěchu
                return RedirectToAction(nameof(Index));
            }
            return View(insurance);
        }
        /// <summary>
        /// Zobrazuje formulář pro úpravu typu pojištění.
        /// </summary>
        /// <param name="id">Identifikátor typu pojištění.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            return View(insurance);
        }
        /// <summary>
        /// Zpracuje data formuláře pro úpravu typu pojištění.
        /// </summary>
        /// <param name="id">Identifikátor typu pojištění.</param>
        /// <param name="insurance">Aktualizovaná data typu pojištění.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type, LastChange")] Insurance insurance)
        {
            if (id != insurance.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var insuranceInDb = await _context.Insurances.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
                    insurance.CreationDate = insuranceInDb.CreationDate; // Přiřazení původní hodnoty CreationDate
                    insurance.LastChange = DateTime.Now;
                    _context.Update(insurance);
                    TempData["SuccessMessage"] = "Typ pojištění úspěšně upraven"; // Uložení zprávy o úspěchu
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.Id))
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
            return View(insurance);
        }
        /// <summary>
        /// Zobrazuje potvrzení pro smazání typu pojištění.
        /// </summary>
        /// <param name="id">Identifikátor typu pojištění.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insurance = await _context.Insurances
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insurance == null)
            {
                return NotFound();
            }
            return View(insurance);
        }
        /// <summary>
        /// Zpracuje smazání typu pojištění.
        /// </summary>
        /// <param name="id">Identifikátor typu pojištění.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insurance = await _context.Insurances.FindAsync(id);
            if (insurance != null)
            {
                _context.Insurances.Remove(insurance);
            }
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Typ pojištění úspěšně odstraněn"; // Uložení zprávy o úspěchu
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Kontroluje, zda typ pojištění existuje.
        /// </summary>
        /// <param name="id">Identifikátor typu pojištění.</param>
        /// <returns>Pravda, pokud typ pojištění existuje; jinak nepravda.</returns>
        private bool InsuranceExists(int id)
        {
            return _context.Insurances.Any(e => e.Id == id);
        }
    }
}