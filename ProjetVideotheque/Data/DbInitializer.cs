using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProjetVideotheque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetVideotheque.Data
{
    public class DbInitializer
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<Context>();
                context.Database.EnsureCreated();

                var films = new Film[]
                {
                    new Film{
                        NomFilm = "Encanto, la fantastique famille Madrigal", 
                        RealisateurFilm = "Byron Howard", 
                        DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Animation",
                        PrixParJour = 4
                    },
                    new Film{
                        NomFilm = "Vice Versa",
                        RealisateurFilm = "Pete Docter",DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Animation",
                        PrixParJour = 4
                    },
                    new Film{
                        NomFilm = "Toy Story 4",
                        RealisateurFilm = "Josh Cooley",DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Animation",
                        PrixParJour = 4
                    },
                    new Film{
                        NomFilm = "Maléfique",
                        RealisateurFilm = "Robert Stromberg",DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Fantastique",
                        PrixParJour = 4
                    },
                    new Film{
                        NomFilm = "Le Livre de la jungle",
                        RealisateurFilm = "Jon Favreau",DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Aventure",
                        PrixParJour = 4
                    },
                    new Film{
                        NomFilm = "Monstres Academy",
                        RealisateurFilm = "Dan Scanlon",DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Aventure",
                        PrixParJour = 4
                    },
                    new Film{
                        NomFilm = "Dumbo",
                        RealisateurFilm = "Tim Burton",DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Famille",
                        PrixParJour = 4
                    },
                    new Film{
                        NomFilm = "Cendrillon",
                        RealisateurFilm = "Kenneth Branagh",DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Fantastique",
                        PrixParJour = 4
                    },
                    new Film{
                        NomFilm = "La Belle et la Bête",
                        RealisateurFilm = "Bill Condon",DateSortieFilm = DateTime.Now,
                        NbLocationsFilm = 0,
                        DisponibiliteFilm = true,
                        CategorieFilm = "Fantastique",
                        PrixParJour = 4
                    },
                };


                if (!context.Film.Any())
                {
                    foreach (Film film in films)
                    {
                        context.Film.Add(film);
                    }
                    context.SaveChanges();
                }

                var clients = new Client[]
                 {
                    new Client { 
                        NomClient = "Teoulle",
                        PrenomClient = "Pauline",
                        MailClient  = "pauline.teoulle@etu.univ-grenoble-alpes.fr",
                        AdresseClient = "15 rue du chemin, 38000 GRENOBLE",
                        NbFilmsLoues = 0
                    },
                    new Client { 
                        NomClient = "Tor",
                        PrenomClient = "Nade",
                        MailClient  = "tor.nade@gmail.com",
                        AdresseClient = "18 rue du vent, 38000 GRENOBLE",
                        NbFilmsLoues = 0 },
                    new Client {
                      
                        NomClient = "Arro",
                        PrenomClient = "Soir",
                        MailClient  = "arro.soir@etu.univ-grenoble-alpes.fr",
                        AdresseClient = "8 rue des jardins, 38000 GRENOBLE",
                        NbFilmsLoues = 0 },
                    new Client { 
                        NomClient = "Po",
                        PrenomClient = "Llen",
                        MailClient  = "po.llen@outlook.fr",
                        AdresseClient = "22 chemin des fleurs, 38000 GRENOBLE",
                        NbFilmsLoues = 0 },
                    new Client {
                        NomClient = "Roy",
                        PrenomClient = "Corentin",
                        MailClient  = "corentin.roy@etu.univ-grenoble-alpes.fr",
                        AdresseClient = "45 rue de la paix, 13008 MARSEILLE",
                        NbFilmsLoues = 0 },
                };



                if (!context.Client.Any())
                {
                    foreach (Client client in clients)
                    {
                        context.Client.Add(client);
                    }
                    context.SaveChanges();
                }




               /* var locations = new Location[]
                 {
                    new Location {
                        FilmId = films.Single( i => i.NomFilm == "Encanto, la fantastique famille Madrigal").Id,
                        ClientId = clients.Single( i => i.NomClient == "Teoulle").Id,
                        DateDebutLocation  = DateTime.Parse("01-02-2008"),
                        DateRetourLocation = DateTime.Parse("01-02-2008"),
                        RenduFilm = false
                    },
                    new Location {
                        FilmId =films.Single( i => i.NomFilm == "Vice Versa").Id,
                        ClientId = clients.Single( i => i.NomClient == "Roy").Id,
                        DateDebutLocation  = DateTime.Parse("01-02-2008"),
                        DateRetourLocation = DateTime.Parse("01-02-2008"),
                        RenduFilm = false },

                    new Location {
                        FilmId = films.Single( i => i.NomFilm == "Maléfique").Id,
                        ClientId = clients.Single( i => i.NomClient == "Llen").Id,
                        DateDebutLocation  = DateTime.Parse("01-02-2008"),
                        DateRetourLocation = DateTime.Parse("01-02-2008"),
                        RenduFilm = false 
                    },

                };

                if (!context.Location.Any())
                {
                    foreach (Location location in locations)
                    {
                        context.Location.Add(location);
                    }
                    context.SaveChanges();
                }*/
            }


          
        }
    }
}
