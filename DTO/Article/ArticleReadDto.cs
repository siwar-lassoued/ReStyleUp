namespace ReStyleUp.DTOs.Article
{
    public class ArticleReadDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Prix { get; set; }
        public string Etat { get; set; } = string.Empty;  
        public int AnnonceId { get; set; }
        public string AnnonceTitre { get; set; } = string.Empty;  
    }
}
