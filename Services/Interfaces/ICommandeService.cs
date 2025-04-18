using ReStyleUp.DTOs.Commande;
using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface ICommandeService
    {
        public IEnumerable<CommandeReadDto> GetAllCommandes();
        public CommandeReadDto GetCommandeById(int id);
        public IEnumerable<CommandeReadDto> GetCommandesByUtilisateurId(int utilisateurId);
        public void AddCommande(CommandeCreateDto commande);
        public void UpdateCommande(int id, CommandeUpdateDto commandeDto);
        public void DeleteCommande(int id);
    }
}
