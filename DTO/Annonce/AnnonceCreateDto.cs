using System.ComponentModel.DataAnnotations;
using ReStyleUp.Models;

namespace ReStyleUp.DTOs.Annonce
{
    public class AnnonceCreateDto
    {
        [Required]
        public string Titre { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public float Prix { get; set; }
        [Required]
        public string UtilisateurId { get; set; }
    }
}
