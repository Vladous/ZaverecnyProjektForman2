using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZaverecnyProjektForman2.Data;
using ZaverecnyProjektForman2.Models;

namespace ZaverecnyProjektForman2.Controllers
{
    public class InsuranceContractsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceContractsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceContracts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InsuranceContracts.Include(i => i.Insurance).Include(i => i.Insured);
            return View(await applicationDbContext.ToListAsync());
        }

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

        // GET: InsuranceContracts/Create
        public IActionResult Create(int insuredId)
        {
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id");
            //ViewData["InsuredId"] = insuredId; // Předání ID pojistníka do view
            return View();
        }

        // POST: InsuranceContracts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InsuredId,InsuranceId,CreationDate,LastChange")] InsuranceContracts insuranceContracts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceContracts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id", insuranceContracts.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "Id", insuranceContracts.InsuredId);
            return View(insuranceContracts);
        }

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
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id", insuranceContracts.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "Id", insuranceContracts.InsuredId);
            return View(insuranceContracts);
        }

        // POST: InsuranceContracts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,InsuredId,InsuranceId,CreationDate,LastChange")] InsuranceContracts insuranceContracts)
        {
            if (id != insuranceContracts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceId"] = new SelectList(_context.Insurances, "Id", "Id", insuranceContracts.InsuranceId);
            ViewData["InsuredId"] = new SelectList(_context.Insureds, "Id", "Id", insuranceContracts.InsuredId);
            return View(insuranceContracts);
        }

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
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceContractsExists(int id)
        {
            return _context.InsuranceContracts.Any(e => e.Id == id);
        }
    }
}
