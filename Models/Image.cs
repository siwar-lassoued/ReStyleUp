namespace ReStyleUp.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int? ArticleId { get; set; }
        public Article? Article { get; set; }
    }

}
