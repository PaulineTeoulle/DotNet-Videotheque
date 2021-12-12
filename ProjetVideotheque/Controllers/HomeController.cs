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

        public IActionResult Index()
        {

            ViewData["Films"] = GetFilms();
            ViewData["Locations"] = GetLocations();
            return View();
        }

        private IEnumerable<Film> GetFilms()
        {
            IEnumerable<Film> films = (from m in _context.Film
                                       select m).Distinct().ToList();
            return !films.Any() ? null : films.OrderByDescending(film => film.NbLocationsFilm).Take(10);
        }


        private IEnumerable<Location> GetLocations()
        {
            var locations = (from m in _context.Location
                             select m).ToList();

            if (!locations.Any()) return null;
            else return locations;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}