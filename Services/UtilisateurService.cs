using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.Services.Interfaces;
namespace ReStyleUp.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly ApplicationDbContext _context;
        public UtilisateurService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Utilisateur> GetAllUtilisateurs()
        {
            return _context.Utilisateurs.Include(u => u.Email).ToList();
        }
        public Utilisateur GetUtilisateurById(int id)
        {
            return _context.Utilisateurs.Include(u => u.Email).FirstOrDefault(u => u.Id == id);
        }
        public IEnumerable<Utilisateur> GetUtilisateurByNom(string nom)
        {
            return _context.Utilisateurs
                           .Where(u => u.Nom == nom)
                           .Include(u => u.Email)
                           .ToList();
        }
        public IEnumerable<Utilisateur> GetUtilisateurByEmail(string email)
        {
            return _context.Utilisateurs
                           .Where (u => u.Email == email)
                           .Include(u => u.Email)
                           .ToList();
        }
        public IEnumerable<Utilisateur> GetUtilisateurByAdresse(string address)
        {
            return _context.Utilisateurs
                           .Where(u => u.Adresse == address)
                           .Include(u => u.Email)
                           .ToList() ;
        }
        public IEnumerable<Utilisateur> GetUtilisateurByTelephone(string telephone)
        {
            return _context.Utilisateurs
                           .Where(u => u.Telephone == telephone)
                           .Include(u => u.Email)
                           .ToList() ;
        }
        public IEnumerable<Utilisateur> GetUtilisateurByCommandeId(int commandeId)
        {
            return _context.Utilisateurs
                           .Where(u => u.CommandeId == commandeId)
                           .Include(u => u.Email)
                           .ToList() ;
        }
        public IEnumerable<Utilisateur> GetUtilisateurByAnnonceId(int annonceId)
        {
            return _context.Utilisateurs
                           .Where(u => u.AnnonceId == annonceId)
                           .Include(u => u.Email)
                           .ToList() ;
        }
        public void AddUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Add(utilisateur);
            _context.SaveChanges() ;
        }
        public void UpdateUtilisateur(Utilisateur utilisateur)
        {
            _context.Utilisateurs.Update(utilisateur);
            _context.SaveChanges() ;
        }
        public void DeleteUtilisateur(int id)
        {
            var utilisateur = _context.Utilisateurs.Find(id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
                _context.SaveChanges() ;
            }
        }
    }
}
