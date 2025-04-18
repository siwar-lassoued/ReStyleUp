using ReStyleUp.DTOs.Utilisateur;
using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface IUtilisateurService
    {
        public IEnumerable<UtilisateurReadDto> GetAllUtilisateurs();
        public UtilisateurReadDto GetUtilisateurById(int id);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByNom(string nom);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByEmail(string email);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByAdresse(string address);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByTelephone(string telephone);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByCommandeId(int commandeId);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByAnnonceId(int annonceId);
        public void AddUtilisateur(UtilisateurCreateDto utilisateur);
        void UpdateUtilisateur(int id, UtilisateurUpdateDto utilisateur);
        public void DeleteUtilisateur(int id);
    }
}
