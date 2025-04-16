using ReStyleUp.Models;


namespace ReStyleUp.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Prix { get; set; }

        public EtatArticle Etat { get; set; }

        public int AnnonceId { get; set; }
        public Annonce Annonce { get; set; }

        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }

}
