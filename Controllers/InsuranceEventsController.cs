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
    public class InsuranceEventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InsuranceEventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Přidáno
        }

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
                           .Include(i => i.Insurance)
                           .Include(i => i.Insured)
            select s;

            switch (sortOrder)
            {
                case "SurnameAsc":
                    events = events.OrderBy(s => s.Insured.Surname);
                    break;
                case "SurnameDesc":
                    events = events.OrderByDescending(s => s.Insured.Surname);
                    break;
                case "InsuranceAsc":
                    events = events.OrderBy(s => s.Insurance.Type);
                    break;
                case "InsuranceDesc":
                    events = events.OrderByDescending(s => s.Insurance.Type);
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
                events = events.Where(s => s.Insured.Surname.Contains(surnameFilter));
            }
            if (!String.IsNullOrEmpty(typFilter))
            {
                events = events.Where(s => s.Insurance.Type.Contains(typFilter));
            }
            if (!String.IsNullOrEmpty(detailFilter))
            {
                events = events.Where(s => s.EventDetail.Contains(detailFilter));
            }

            var eventsQuery = _context.InsuranceEvents
                                          .Include(i => i.InsuranceContracts)
                                          .Include(i => i.Insurance)
                                          .Include(i => i.Insured)
                                          .AsQueryable();

            // Vaše stávající logika pro řazení a filtry

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

        // GET: InsuranceEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceEvents = await _context.InsuranceEvents
                .Include(i => i.Insurance)
                .Include(i => i.Insured)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceEvents == null)
            {
                return NotFound();
            }

            return View(insuranceEvents);
        }

        // GET: InsuranceEvents/Create
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


        // POST: InsuranceEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InsuredId,InsuranceId,EventDetail,FulfillmentAmount,FulfillmentDate,CreationDate,LastChange,EventsCount")] InsuranceEvents insuranceEvents)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceEvents);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id", insuranceEvents.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "Id", insuranceEvents.InsuredId);
            return View(insuranceEvents);
        }

        // GET: InsuranceEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceEvents = await _context.InsuranceEvents.FindAsync(id);
            if (insuranceEvents == null)
            {
                return NotFound();
            }

            var insureds = _context.Insureds.Select(i => new
            {
                Id = i.Id,
                NameSurname = $"{i.Name} {i.Surname}"
            }).ToList();

            var insurances = _context.InsuranceContracts.Select(i => new
            {
                Id = i.Id,
                InsuranceType = $"{i.Insurance.Type} + {i.NameSubject}" // Předpokládám, že máte vlastnosti Type a NameSubject ve vaší třídě Insurance
            }).ToList();


            ViewData["InsuranceId"] = new SelectList(insurances, "Id", "InsuranceType", insuranceEvents.InsuranceId);
            ViewData["InsuredId"] = new SelectList(insureds, "Id", "NameSurname", insuranceEvents.InsuredId);
            return View(insuranceEvents);
        }

        // POST: InsuranceEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InsuredId,InsuranceId,EventDetail,FulfillmentAmount,FulfillmentDate,CreationDate,LastChange,EventsCount")] InsuranceEvents insuranceEvents)
        {
            if (id != insuranceEvents.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceEvents);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceEventsExists(insuranceEvents.Id))
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
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id", insuranceEvents.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "Id", insuranceEvents.InsuredId);
            return View(insuranceEvents);
        }

        // GET: InsuranceEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insuranceEvents = await _context.InsuranceEvents
                .Include(i => i.Insurance)
                .Include(i => i.Insured)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceEvents == null)
            {
                return NotFound();
            }

            return View(insuranceEvents);
        }

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
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceEventsExists(int id)
        {
            return _context.InsuranceEvents.Any(e => e.Id == id);
        }
    }
}
