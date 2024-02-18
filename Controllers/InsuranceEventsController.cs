using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// Kontroler pro správu pojistných událostí.
    /// </summary>
    public class InsuranceEventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        /// <summary>
        /// Inicializuje novou instanci <see cref="InsuranceEventsController"/> třídy.
        /// </summary>
        /// <param name="context">Kontext databáze.</param>
        /// <param name="userManager">Správa uživatelů.</param>
        public InsuranceEventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Přidáno
        }
        /// <summary>
        /// Zobrazuje seznam pojistných událostí.
        /// </summary>
        /// <param name="sortOrder">Parametr pro řazení.</param>
        /// <param name="surnameFilter">Filtr pro příjmení pojistníka.</param>
        /// <param name="typFilter">Filtr pro typ pojištění.</param>
        /// <param name="detailFilter">Filtr pro detail události.</param>
        /// <param name="page">Číslo aktuální stránky.</param>
        /// <param name="pageSize">Velikost stránky.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceEvents
        public async Task<IActionResult> Index(string sortOrder, string surnameFilter, string typFilter, string detailFilter, int page = 1, int pageSize = 10)
        {
            ViewData["IdSortParm"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["SurnameSortParm"] = sortOrder == "Surname" ? "surname_desc" : "Surname";
            ViewData["InsuranceSortParm"] = sortOrder == "Insurance" ? "insurance_desc" : "Insurance";
            ViewData["DetailSortParm"] = sortOrder == "Detail" ? "detail_desc" : "Detail";
            ViewData["AmountSortParm"] = sortOrder == "Amount" ? "amount_desc" : "Amount";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["SurnameFilterApplied"] = !string.IsNullOrEmpty(surnameFilter);
            ViewData["TypFilterApplied"] = !string.IsNullOrEmpty(typFilter);
            ViewData["DetailFilterApplied"] = !string.IsNullOrEmpty(detailFilter);
            var events = from s in _context.InsuranceEvents
                           .Include(i => i.InsuranceContracts)
                           .ThenInclude(ic => ic.Insured)
                           .Include(ie => ie.InsuranceContracts)
                           .ThenInclude(ic => ic.Insurance)
                         select s;
            switch (sortOrder)
            {
                case "SurnameAsc":
                    events = events.OrderBy(s => s.InsuranceContracts.Insured.Surname);
                    break;
                case "SurnameDesc":
                    events = events.OrderByDescending(s => s.InsuranceContracts.Insured.Surname);
                    break;
                case "InsuranceAsc":
                    events = events.OrderBy(s => s.InsuranceContracts.Insurance.Type);
                    break;
                case "InsuranceDesc":
                    events = events.OrderByDescending(s => s.InsuranceContracts.Insurance.Type);
                    break;
                case "DetailAsc":
                    events = events.OrderBy(s => s.EventDetail);
                    break;
                case "DetailDesc":
                    events = events.OrderByDescending(s => s.EventDetail);
                    break;
                case "AmountAsc":
                    events = events.OrderBy(s => s.FulfillmentAmount);
                    break;
                case "AmountDesc":
                    events = events.OrderByDescending(s => s.FulfillmentAmount);
                    break;
                case "DateAsc":
                    events = events.OrderBy(s => s.FulfillmentDate);
                    break;
                case "DateDesc":
                    events = events.OrderByDescending(s => s.FulfillmentDate);
                    break;
                default:
                    events = events.OrderBy(s => s.Id);
                    break;
            }
            if (!String.IsNullOrEmpty(surnameFilter))
            {
                events = events.Where(s => s.InsuranceContracts.Insured.Surname.Contains(surnameFilter));
            }
            if (!String.IsNullOrEmpty(typFilter))
            {
                events = events.Where(s => s.InsuranceContracts.Insurance.Type.Contains(typFilter));
            }
            if (!String.IsNullOrEmpty(detailFilter))
            {
                events = events.Where(s => s.EventDetail.Contains(detailFilter));
            }
            var eventsQuery = _context.InsuranceEvents
                                          .Include(i => i.InsuranceContracts)
                                          .AsQueryable();
            // Počet záznamů pro stránkování
            var totalRecords = await eventsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            // Data pro aktuální stránku            
            var viewModel = new InsuranceEventsIndexViewModel
            {
                Events = events,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                SortOrder = sortOrder,
                SurnameFilter = surnameFilter,
                TypFilter = typFilter,
                DetailFilter = detailFilter
            };
            return View(viewModel);
        }
        /// <summary>
        /// Zobrazuje detaily konkrétní pojistné události.
        /// </summary>
        /// <param name="id">Identifikátor pojistné události.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insuranceEvents = await _context.InsuranceEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceEvents == null)
            {
                return NotFound();
            }
            return View(insuranceEvents);
        }
        /// <summary>
        /// Zobrazuje formulář pro vytvoření nové pojistné události.
        /// </summary>
        /// <param name="insuredId">Volitelný identifikátor pojistníka pro předvyplnění.</param>
        /// <returns>Akce vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceEvents/Create
        public async Task<IActionResult> Create(int? insuredId)
        {
            var insurances = _context.Insurances.Select(i => new
            {
                Id = i.Id,
                Type = $"{i.Type}"
            }).ToList();
            var insuranceContracts = _context.InsuranceContracts.Select(ic => new
            {
                Id = ic.Id,
                Name = $"{ic.Insurance.Type} - {ic.NameSubject}" // Případně upravte podle skutečné struktury vaší databáze
            }).ToList();
            ViewBag.InsuranceId = new SelectList(insurances, "Id", "Type");
            ViewBag.InsuranceContractId = new SelectList(insuranceContracts, "Id", "Name");
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
        /// Zpracuje data formuláře pro vytvoření nové pojistné události.
        /// </summary>
        /// <param name="insuranceEvents">Data pojistné události.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: InsuranceEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InsuredId,InsuranceId, InsuranceContractId, EventDetail,FulfillmentAmount,FulfillmentDate,CreationDate,LastChange,EventsCount")] InsuranceEvents insuranceEvents)
        {
            if (ModelState.IsValid)
            {
                insuranceEvents.CreationDate = DateTime.Now;
                insuranceEvents.LastChange = DateTime.Now;
                _context.Add(insuranceEvents);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Pojistná událost úspěšně vytvořena"; // Uložení zprávy o úspěchu
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id", insuranceEvents.InsuranceContracts.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "Id", insuranceEvents.InsuranceContracts.InsuredId);
            return View(insuranceEvents);
        }
        /// <summary>
        /// Zobrazuje formulář pro úpravu pojistné události.
        /// </summary>
        /// <param name="id">Identifikátor pojistné události.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insuranceEvent = await _context.InsuranceEvents.FindAsync(id);
            if (insuranceEvent == null)
            {
                return NotFound();
            }
            var insurancesList = _context.Insurances.Select(ins => new
            {
                Id = ins.Id,
                Type = ins.Type
            }).ToList();
            var insuranceContracts = _context.InsuranceContracts.Select(ic => new
            {
                Id = ic.Id,
                Name = $"{ic.NameSubject}" // Případně upravte podle skutečné struktury vaší databáze
            }).ToList();
            ViewBag.InsuranceId = new SelectList(insurancesList, "Id", "Type", insuranceEvent.InsuranceContracts.InsuranceId);
            ViewBag.InsuranceContractId = new SelectList(insuranceContracts, "Id", "Name");
            var insuredsList = _context.Insureds.Select(ins => new
            {
                Id = ins.Id,
                NameSurname = $"{ins.Name} {ins.Surname}"
            }).ToList();
            ViewBag.InsuredId = new SelectList(insuredsList, "Id", "NameSurname", insuranceEvent.InsuranceContracts.InsuredId);
            return View(insuranceEvent);
        }
        /// <summary>
        /// Zpracuje data formuláře pro úpravu pojistné události.
        /// </summary>
        /// <param name="id">Identifikátor pojistné události.</param>
        /// <param name="insuranceEvents">Aktualizovaná data pojistné události.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: InsuranceEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InsuredId,InsuranceId,InsuranceContractId,EventDetail,FulfillmentAmount,FulfillmentDate,LastChange,EventsCount")] InsuranceEvents insuranceEvent)
        {
            if (id != insuranceEvent.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var insuranceEventInDb = await _context.InsuranceEvents.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
                    insuranceEvent.CreationDate = insuranceEventInDb.CreationDate; // Přiřazení původní hodnoty CreationDate
                    insuranceEvent.LastChange = DateTime.Now;
                    _context.Update(insuranceEvent);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Pojistná událost úspěšně upravena";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceEventExists(insuranceEvent.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            // Příprava SelectListů pro zobrazení ve view, stejně jako v metodě GET
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Type", insuranceEvent.InsuranceContracts.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "NameSurname", insuranceEvent.InsuranceContracts.InsuredId);
            return View(insuranceEvent);
        }
        private bool InsuranceEventExists(int id)
        {
            return _context.InsuranceEvents.Any(e => e.Id == id);
        }
        /// <summary>
        /// Zobrazuje potvrzení smazání pojistné události.
        /// </summary>
        /// <param name="id">Identifikátor pojistné události.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // GET: InsuranceEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var insuranceEvents = await _context.InsuranceEvents
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceEvents == null)
            {
                return NotFound();
            }
            return View(insuranceEvents);
        }
        /// <summary>
        /// Zpracuje smazání pojistné události.
        /// </summary>
        /// <param name="id">Identifikátor pojistné události.</param>
        /// <returns>Asynchronní operace, která vrací <see cref="IActionResult"/>.</returns>
        // POST: InsuranceEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insuranceEvents = await _context.InsuranceEvents.FindAsync(id);
            if (insuranceEvents != null)
            {
                _context.InsuranceEvents.Remove(insuranceEvents);
            }
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Pojistná událost úspěšně odstraněna"; // Uložení zprávy o úspěchu
            return RedirectToAction(nameof(Index));
        }
        /// <summary>
        /// Kontroluje, zda pojistná událost existuje.
        /// </summary>
        /// <param name="id">Identifikátor pojistné události.</param>
        /// <returns>Pravda, pokud pojistná událost existuje; jinak nepravda.</returns>
        private bool InsuranceEventsExists(int id)
        {
            return _context.InsuranceEvents.Any(e => e.Id == id);
        }
    }
}
