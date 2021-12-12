using System;

using System.ComponentModel.DataAnnotations;

namespace ProjetVideotheque.Models
{
    public class Location
    {
        public int Id { get; set; }

        [Display(Name = "Nom du film")]
        public virtual Film LocationFilmId { get; set; }

        [Display(Name = "Film")]
        public int FilmId { get; set; }

        [Display(Name = "Nom du client")]
        public virtual Client LocationClientId { get; set; }

        [Display(Name = "Client")]
        public int ClientId { get; set; }

        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:d}")]
        [Display(Name = "Date de début")]
        public DateTime DateDebutLocation { get; set; }


        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:d}")]
        [Display(Name = "Date de retour")]
        public DateTime DateRetourLocation { get; set; }

        public bool RenduFilm { get; set; }

        public Location()
        {
            this.RenduFilm = false;
            this.DateDebutLocation = DateTime.Now;
        }
    }
}