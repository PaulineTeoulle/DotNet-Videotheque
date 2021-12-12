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
    public class ClientsController : Controller
    {
        private readonly Context _context;

        public ClientsController(Context context)
        {
            _context = context;
        }



        // GET: Clients
        public async Task<IActionResult> Index(string searchString)
        {

            var clients = from m in _context.Client
                          select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                // movies = movies.Where(s => s.NomFilm.Contains(searchString));  
                clients = clients.Where(s => s.NomClient.Contains(searchString) ||
                                                s.PrenomClient.Contains(searchString) ||
                                                s.AdresseClient.Contains(searchString) ||
                                                s.MailClient.Contains(searchString));

            }

            return View(await clients.ToListAsync());
        }



        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);


            if (client == null)
            {
                return NotFound();
            }



            var locations = GetLocationsForOneClient(id);
            ViewData["Locations"] = locations;



            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomClient,PrenomClient,AdresseClient,MailClient")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomClient,PrenomClient,AdresseClient,MailClient")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }

        // GET: Clients/Facture
        public async Task<IActionResult> Facture(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            var locations = GetLocationsForOneClient(id);
            ViewData["Locations"] = locations;


            ViewBag.totalPrice = GetTotalPrice(locations);
            return View(client);
        }

        private static double GetTotalPrice(IEnumerable<Location> locations)
        {

            if (locations != null)
            {
                double totalPrice = 0;
                foreach (Location location in locations)
                {

                    totalPrice += location.LocationFilmId.PrixParJour * ((location.DateRetourLocation.Date - location.DateDebutLocation.Date).TotalDays);
                }
                return totalPrice;
            }
            return 0;
        }


        private IEnumerable<Location> GetLocationsForOneClient(int? id)
        {

            if (id == null)
            {
                return null;
            }
            var locations = _context.Location
                .Where(m => m.ClientId == id && m.RenduFilm == false)
               .Include(l => l.LocationClientId)
               .Include(l => l.LocationFilmId).ToList();


            if (!locations.Any()) return null;
            else return locations.Distinct();
        }



    }
}
