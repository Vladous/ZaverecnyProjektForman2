﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ZaverecnyProjektForman2.Data;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Controllers
{
    /// <summary>
    /// Kontroler pro správu pojistných smluv.
    /// </summary>
    public class InsuranceContractsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Inicializuje novou instanci <see cref="InsuranceContractsController"/> třídy.
        /// </summary>
        /// <param name="context">Kontext databáze.</param>
        /// <param name="userManager">Správa uživatelů.</param>
        public InsuranceContractsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Přidáno
        }
        /// <summary>
        /// Zobrazuje seznam pojistných smluv.
        /// </summary>
        /// <param name="sortOrder">Parametr pro řazení.</param>
        /// <param name="surnameFilter">Filtr pro příjmení pojistníka.</param>
        /// <param name="typFilter">Filtr pro typ pojištění.</param>
        /// <param name="subjectFilter">Filtr pro předmět pojištění.</param>
        /// <param name="page">Číslo aktuální stránky.</param>
        /// <param name="pageSize">Velikost stránky.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceContracts
        public async Task<IActionResult> Index(string sortOrder, string surnameFilter, string typFilter, string subjectFilter, int page = 1, int pageSize = 10)
        {
            ViewData["SurnameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "surnname_desc" : "";
            ViewData["InsurenceSortParm"] = sortOrder == "Insurence" ? "insurence_desc" : "Insurence";
            ViewData["SubjectSortParm"] = sortOrder == "Subject" ? "subject_desc" : "Subject";
            ViewData["AmountSortParm"] = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewData["FromSortParm"] = sortOrder == "From" ? "from_desc" : "From";
            ViewData["UntilSortParm"] = sortOrder == "Until" ? "until_desc" : "Until";
            ViewData["SurnameFilterApplied"] = !string.IsNullOrEmpty(surnameFilter);
            ViewData["TypFilterApplied"] = !string.IsNullOrEmpty(typFilter);
            ViewData["SubjectFilterApplied"] = !string.IsNullOrEmpty(subjectFilter);
            var contracts = from s in _context.InsuranceContracts
                           .Include(i => i.Insured)
                           .Include(i => i.Insurance)
                           .Include(i => i.InsuranceEvents)
                           select s;
            switch (sortOrder)
            {
                case "SurnameAsc":
                    contracts = contracts.OrderBy(s => s.Insured.Surname);
                    break;
                case "SurnameDesc":
                    contracts = contracts.OrderByDescending(s => s.Insured.Surname);
                    break;
                case "InsurenceAsc":
                    contracts = contracts.OrderBy(s => s.Insurance.Type);
                    break;
                case "InsurenceDesc":
                    contracts = contracts.OrderByDescending(s => s.Insurance.Type);
                    break;
                case "SubjectAsc":
                    contracts = contracts.OrderBy(s => s.NameSubject);
                    break;
                case "SubjectDesc":
                    contracts = contracts.OrderByDescending(s => s.NameSubject);
                    break;
                case "AmountAsc":
                    contracts = contracts.OrderBy(s => s.Amount);
                    break;
                case "AmountDesc":
                    contracts = contracts.OrderByDescending(s => s.Amount);
                    break;
                case "FromAsc":
                    contracts = contracts.OrderBy(s => s.InsuredFrom);
                    break;
                case "FromDesc":
                    contracts = contracts.OrderByDescending(s => s.InsuredFrom);
                    break;
                case "UntilAsc":
                    contracts = contracts.OrderBy(s => s.InsuredUntil);
                    break;
                case "UntilDesc":
                    contracts = contracts.OrderByDescending(s => s.InsuredUntil);
                    break;
                default:
                    contracts = contracts.OrderBy(s => s.Id);
                    break;
            }
            if (!String.IsNullOrEmpty(surnameFilter))
            {
                contracts = contracts.Where(s => s.Insured.Surname.Contains(surnameFilter));
            }
            if (!String.IsNullOrEmpty(typFilter))
            {
                contracts = contracts.Where(s => s.Insurance.Type.Contains(typFilter));
            }
            if (!String.IsNullOrEmpty(subjectFilter))
            {
                contracts = contracts.Where(s => s.NameSubject.Contains(subjectFilter));
            }
            // Počet záznamů pro stránkování
            var totalRecords = await contracts.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            // Data pro aktuální stránku
            var pagedContracts = await contracts.Skip((page - 1) * pageSize)
                                                     .Take(pageSize)
                                                     .AsNoTracking()
                                                     .ToListAsync();
            var viewModel = new InsuranceContractsIndexViewModel
            {
                Contracts = pagedContracts,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                SortOrder = sortOrder,
                SurnameFilter = surnameFilter,
                TypFilter = typFilter,
                SubjectFilter = subjectFilter
            };
            return View(viewModel);
        }
        /// <summary>
        /// Zobrazuje detaily konkrétní pojistné smlouvy.
        /// </summary>
        /// <param name="id">Identifikátor pojistné smlouvy.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceContracts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insuranceContracts = await _context.InsuranceContracts
                .Include(i => i.Insurance)
                .Include(i => i.Insured)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceContracts == null)
            {
                return NotFound();
            }
            return View(insuranceContracts);
        }
        /// <summary>
        /// Zobrazuje formulář pro vytvoření nové pojistné smlouvy.
        /// </summary>
        /// <param name="insuredId">Volitelný identifikátor pojistníka pro předvyplnění.</param>
        /// <returns>Akce vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceContracts/Create
        public async Task<IActionResult> Create(int? insuredId)
        {
            var insurances = _context.Insurances.Select(i => new
            {
                Id = i.Id,
                Type = $"{i.Type}"
            }).ToList();
            ViewBag.InsuranceId = new SelectList(insurances, "Id", "Type");
            if (insuredId.HasValue)
            {
                var insured = await _context.Insureds.FindAsync(insuredId.Value);
                if (insured == null)
                {
                    // Pojistník nenalezen
                    return NotFound();
                }
                ViewBag.InsuredNameSurname = $"{insured.Name} {insured.Surname}";
                ViewBag.InsuredId = new SelectList(new[] { insured }.Select(i => new
                {
                    Id = i.Id,
                    NameSurname = $"{i.Name} {i.Surname}"
                }), "Id", "NameSurname", insured.Id);
            }
            else
            {
                ViewBag.InsuredId = new SelectList(_context.Insureds.Select(i => new
                {
                    Id = i.Id,
                    NameSurname = $"{i.Name} {i.Surname}"
                }), "Id", "NameSurname");
            }
            return View();
        }
        /// <summary>
        /// Zpracuje data formuláře pro vytvoření nové pojistné smlouvy.
        /// </summary>
        /// <param name="insuranceContracts">Data pojistné smlouvy.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: InsuranceContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InsuredId,InsuranceId,NameSubject,Amount,InsuredFrom,InsuredUntil,CreationDate,LastChange")] InsuranceContracts insuranceContracts)
        {
            if (ModelState.IsValid)
            {
                insuranceContracts.CreationDate = DateTime.Now;
                insuranceContracts.LastChange = DateTime.Now;
                _context.Add(insuranceContracts);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pojistná smlouva byla úspěšně vytvořena"; // Uložení zprávy o úspěchu
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id", insuranceContracts.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "Id", insuranceContracts.InsuredId);
            return View(insuranceContracts);
        }
        /// <summary>
        /// Zobrazuje formulář pro úpravu pojistné smlouvy.
        /// </summary>
        /// <param name="id">Identifikátor pojistné smlouvy.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceContracts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insuranceContracts = await _context.InsuranceContracts.FindAsync(id);
            if (insuranceContracts == null)
            {
                return NotFound();
            }
            var insureds = _context.Insureds.Select(i => new
            {
                Id = i.Id,
                NameSurname = $"{i.Name} {i.Surname}"
            }).ToList();
            var insurances = _context.Insurances.Select(i => new
            {
                Id = i.Id,
                InsuranceType = $"{i.Type}" // Předpokládám, že máte vlastnosti Type a NameSubject ve vaší třídě Insurance
            }).ToList();
            ViewData["InsuranceId"] = new SelectList(insurances, "Id", "InsuranceType", insuranceContracts.InsuranceId);
            ViewData["InsuredId"] = new SelectList(insureds, "Id", "NameSurname", insuranceContracts.InsuredId);
            return View(insuranceContracts);
        }
        /// <summary>
        /// Zpracuje data formuláře pro úpravu pojistné smlouvy.
        /// </summary>
        /// <param name="id">Identifikátor pojistné smlouvy.</param>
        /// <param name="insuranceContracts">Aktualizovaná data pojistné smlouvy.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: InsuranceContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InsuredId,InsuranceId,NameSubject,Amount,InsuredFrom,InsuredUntil,LastChange")] InsuranceContracts insuranceContracts)
        {
            if (id != insuranceContracts.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var insuranceContractsInDb = await _context.InsuranceContracts.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
                    insuranceContracts.CreationDate = insuranceContractsInDb.CreationDate; // Přiřazení původní hodnoty CreationDate
                    insuranceContracts.LastChange = DateTime.Now;
                    _context.Update(insuranceContracts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceContractsExists(insuranceContracts.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["SuccessMessage"] = "Pojistná smlouva byla úspěšně upravena"; // Uložení zprávy o úspěchu
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id", insuranceContracts.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "Id", insuranceContracts.InsuredId);
            return View(insuranceContracts);
        }
        /// <summary>
        /// Zobrazuje potvrzení smazání pojistné smlouvy.
        /// </summary>
        /// <param name="id">Identifikátor pojistné smlouvy.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceContracts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insuranceContracts = await _context.InsuranceContracts
                .Include(i => i.Insurance)
                .Include(i => i.Insured)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceContracts == null)
            {
                return NotFound();
            }
            return View(insuranceContracts);
        }
        /// <summary>
        /// Zpracuje smazání pojistné smlouvy.
        /// </summary>
        /// <param name="id">Identifikátor pojistné smlouvy.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: InsuranceContracts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceContracts = await _context.InsuranceContracts.FindAsync(id);
            if (insuranceContracts != null)
            {
                _context.InsuranceContracts.Remove(insuranceContracts);
            }
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Pojistná smlouva byla úspěšně odstraněna"; // Uložení zprávy o úspěchu
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Kontroluje, zda pojistná smlouva existuje.
        /// </summary>
        /// <param name="id">Identifikátor pojistné smlouvy.</param>
        /// <returns>Pravda, pokud pojistná smlouva existuje; jinak nepravda.</returns>
        private bool InsuranceContractsExists(int id)
        {
            return _context.InsuranceContracts.Any(e => e.Id == id);
        }
    }
}
