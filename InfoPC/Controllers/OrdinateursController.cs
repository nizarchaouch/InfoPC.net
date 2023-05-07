using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InfoPC.Models.InfoPC;

namespace InfoPC.Controllers
{
    public class OrdinateursController : Controller
    {
        private readonly InfoPcDbContext _context;

        public OrdinateursController(InfoPcDbContext context)
        {
            _context = context;
        }

        // GET: Ordinateurs
        public async Task<IActionResult> Index()
        {
            var infoPcDbContext = _context.Ordinateurs.Include(o => o.Marque);
            return View(await infoPcDbContext.ToListAsync());
        }

        public async Task<IActionResult> PcAndTheirMarque()
        {
            var InfoPcDbContext = _context.Ordinateurs.Include(o => o.Marque);
            return View(await InfoPcDbContext.ToListAsync());
        }

        public IActionResult SearchByTitle(string title)

        {

            return View(_context.Ordinateurs.Where(m => m.Title.Contains(title)).ToList());
        }

        // GET: Ordinateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ordinateurs == null)
            {
                return NotFound();
            }

            var ordinateur = await _context.Ordinateurs
                .Include(o => o.Marque)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordinateur == null)
            {
                return NotFound();
            }

            return View(ordinateur);
        }

        // GET: Ordinateurs/Create
        public IActionResult Create()
        {
            ViewData["MarqueId"] = new SelectList(_context.Marques, "Id", "Id");
            return View();
        }

        // POST: Ordinateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Prix,MarqueId")] Ordinateur ordinateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordinateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MarqueId"] = new SelectList(_context.Marques, "Id", "Id", ordinateur.MarqueId);
            return View(ordinateur);
        }

        // GET: Ordinateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ordinateurs == null)
            {
                return NotFound();
            }

            var ordinateur = await _context.Ordinateurs.FindAsync(id);
            if (ordinateur == null)
            {
                return NotFound();
            }
            ViewData["MarqueId"] = new SelectList(_context.Marques, "Id", "Id", ordinateur.MarqueId);
            return View(ordinateur);
        }

        // POST: Ordinateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Prix,MarqueId")] Ordinateur ordinateur)
        {
            if (id != ordinateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordinateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdinateurExists(ordinateur.Id))
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
            ViewData["MarqueId"] = new SelectList(_context.Marques, "Id", "Id", ordinateur.MarqueId);
            return View(ordinateur);
        }

        


        // GET: Ordinateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ordinateurs == null)
            {
                return NotFound();
            }

            var ordinateur = await _context.Ordinateurs
                .Include(o => o.Marque)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ordinateur == null)
            {
                return NotFound();
            }

            return View(ordinateur);
        }

        // POST: Ordinateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ordinateurs == null)
            {
                return Problem("Entity set 'InfoPcDbContext.Ordinateurs'  is null.");
            }
            var ordinateur = await _context.Ordinateurs.FindAsync(id);
            if (ordinateur != null)
            {
                _context.Ordinateurs.Remove(ordinateur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdinateurExists(int id)
        {
          return (_context.Ordinateurs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
