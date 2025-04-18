using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface IUtilisateurService
    {
        public IEnumerable<Utilisateur> GetAllUtilisateurs();
        public Utilisateur GetUtilisateurById(int id);
        public IEnumerable<Utilisateur> GetUtilisateurByNom(string nom);    
        public IEnumerable<Utilisateur> GetUtilisateurByEmail(string email);    
        public IEnumerable<Utilisateur> GetUtilisateurByAdresse(string address);    
        public IEnumerable<Utilisateur> GetUtilisateurByTelephone(string telephone);
        public IEnumerable<Utilisateur> GetUtilisateurByCommandeId(int commandeId);
        public IEnumerable<Utilisateur> GetUtilisateurByAnnonceId(int annonceId);
        public void AddUtilisateur(Utilisateur utilisateur);
        public void UpdateUtilisateur(Utilisateur utilisateur);
        public void DeleteUtilisateur(int id);
    }
}
