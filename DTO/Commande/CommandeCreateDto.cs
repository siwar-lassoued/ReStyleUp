namespace ReStyleUp.DTOs.Commande
{
    public class CommandeCreateDto
    {
        public DateTime DateCommande { get; set; } = DateTime.Now;
        public float MontantTotal { get; set; }

        public int UtilisateurId { get; set; }

        public List<int> ArticlesIds { get; set; } = new List<int>();
    }
}
