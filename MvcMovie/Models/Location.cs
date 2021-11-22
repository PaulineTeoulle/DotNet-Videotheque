using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
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


        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date de retour")]
        public DateTime DateRetourLocation { get; set; }
        public Boolean RenduFilm { get; set; }

        public Location()
        {
            this.RenduFilm = false;
        }
    }
}