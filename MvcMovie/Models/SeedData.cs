using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (context.Client.Any() || context.Film.Any() || context.Location.Any())
                {
                    return;   // DB has been seeded
                }

              

                context.Client.AddRange(
                    new Client
                    {
                        PrenomClient = "Corentin",
                        NomClient ="ROY",
                        AdresseClient = "151548 Mombéliard",
                        MailClient = "darkcocoDu76@gmail.com"
                    },

                    new Client
                    {
                        PrenomClient = "Pauline",
                        NomClient = "TEOULLE",
                        AdresseClient = "151548 Avignon",
                        MailClient = "popodu84@gmail.com"
                    }

            
              );


                context.Film.AddRange(
                  new Film
                  {
                      NomFilm = "Coco",
                      RealisateurFilm = "test",
                      DateSortieFilm = DateTime.Now,
                      NbLocationsFilm = 15,
                      DisponibiliteFilm = true,
                      CategorieFilm = "Test"
                  },

                  new Film
                  {
                      NomFilm = "Blabla",
                      RealisateurFilm = "test",
                      DateSortieFilm = DateTime.Now,
                      NbLocationsFilm = 2,
                      DisponibiliteFilm = true,
                      CategorieFilm = "Categorie"
                  }
              );



                context.Location.AddRange(
                  new Location
                  {
                      FilmId = 0,
                      ClientId = 0,
                      DateRetourLocation = DateTime.Now,
                      RenduFilm = false
                  },

                  new Location
                  {
                      FilmId = 1,
                      ClientId = 1,
                      DateRetourLocation = DateTime.Now,
                      RenduFilm = false
                  }
              ) ;

                context.SaveChanges();
            }
        }
    }
}