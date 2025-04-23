namespace ReStyleUp.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int? AnnonceId { get; set; }
        public Annonce? Annonce { get; set; }
    }

}
