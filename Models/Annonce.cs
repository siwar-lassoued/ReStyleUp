using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace ReStyleUp.Models
{
    public class Annonce 
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Prix { get; set; }
        public DateTime DatePublication { get; set; }

        public String UtilisateurId { get; set; }
        [ForeignKey("UtilisateurId")]

        public IdentityUser? Utilisateur { get; set; }

        public Article Articles { get; set; } 
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
    
}
