using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReStyleUp.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public float MontantTotal { get; set; }

        public String UtilisateurId { get; set; }
        [ForeignKey("UtilisateurId")]

        public IdentityUser? Utilisateur { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }


}
