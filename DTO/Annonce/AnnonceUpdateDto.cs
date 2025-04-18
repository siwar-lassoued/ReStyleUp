namespace ReStyleUp.DTOs.Annonce
{
    public class AnnonceUpdateDto
    {
        public string Titre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Prix { get; set; }
    }
}
