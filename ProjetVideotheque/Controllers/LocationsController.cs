using Microsoft.AspNetCore.Mvc;
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
        // Recherche la searchString dans la table Location et renvoie la liste à la Vue
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
        // Retourne les informations d'une location grâce à l'id passé en URL 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location location = await GetLocationInfosFromId(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // GET: Locations/Create
        // Renvoie la vue de création de location avec les films disponibles et les clients
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
        // Création d'une nouvelle location dans le contexte via les paramètres bindés
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FilmId,ClientId,DateRetourLocation,RenduFilm")] Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Add(location);
                //Récupération du film emprunté
                var film = _context.Film.Find(location.FilmId);
                film.NbLocationsFilm++;
                film.DisponibiliteFilm = false;
                _context.Update(film);

                //Récupération du client qui fait la location
                var client = _context.Client.Find(location.ClientId);
                client.NbFilmsLoues++;
                _context.Update(client);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);

        }

        // GET: Locations/ReturnFilm/5
        // Retourne les informations de la location grâce à l'id passé en URL
        public async Task<IActionResult> ReturnFilm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Location location = await GetLocationInfosFromId(id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/ReturnFilm/5
        // Update la date de retour de location et les booléens de disponibilité
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnFilmConfirmed(int id)
        {
            Location location = await GetLocationFromId(id);
            location.RenduFilm = true;
            location.DateRetourLocation = DateTime.Now;
            _context.Update(location);

            //Récupération du film
            var film = _context.Film.Find(location.FilmId);
            film.DisponibiliteFilm = true;
            _context.Update(film);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Location> GetLocationFromId(int id)
        {
            //Récupération de la location
            return await _context.Location.FindAsync(id);
        }

        // GET: Locations/Delete/5
        // Retourne les informations de la location à supprimer grâce à l'id passé en URL 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Location location = await GetLocationInfosFromId(id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        // Suppression de la location dans le context récupéré via l'id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Récupération de la location par l'id
            Location location = await GetLocationFromId(id);
            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Location.Any(e => e.Id == id);
        }



        // GET: Locations/Edit/5
        // Retourne les informations de la location à éditer grâce à l'id passé en URL 
        public async Task<IActionResult> Edit(int? id)
        {
            Location location = await GetLocationInfosFromId(id);

            return View(location);
        }

        // POST: Locations/Edit/5
        // Modification de la location dans le contexte 
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


        //Récupère les informations d'une location à partir de l'id
        private async Task<Location> GetLocationInfosFromId(int? id)
        {
            return await _context.Location
                .Include(l => l.LocationClientId)
                .Include(l => l.LocationFilmId)
                .FirstOrDefaultAsync(m => m.Id == id);
        }


    }
}
