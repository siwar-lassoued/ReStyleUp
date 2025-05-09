namespace ReStyleUp.DTOs.Annonce
{
    public class AnnonceReadDto
    {
        public int Id { get; set; }
        public string Titre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Prix { get; set; }
        public DateTime DatePublication { get; set; }
        public Guid UtilisateurId { get; set; }
        public string? UtilisateurNom { get; set; }
        public List<string> ImagesUrls { get; set; } = new();
    }
}
