namespace ReStyleUp.DTOs.Article
{
    public class ArticleCreateDto
    {
        public string Categorie { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Prix { get; set; }

        public int AnnonceId { get; set; } 

    }
}
