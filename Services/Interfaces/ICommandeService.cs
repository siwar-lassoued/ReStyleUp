using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface ICommandeService
    {
        IEnumerable<Commande> GetAllCommandes();
        Commande GetCommandeById(int id);
        IEnumerable<Commande> GetCommandeByUserName(string userName);
        //IEnumerable<Commande> GetCommandeByArticle(int articleId);
        void AddCommande(Commande commande);
        void UpdateCommande(Commande commande);
        void DeleteCommande(int id);
    }
}
