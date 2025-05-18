namespace ReStyleUp.DTOs.Article
{
    public class ArticleUpdateDto
    {
        public string Categorie { get; set; } = string.Empty;

        public string Nom { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Prix { get; set; }
        public string Etat { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

    }
}
