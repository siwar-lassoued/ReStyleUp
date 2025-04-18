namespace ReStyleUp.DTOs.Commande
{
    public class CommandeReadDto
    {
        public int Id { get; set; }
        public DateTime DateCommande { get; set; }
        public float MontantTotal { get; set; }

        public int UtilisateurId { get; set; }
        public string UtilisateurNom { get; set; }

        public List<int> ArticlesIds { get; set; } = new List<int>();
    }
}
