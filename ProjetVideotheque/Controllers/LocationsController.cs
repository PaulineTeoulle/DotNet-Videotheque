﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjetVideotheque.Data;
using ProjetVideotheque.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetVideotheque.Controllers
{
    public class LocationsController : Controller
    {
        private readonly Context _context;

        public LocationsController(Context context)
        {
            _context = context;
        }

        // GET: Locations
        public async Task<IActionResult> Index(string searchString)
        {
            var locations = from m in _context.Location
                            select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(s => s.LocationClientId.NomClient.Contains(searchString) ||
                                            s.LocationClientId.PrenomClient.Contains(searchString) ||
                                            s.LocationFilmId.NomFilm.Contains(searchString) ||
                                            s.LocationFilmId.RealisateurFilm.Contains(searchString) ||
                                            s.LocationFilmId.CategorieFilm.Contains(searchString)
                );

            }

            locations = locations.Include(l => l.LocationClientId).Include(l => l.LocationFilmId);

            return View(await locations.ToListAsync());
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
            var films = from b in _context.Film
                        where b.DisponibiliteFilm == true
                        select b;

            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "NomClient");
            ViewData["FilmId"] = new SelectList(films, "Id", "NomFilm");
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
                /*var locationCreated = _context.Location.Find(location.FilmId);
                locationCreated.DateDebutLocation = DateTime.Now;
                _context.Update(locationCreated);*/

                var film = _context.Film.Find(location.FilmId);
                film.NbLocationsFilm++;
                film.DisponibiliteFilm = false;
                _context.Update(film);

                var client = _context.Client.Find(location.ClientId);
                client.NbFilmsLoues++;
                _context.Update(client);



                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "NomClient", location.ClientId);
            ViewData["FilmId"] = new SelectList(_context.Film, "Id", "NomFilm", location.FilmId);
            return View(location);

        }


        // GET: Locations/Edit/5
        public async Task<IActionResult> ReturnFilm(int? id)
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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnFilmConfirmed(int id)
        {


            var location = await _context.Location.FindAsync(id);
            location.RenduFilm = true;
            location.DateRetourLocation = DateTime.Now;
            _context.Update(location);

            var film = _context.Film.Find(location.FilmId);
            film.DisponibiliteFilm = true;
            _context.Update(film);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        // POST: Locations/Delete/5
        [HttpPost, ActionName("ReturnConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnConfirmed(int id)
        {
            var location = await _context.Location.FindAsync(id);
            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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



        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            var location = await _context.Location
               .Include(l => l.LocationClientId)
               .Include(l => l.LocationFilmId)
               .FirstOrDefaultAsync(m => m.Id == id);

            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Location location)
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
            return View(location);

        }


    }
}
