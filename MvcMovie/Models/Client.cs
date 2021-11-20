using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
  
    
        public class Client
        {
            public int Id { get; set; }

        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Le nom du client est requis.")]
        public string NomClient { get; set; }
        [Required(ErrorMessage = "Le prénom du client est requis.")]

        [Display(Name = "Prénom")]
        public string PrenomClient { get; set; }
        [Required(ErrorMessage = "L'adresse du client est requis.")]

        [Display(Name = "Adresse")]
        public string AdresseClient { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Le mail du client est requis.")]
        [EmailAddress]
        public string MailClient { get; set; }
        }
    
}
