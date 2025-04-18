namespace ReStyleUp.DTOs.Utilisateur
{
    public class UtilisateurReadDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Email { get; set; }
        public string Adresse { get; set; }
        public string Telephone { get; set; }
        public List<int> CommandeIds { get; set; }
        public List<int> AnnonceIds { get; set; }
    }
}
