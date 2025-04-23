using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("Utilisateur")]
        public int UtilisateurId { get; set; } // Doit correspondre à un ID existant dans Utilisateurs
        public Utilisateur Utilisateur { get; set; }
        public Article Articles { get; set; } 
        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
    
}
