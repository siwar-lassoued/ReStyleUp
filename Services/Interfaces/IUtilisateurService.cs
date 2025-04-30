using Microsoft.AspNetCore.Identity;
using ReStyleUp.DTOs.Utilisateur;
using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface IUtilisateurService
    {
        IEnumerable<IdentityUser> GetUsersList();
        public IEnumerable<UtilisateurReadDto> GetAllUtilisateurs();
        public UtilisateurReadDto GetUtilisateurById(int id);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByNom(string nom);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByEmail(string email);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByAdresse(string address);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByTelephone(string telephone);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByCommandeId(int commandeId);
        public IEnumerable<UtilisateurReadDto> GetUtilisateurByAnnonceId(int annonceId);
        UtilisateurReadDto AddUtilisateur(UtilisateurCreateDto utilisateurCreateDto);
        void UpdateUtilisateur(int id, UtilisateurUpdateDto utilisateur);
        public void DeleteUtilisateur(int id);
    }
}
