using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetVideotheque.Data;
using ProjetVideotheque.Models;

namespace ProjetVideotheque.Controllers
{
    public class FilmsController : Controller
    {
        private readonly Context _context;

        public FilmsController(Context context)
        {
            _context = context;
        }

        // GET: Films
        public async Task<IActionResult> Index(string searchString)
        {

            var movies = from m in _context.Film
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
               // movies = movies.Where(s => s.NomFilm.Contains(searchString));  
                movies = movies.Where(s => s.NomFilm.Contains(searchString) || s.CategorieFilm.Contains(searchString) || s.RealisateurFilm.Contains(searchString));

            }

            return View(await movies.ToListAsync());
        }

        // GET: Films/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film

                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            ViewBag.disponibilite = film.DisponibiliteFilm;

            ViewData["Locations"] = GetLocations(id);

            return View(film);
        }


        private IEnumerable<Location> GetLocations(int? id)
        {

            if (id == null)
            {
                return null;
            }
            var locations = _context.Location
                .Where(m => m.FilmId == id && m.RenduFilm== false)
               .Include(l => l.LocationClientId)
               .Include(l => l.LocationFilmId).ToList();


            if (!locations.Any()) return null;
            else return locations.Distinct();

        }



        // GET: Films/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomFilm,DateSortieFilm,NbLocationsFilm,RealisateurFilm,DisponibiliteFilm,CategorieFilm,PrixParJour")] Film film)
        {
            if (ModelState.IsValid)
            {
                _context.Add(film);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(film);
        }

        // GET: Films/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film.FindAsync(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomFilm,DateSortieFilm,NbLocationsFilm,RealisateurFilm,DisponibiliteFilm,CategorieFilm,PrixParJour")] Film film)
        {
            if (id != film.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(film);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FilmExists(film.Id))
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
            return View(film);
        }

        // GET: Films/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Film
                .FirstOrDefaultAsync(m => m.Id == id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Film.FindAsync(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }
    }
}
