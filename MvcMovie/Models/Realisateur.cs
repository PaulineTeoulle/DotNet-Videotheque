using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class Realisateur
    {
        public int Id { get; set; }


        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le nom du réalisateur est requis.")]

        public string NomRealisateur { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Le prénom du réalisateur est requis.")]
        public string PrenomRealisateur { get; set; }

    }
}
