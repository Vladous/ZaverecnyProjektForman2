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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InsuranceEvents.Include(i => i.Insurance).Include(i => i.Insured);
            return View(await applicationDbContext.ToListAsync());
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
