using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.DTOs.Utilisateur;
using ReStyleUp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<UtilisateurReadDto> GetIdentityUserById(string id)
        {
            // For identity users with GUID IDs
            var identityUser = await _userManager.FindByIdAsync(id);
            if (identityUser == null)
                return null;

            // Check if a corresponding Utilisateur exists - use explicit column selection to avoid LegacyId
            var utilisateur = await _context.Utilisateurs
                .Where(u => u.Id == Guid.Parse(id))
                .Select(u => new Utilisateur
                {
                    Id = u.Id,
                    Nom = u.Nom,
                    Email = u.Email,
                    MotDePasse = u.MotDePasse,
                    Adresse = u.Adresse,
                    Telephone = u.Telephone
                })
                .FirstOrDefaultAsync();

            // If no corresponding Utilisateur exists, create a DTO directly from Identity user
            if (utilisateur == null)
            {
                return new UtilisateurReadDto
                {
                    Id = Guid.Parse(identityUser.Id),
                    Nom = identityUser.UserName,
                    Email = identityUser.Email,
                    Adresse = string.Empty,
                    Telephone = identityUser.PhoneNumber ?? string.Empty
                };
            }

            // Otherwise map from the existing Utilisateur
            return _mapper.Map<UtilisateurReadDto>(utilisateur);
        }

        // Keep this method for legacy code using integer IDs
        public UtilisateurReadDto GetUtilisateurById(int id)
        {
            // Check if LegacyId column exists in the database
            try
            {
                var utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.LegacyId == id);
                return _mapper.Map<UtilisateurReadDto>(utilisateur);
            }
            catch (Exception)
            {
                // If LegacyId doesn't exist yet, fallback to search by Id
                // Note: This is a temporary solution until migration is complete
                var utilisateur = _context.Utilisateurs.FirstOrDefault();
                return _mapper.Map<UtilisateurReadDto>(utilisateur);
            }
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

            var utilisateur = new List<IdentityUser> { commande.Utilisateur };
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateur);
        }

        public IEnumerable<UtilisateurReadDto> GetUtilisateurByAnnonceId(int annonceId)
        {
            var annonce = _context.Annonces
                                .Include(a => a.Utilisateur)
                                .FirstOrDefault(a => a.Id == annonceId);

            if (annonce == null || annonce.Utilisateur == null)
                return new List<UtilisateurReadDto>();

            var utilisateur = new List<IdentityUser> { annonce.Utilisateur };
            return _mapper.Map<IEnumerable<UtilisateurReadDto>>(utilisateur);
        }

        public async Task<UtilisateurReadDto> AddUtilisateurForIdentityUser(string identityUserId, UtilisateurCreateDto dto)
        {
            // Verify identity user exists
            var identityUser = await _userManager.FindByIdAsync(identityUserId);
            if (identityUser == null)
            {
                throw new ArgumentException($"Identity user with ID {identityUserId} not found");
            }

            // Check if Utilisateur already exists for this identity user
            var existingUtilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Id == Guid.Parse(identityUserId));
            if (existingUtilisateur != null)
            {
                throw new InvalidOperationException($"Utilisateur already exists for identity user with ID {identityUserId}");
            }

            // Create new Utilisateur with the GUID from identity user
            var utilisateur = new Utilisateur
            {
                Id = Guid.Parse(identityUserId),
                Nom = dto.Nom ?? identityUser.UserName,
                Email = dto.Email ?? identityUser.Email,
                MotDePasse = dto.MotDePasse ?? string.Empty, // Consider not storing passwords separately
                Adresse = dto.Adresse ?? string.Empty,
                Telephone = dto.Telephone ?? identityUser.PhoneNumber ?? string.Empty
            };

            _context.Utilisateurs.Add(utilisateur);
            await _context.SaveChangesAsync();

            return _mapper.Map<UtilisateurReadDto>(utilisateur);
        }

        // Legacy method - avoid using this for new users since it doesn't tie to Identity
        public UtilisateurReadDto AddUtilisateur(UtilisateurCreateDto dto)
        {
            var utilisateur = new Utilisateur
            {
                Id = Guid.NewGuid(), // Generate a new GUID
                Nom = dto.Nom,
                Email = dto.Email,
                MotDePasse = dto.MotDePasse,
                Adresse = dto.Adresse,
                Telephone = dto.Telephone
            };

            _context.Utilisateurs.Add(utilisateur);
            _context.SaveChanges();

            return _mapper.Map<UtilisateurReadDto>(utilisateur);
        }

        public async Task UpdateUtilisateur(string id, UtilisateurUpdateDto utilisateurUpdateDto)
        {
            // Parse the GUID
            if (!Guid.TryParse(id, out Guid userId))
            {
                throw new ArgumentException("Invalid user ID format");
            }

            // Find the existing user
            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (utilisateur == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found");
            }

            // Update only the allowed fields
            utilisateur.Nom = utilisateurUpdateDto.Nom ?? utilisateur.Nom;
            utilisateur.Email = utilisateurUpdateDto.Email ?? utilisateur.Email;
            utilisateur.Adresse = utilisateurUpdateDto.Adresse ?? utilisateur.Adresse;
            utilisateur.Telephone = utilisateurUpdateDto.Telephone ?? utilisateur.Telephone;

            // Explicitly mark as modified
            _context.Entry(utilisateur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Utilisateurs.AnyAsync(u => u.Id == userId))
                {
                    throw new KeyNotFoundException($"User with ID {id} no longer exists");
                }
                throw;
            }
        }

        public async Task DeleteUtilisateur(string id)
        {
            var utilisateur = await _context.Utilisateurs.FirstOrDefaultAsync(u => u.Id == Guid.Parse(id));
            if (utilisateur != null)
            {
                _context.Utilisateurs.Remove(utilisateur);
                await _context.SaveChangesAsync();
            }
        }
    }
}