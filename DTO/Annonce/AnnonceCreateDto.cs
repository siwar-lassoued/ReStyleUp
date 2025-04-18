namespace ReStyleUp.DTOs.Annonce
{
    public class AnnonceCreateDto
    {
        public string Titre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Prix { get; set; }
        public int UtilisateurId { get; set; }
        // Les images et articles peuvent être ajoutés ensuite via d'autres endpoints
    }
}
