using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcMovie.Data;
using MvcMovie.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {

        private readonly MvcMovieContext _context;


    
        public HomeController(MvcMovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var movies = from m in _context.Film
                         select m;
            ViewBag.Message = "Welcome to my demo!";
            ViewData["Films"] = GetFilms();
            ViewData["Locations"] = GetLocations();
            return View();
        }

        private IEnumerable<Film> GetFilms()
        {
            IEnumerable<Film> films = (from m in _context.Film
                               select m).ToList();
            return films;
        }


        private IEnumerable<Location> GetLocations()
        {
            IEnumerable<Location> locations = (from m in _context.Location
                                            select m).ToList();
            return locations;
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
