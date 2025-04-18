using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Services
{
    public class CommandeService : ICommandeService
    {
        private readonly ApplicationDbContext _context;

        public CommandeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Commande> GetAllCommandes()
        {
            return _context.Commandes.Include(c => c.Utilisateur ).ToList();
        }
        public Commande GetCommandeById(int id)
        {
            return _context.Commandes.Include(c => c.Utilisateur).FirstOrDefault(c => c.Id == id);
        }
        public IEnumerable<Commande> GetCommandeByUserName(string userName)
        {
            return _context.Commandes
                           .Where(c => c.Utilisateur.Nom == userName)
                           .Include(c => c.Utilisateur)
                           .ToList();
        }
        public void AddCommande(Commande commande)
        {
            _context.Commandes.Add(commande);
            _context.SaveChanges();
        }
        public void UpdateCommande(Commande commande)
        {
            _context.Commandes.Update(commande);
            _context.SaveChanges();
        }
        public void DeleteCommande(int id)
        {
            var commande = _context.Commandes.Find(id);
            if (commande != null)
            {
                _context.Commandes.Remove(commande);
                _context.SaveChanges();
            }
            
        }

    }
}
