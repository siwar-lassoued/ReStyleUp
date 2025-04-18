using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.DTOs.Utilisateur;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UtilisateurService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<UtilisateurReadDto> GetAllUtilisateurs()
        {
            var utilisateurs = _context.Utilisateurs.Include(u => u.Email).ToList();
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        }

        public UtilisateurReadDto GetUtilisateurById(int id)
        {
            var utilisateur = _context.Utilisateurs.Include(u => u.Email).FirstOrDefault(u => u.Id == id);
            return _mapper.Map<UtilisateurReadDto>(utilisateur);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByNom(string nom)
        {
            var utilisateurs = _context.Utilisateurs
                                        .Where(u => u.Nom == nom)
                                        .Include(u => u.Email)
                                        .ToList();
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByEmail(string email)
        {
            var utilisateurs = _context.Utilisateurs
                                        .Where(u => u.Email == email)
                                        .Include(u => u.Email)
                                        .ToList();
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByAdresse(string address)
        {
            var utilisateurs = _context.Utilisateurs
                                        .Where(u => u.Adresse == address)
                                        .Include(u => u.Email)
                                        .ToList();
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByTelephone(string telephone)
        {
            var utilisateurs = _context.Utilisateurs
                                        .Where(u => u.Telephone == telephone)
                                        .Include(u => u.Email)
                                        .ToList();
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByCommandeId(int commandeId)
        {
            var commande = _context.Commandes
                                   .Include(c => c.Utilisateur)
                                   .FirstOrDefault(c => c.Id == commandeId);

            if (commande == null || commande.Utilisateur == null)
                return new List<UtilisateurReadDto>();

            var utilisateur = new List<Utilisateur> { commande.Utilisateur };
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateur);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByAnnonceId(int annonceId)
        {
            var annonce = _context.Annonces
                                  .Include(a => a.Utilisateur)
                                  .FirstOrDefault(a => a.Id == annonceId);

            if (annonce == null || annonce.Utilisateur == null)
                return new List<UtilisateurReadDto>();

            var utilisateur = new List<Utilisateur> { annonce.Utilisateur };
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateur);
        }


        public void AddUtilisateur(UtilisateurCreateDto utilisateurCreateDto)
        {
            var utilisateur = _mapper.Map<Utilisateur>(utilisateurCreateDto);
            _context.Utilisateurs.Add(utilisateur);
            _context.SaveChanges();
        }

        public void UpdateUtilisateur(int id, UtilisateurUpdateDto utilisateurUpdateDto)
        {
            var utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.Id == id);
            if (utilisateur != null)
            {
                _mapper.Map(utilisateurUpdateDto, utilisateur);
                _context.Utilisateurs.Update(utilisateur);
                _context.SaveChanges();
            }
        }

        public void DeleteUtilisateur(int id)
        {
            var utilisateur = _context.Utilisateurs.Find(id);
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
                _context.SaveChanges();
            }
        }
    }
}
