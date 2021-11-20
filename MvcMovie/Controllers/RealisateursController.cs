using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class RealisateursController : Controller
    {
        private readonly MvcMovieContext _context;

        public RealisateursController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Realisateurs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Realisateur.ToListAsync());
        }

        // GET: Realisateurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realisateur = await _context.Realisateur
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realisateur == null)
            {
                return NotFound();
            }

            return View(realisateur);
        }

        // GET: Realisateurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Realisateurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomRealisateur,PrenomRealisateur")] Realisateur realisateur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(realisateur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(realisateur);
        }

        // GET: Realisateurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realisateur = await _context.Realisateur.FindAsync(id);
            if (realisateur == null)
            {
                return NotFound();
            }
            return View(realisateur);
        }

        // POST: Realisateurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomRealisateur,PrenomRealisateur")] Realisateur realisateur)
        {
            if (id != realisateur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realisateur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealisateurExists(realisateur.Id))
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
            return View(realisateur);
        }

        // GET: Realisateurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realisateur = await _context.Realisateur
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realisateur == null)
            {
                return NotFound();
            }

            return View(realisateur);
        }

        // POST: Realisateurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realisateur = await _context.Realisateur.FindAsync(id);
            _context.Realisateur.Remove(realisateur);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealisateurExists(int id)
        {
            return _context.Realisateur.Any(e => e.Id == id);
        }
    }
}
