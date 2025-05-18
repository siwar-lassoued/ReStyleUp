using ReStyleUp.DTOs.Commande;
using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface ICommandeService
    {
        IEnumerable<CommandeReadDto> GetAllCommandes();
        CommandeReadDto GetCommandeById(int id);
        IEnumerable<CommandeReadDto> GetCommandesByUtilisateurId(string utilisateurId);
        Task<int> AddCommande(CommandeCreateDto commande);
        void UpdateCommande(int id, CommandeUpdateDto commandeDto);
        void DeleteCommande(int id);
    }
}
