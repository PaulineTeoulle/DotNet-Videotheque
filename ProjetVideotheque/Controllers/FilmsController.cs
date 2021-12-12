using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetVideotheque.Data;
using ProjetVideotheque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        // Recherche la searchString dans la table Film et renvoie la liste à la Vue
        public async Task<IActionResult> Index(string searchString)
        {
            var movies = from m in _context.Film
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.NomFilm.Contains(searchString) || 
                                            s.CategorieFilm.Contains(searchString) ||
                                            s.RealisateurFilm.Contains(searchString));
            }

            return View(await movies.ToListAsync());
        }

        // GET: Films/Details/5
        // Retourne les informations d'un film grâce à l'id passé en URL 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Film film = await GetFilmFromId(id);
            if (film == null)
            {
                return NotFound();
            }
            //Récupération des locations en cours du film
            ViewBag.disponibilite = film.DisponibiliteFilm;
            ViewData["Locations"] = GetLocationsForOneFilm(id);

            return View(film);
        }

        // GET: Films/Create
        // Renvoie la vue de création de film
        public IActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // Création d'un nouveau film dans le contexte via les paramètres bindés
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
        // Retourne les informations d'un film grâce à l'id passé en URL 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Récupération du film grâce à l'id
            Film film = await GetFilmFromId(id);
            if (film == null)
            {
                return NotFound();
            }
            return View(film);
        }

        // POST: Films/Edit/5
        // Modification d'un film dans le contexte via les paramètres bindés
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
        // Retourne les informations d'un film grâce à l'id passé en URL 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Récupération du film grâce à l'id
            Film film = await GetFilmFromId(id);
            if (film == null)
            {
                return NotFound();
            }

            return View(film);
        }

        // POST: Films/Delete/5
        // Suppression du client dans le context récupéré via l'id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Récupération du film par l'id
            Film film = await GetFilmFromId(id);
            _context.Film.Remove(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //Check si le film existe
        private bool FilmExists(int id)
        {
            return _context.Film.Any(e => e.Id == id);
        }

        //Récupère le film depuis l'id
        private async Task<Film> GetFilmFromId(int? id)
        {
            return await _context.Film
                .FirstOrDefaultAsync(m => m.Id == id);
        }


        //Récupère les locations en cours pour un film
        private IEnumerable<Location> GetLocationsForOneFilm(int? id)
        {

            if (id == null)
            {
                return null;
            }
            var locations = _context.Location
                .Where(m => m.FilmId == id && m.RenduFilm == false)
               .Include(l => l.LocationClientId)
               .Include(l => l.LocationFilmId).ToList();


            if (!locations.Any()) return null;
            else return locations.Distinct();

        }
    }
}
