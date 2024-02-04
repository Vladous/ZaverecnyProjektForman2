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
    public class InsuredsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public InsuredsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager; // Přidáno
        }

        // GET: Insureds
        public async Task<IActionResult> Index()
        {
            var insuredList = await _context.Insureds
              .Include(i => i.InsuranceContracts)
              .ToListAsync();
            return View(insuredList);
        }

        // GET: Insureds/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Insureds/Create
        public IActionResult Create()
        {
            return View();
        }

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
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    insured.UserCreated = currentUser;
                    insured.UserLastChanged = currentUser;
                }

                _context.Add(insured);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insured);
        }

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

        private bool InsuredExists(int id)
        {
            return _context.Insureds.Any(e => e.Id == id);
        }
    }
}
