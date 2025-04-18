using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Services
{
    public class AnnonceService : IAnnonceService
    {
        private readonly ApplicationDbContext _context;

        public AnnonceService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Annonce> GetAllAnnonces()
        {
            return _context.Annonces.Include(a => a.Utilisateur).ToList();
        }

        public Annonce GetAnnonceById(int id)
        {
            return _context.Annonces.Include(a => a.Utilisateur).FirstOrDefault(a => a.Id == id);
        }
        public IEnumerable<Annonce> GetAnnoncesByUserId(int userId)
        {
            return _context.Annonces
                           .Where(a => a.UtilisateurId == userId)
                           .Include(a => a.Utilisateur)
                           .ToList();
        }

        public IEnumerable<Annonce> GetAnnonceByUserName(string userName)
        {
            return _context.Annonces
                           .Where(a => a.Utilisateur.Nom == userName)
                           .Include(a => a.Utilisateur)
                           .ToList();
        }
        public IEnumerable<Annonce> GetAnnonceByDate(DateTime date)
        {
            return _context.Annonces
                            .Where(a => a.DatePublication == date)
                            .Include(a => a.DatePublication)
                            .ToList();
        }

        public void AddAnnonce(Annonce annonce)
        {
            _context.Annonces.Add(annonce);
            _context.SaveChanges();
        }

        public void UpdateAnnonce(Annonce annonce)
        {
            _context.Annonces.Update(annonce);
            _context.SaveChanges();
        }

        public void DeleteAnnonce(int id)
        {   
            var annonce = _context.Annonces.Find(id);
            if (annonce != null)
            {
                _context.Annonces.Remove(annonce);
                _context.SaveChanges();
            }
        }
    }
}
