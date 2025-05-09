using Microsoft.AspNetCore.Identity;
using ReStyleUp.DTOs.Utilisateur;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReStyleUp.Services.Interfaces
{
    public interface IUtilisateurService
    {
        IEnumerable<IdentityUser> GetUsersList();

        // New methods for GUID ID-based operations
        Task<UtilisateurReadDto> GetIdentityUserById(string id);
        Task<UtilisateurReadDto> AddUtilisateurForIdentityUser(string identityUserId, UtilisateurCreateDto dto);
        Task UpdateUtilisateur(string id, UtilisateurUpdateDto utilisateurUpdateDto);
        Task DeleteUtilisateur(string id);

        // Legacy methods for integer ID-based operations
        UtilisateurReadDto GetUtilisateurById(int id);
        UtilisateurReadDto AddUtilisateur(UtilisateurCreateDto dto);

        // Query methods
        IEnumerable<UtilisateurReadDto> GetUtilisateurByNom(string nom);
        IEnumerable<UtilisateurReadDto> GetUtilisateurByEmail(string email);
        IEnumerable<UtilisateurReadDto> GetUtilisateurByAdresse(string address);
        IEnumerable<UtilisateurReadDto> GetUtilisateurByTelephone(string telephone);
        IEnumerable<UtilisateurReadDto> GetUtilisateurByCommandeId(int commandeId);
        IEnumerable<UtilisateurReadDto> GetUtilisateurByAnnonceId(int annonceId);
    }
}