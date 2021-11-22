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
    public class LocationsController : Controller
    {
        private readonly MvcMovieContext _context;

        public LocationsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            var mvcMovieContext = _context.Location.Include(l => l.LocationClientId).Include(l => l.LocationFilmId);
            return View(await mvcMovieContext.ToListAsync());
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .Include(l => l.LocationClientId)
                .Include(l => l.LocationFilmId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "NomClient");
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "NomFilm");
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilmId,ClientId,DateRetourLocation,RenduFilm")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                //TODO : Modifier de +1 le nombre de location du client
             /*   Film film = (from f in _context.Film where f.Id == location.Id select f).First();
                if (film == null)
                {
                    return NotFound();
                }
                film.NbLocationsFilm++;
                film.DisponibiliteFilm = false;


                var film
                 = await _context.Film 
                 .Include(f => f.Id)
               .Include(f => f.LocationFilmId)
               .FirstOrDefaultAsync(m => m.Id == id);*/










                //_context.Location.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "NomClient", location.ClientId);
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "NomFilm", location.FilmId);
            return View(location);

        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "NomClient", location.ClientId);
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "NomFilm", location.FilmId);
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FilmId,ClientId,DateRetourLocation,RenduFilm")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "NomClient", location.ClientId);
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "NomFilm", location.FilmId);
            return View(location);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .Include(l => l.LocationClientId)
                .Include(l => l.LocationFilmId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Location.FindAsync(id);
            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.Id == id);
        }
    }
}
