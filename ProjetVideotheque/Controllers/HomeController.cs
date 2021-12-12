using Microsoft.AspNetCore.Mvc;
using ProjetVideotheque.Data;
using ProjetVideotheque.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProjetVideotheque.Controllers
{
    public class HomeController : Controller
    {

        private readonly Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        //Retourne la vue avec la liste des 10 films les plus loués et les locations
        public IActionResult Index()
        {

            ViewData["Films"] = GetFilms();
            ViewData["Locations"] = GetLocations();
            return View();
        }

        //Récupère les 10 films les plus loués
        private IEnumerable<Film> GetFilms()
        {
            IEnumerable<Film> films = (from m in _context.Film
                                       select m).Distinct().ToList();
            return !films.Any() ? null : films.OrderByDescending(film => film.NbLocationsFilm).Take(10);
        }

        //Récupère toutes les locations
        private IEnumerable<Location> GetLocations()
        {
            var locations = (from m in _context.Location
                             select m).ToList();

            if (!locations.Any()) return null;
            else return locations;
        }
    }
}