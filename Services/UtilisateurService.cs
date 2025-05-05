using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.DTOs.Utilisateur;
using ReStyleUp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ReStyleUp.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;


        public UtilisateurService(ApplicationDbContext context, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;

        }

        public IEnumerable<IdentityUser> GetUsersList()
        {
            return _userManager.Users.ToList();
        }
        //public IEnumerable<UtilisateurReadDto> GetAllUtilisateurs()
        //{
        //    var utilisateurs = _context.Utilisateurs.ToList();
        //    return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        //}

        public UtilisateurReadDto GetUtilisateurById(int id)
        {
            var utilisateur = _context.Users.Find(id);
            return _mapper.Map<UtilisateurReadDto>(utilisateur);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByNom(string nom)
        {
            var utilisateurs = _context.Utilisateurs
                                        .Where(u => u.Nom == nom)
                                        .ToList();
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByEmail(string email)
        {
            var utilisateurs = _context.Utilisateurs
                                        .Where(u => u.Email == email)
                                        .ToList();
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByAdresse(string address)
        {
            var utilisateurs = _context.Utilisateurs
                                        .Where(u => u.Adresse == address)
                                        .ToList();
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateurs);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByTelephone(string telephone)
        {
            var utilisateurs = _context.Utilisateurs
                                        .Where(u => u.Telephone == telephone)
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


        public UtilisateurReadDto AddUtilisateur(UtilisateurCreateDto dto)
        {
            var utilisateur = new Utilisateur
            {
                Nom = dto.Nom,
                Email = dto.Email,
                MotDePasse = dto.MotDePasse,
                Adresse = dto.Adresse,
                Telephone = dto.Telephone
            };

            _context.Utilisateurs.Add(utilisateur);
            _context.SaveChanges();

            return new UtilisateurReadDto
            {
                Nom = utilisateur.Nom,
                Email = utilisateur.Email,
                Adresse = utilisateur.Adresse,
                Telephone = utilisateur.Telephone
            };
        }


        public void UpdateUtilisateur(int id, UtilisateurUpdateDto utilisateurUpdateDto)
        {
            var utilisateur = _context.Utilisateurs.Find(id);
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
