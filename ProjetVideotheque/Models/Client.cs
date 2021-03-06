using System.ComponentModel.DataAnnotations;

namespace ProjetVideotheque.Models
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

        [Display(Name = "Nombre de films loués")]
        public int NbFilmsLoues { get; set; }


        public Client()
        {
            // quand le film est créé, il est de suite disponible à la location
            this.NbFilmsLoues = 0;
        }
    }

}