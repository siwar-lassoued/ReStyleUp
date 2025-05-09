namespace ReStyleUp.Models
{
    public class Commande
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public float MontantTotal { get; set; }

        public Guid UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

        public ICollection<Article> Articles { get; set; } = new List<Article>();
    }


}
