namespace ReStyleUp.DTOs.Commande
{
    public class CommandeUpdateDto
    {
        public DateTime DateCommande { get; set; }
        public float MontantTotal { get; set; }

        public int UtilisateurId { get; set; }

        public List<int> ArticlesIds { get; set; } = new List<int>();
    }
}
