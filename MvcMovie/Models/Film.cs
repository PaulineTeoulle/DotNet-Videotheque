using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Display(Name = "Titre")]
        [Required(ErrorMessage = "Le titre du film est obligatoire.")]
        public string NomFilm { get; set; }
      
        public virtual Realisateur FilmRealisateurId{ get; set; }
        public int RealisateurId { get; set; }

        [Display(Name = "Date de parution")]
        [DisplayFormat(HtmlEncode = false, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "L'année de sortie du film est requise.")]
        public DateTime DateSortieFilm { get; set; }
        [Display(Name = "Nombre de locations")]
        public int NbLocationsFilm { get; set; }

        [Display(Name = "Disponibilité")]
        public Boolean DisponibiliteFilm { get; set; }

        [Display(Name = "Catégorie")]
        [Required(ErrorMessage = "La catégorie du film est requise.")]
        public string CategorieFilm { get; set; }

        public Film()
        {
            // quand le film est créé, il est de suite disponible à la location
            this.DisponibiliteFilm = true;
        }
    }
}