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
        // Recherche la searchString dans la table Client et renvoie la liste à la Vue
        public async Task<IActionResult> Index(string searchString)
        {
            var clients = from m in _context.Client
                          select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(s => s.NomClient.Contains(searchString) ||
                                                s.PrenomClient.Contains(searchString) ||
                                                s.AdresseClient.Contains(searchString) ||
                                                s.MailClient.Contains(searchString));
            }
            return View(await clients.ToListAsync());
        }

        // GET: Clients/Details/5
        // Retourne les informations d'un client grâce à l'id passé en URL 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Client client = await GetClientFromId(id);

            if (client == null)
            {
                return NotFound();
            }

            //Récupération des locations du client
            var locations = GetLocationsForOneClient(id);
            ViewData["Locations"] = locations;

            return View(client);
        }

      

        // GET: Clients/Create
        // Renvoie la vue de création de client
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // Création d'un nouveau client dans le contexte via les paramètres bindés
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
        // Retourne les informations d'un client grâce à l'id passé en URL 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Récupération du client grâce à l'id
            Client client = await GetClientFromId(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // Modification d'un client dans le contexte via les paramètres bindés
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
        // Retourne les informations d'un client grâce à l'id passé en URL 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Récupération du client grâce à l'id
            Client client = await GetClientFromId(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        // Suppression du client dans le context récupéré via l'id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Récupération du client par l'id
            Client client = await GetClientFromId(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Check si le client existe
        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }

        // GET: Clients/Facture
        // Récupère les informations utiles à passer dans la vue pour la facture
        public async Task<IActionResult> Facture(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Récupération du client depuis l'id
            Client client = await GetClientFromId(id);

            if (client == null)
            {
                return NotFound();
            }

            //Récupération des locations du client
            var locations = GetLocationsForOneClient(id);
            ViewData["Locations"] = locations;
            // Récupération du prix total pour les locations du client
            ViewBag.totalPrice = GetTotalPrice(locations);
            return View(client);
        }


        //Récupère le client depuis l'id
        private async Task<Client> GetClientFromId(int? id)
        {
            return await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Récupère les locations en cours pour un client
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

        //Calcule le prix total des locations
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

    }
}
