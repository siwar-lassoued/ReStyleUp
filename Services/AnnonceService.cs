using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReStyleUp.Data;
using ReStyleUp.Models;
using ReStyleUp.DTOs.Annonce;
using ReStyleUp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReStyleUp.Services
{
    public class AnnonceService : IAnnonceService
    {
    
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AnnonceService> _logger;

        // Added logger for better error tracking
        public AnnonceService(
            ApplicationDbContext context,
            IMapper mapper,
            UserManager<IdentityUser> userManager,
            ILogger<AnnonceService> logger)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _logger = logger;
        }

        public IEnumerable<AnnonceReadDto> GetAllAnnonces()
        {
            try
            {
                var annonces = _context.Annonces.Include(a => a.Utilisateur).ToList();
                return _mapper.Map<IEnumerable<AnnonceReadDto>>(annonces);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de toutes les annonces");
                throw;
            }
        }

        public AnnonceReadDto GetAnnonceById(int id)
        {
            try
            {
                var annonce = _context.Annonces.Include(a => a.Utilisateur).FirstOrDefault(a => a.Id == id);
                return _mapper.Map<AnnonceReadDto>(annonce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération de l'annonce avec l'ID {id}");
                throw;
            }
        }

        public IEnumerable<AnnonceReadDto> GetAnnoncesByUtilisateurId(Guid utilisateurId)
        {
            try
            {
                var annonces = _context.Annonces
                                    .Where(a => a.UtilisateurId == utilisateurId)
                                    .Include(a => a.Utilisateur)
                                    .ToList();
                return _mapper.Map<IEnumerable<AnnonceReadDto>>(annonces);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération des annonces pour l'utilisateur {utilisateurId}");
                throw;
            }
        }

        public int AddAnnonce(AnnonceCreateDto annonceCreateDto)
        {
            try
            {
                // Validate input
                if (annonceCreateDto == null)
                {
                    throw new ArgumentNullException(nameof(annonceCreateDto), "Les données de l'annonce ne peuvent pas être nulles");
                }

                // Vérifier si l'utilisateur avec le UUID existe dans la table IdentityUsers
                var utilisateurId = annonceCreateDto.UtilisateurId;
                _logger.LogInformation($"Recherche de l'utilisateur avec l'ID {utilisateurId}");

                var identityUser = _userManager.FindByIdAsync(utilisateurId.ToString()).Result;

                if (identityUser == null)
                {
                    _logger.LogWarning($"L'utilisateur avec l'ID {utilisateurId} n'existe pas dans IdentityUsers");
                    throw new ArgumentException($"L'utilisateur avec l'ID {utilisateurId} n'existe pas");
                }

                _logger.LogInformation($"Utilisateur trouvé: {identityUser.UserName}");

                // Vérifier si un Utilisateur existe déjà pour cet identity user, sinon le créer
                var utilisateur = _context.Utilisateurs.FirstOrDefault(u => u.Id == utilisateurId);
                if (utilisateur == null)
                {
                    _logger.LogInformation($"Création d'un nouvel utilisateur dans la table Utilisateurs pour {utilisateurId}");

                    // Créer un nouvel Utilisateur basé sur les données de l'IdentityUser
                    utilisateur = new Utilisateur
                    {
                        Id = Guid.Parse(identityUser.Id),
                        Nom = identityUser.UserName ?? string.Empty,
                        Email = identityUser.Email ?? string.Empty,
                        // Autres champs avec des valeurs par défaut
                        Adresse = string.Empty,
                        Telephone = identityUser.PhoneNumber ?? string.Empty
                    };

                    _context.Utilisateurs.Add(utilisateur);
                    _context.SaveChanges();
                    _logger.LogInformation($"Nouvel utilisateur créé avec succès dans Utilisateurs");
                }

                // Créer l'annonce
                var annonce = new Annonce
                {
                    Titre = annonceCreateDto.Titre,
                    Description = annonceCreateDto.Description,
                    Prix = annonceCreateDto.Prix,
                    UtilisateurId = utilisateurId,
                    DatePublication = DateTime.Now  
                };

                _logger.LogInformation($"Tentative d'ajout d'une nouvelle annonce: {annonce.Titre}");

                // Ajouter l'annonce à la base de données
                _context.Annonces.Add(annonce);
                _context.SaveChanges();

                _logger.LogInformation($"Annonce créée avec succès, ID: {annonce.Id}");

                // Retourner l'ID de l'annonce créée
                return annonce.Id;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, $"Erreur de base de données lors de la création de l'annonce: {dbEx.InnerException?.Message ?? dbEx.Message}");
                throw new ApplicationException("Erreur lors de l'enregistrement de l'annonce dans la base de données", dbEx);
            }
            catch (Exception ex) when (!(ex is ArgumentException || ex is ArgumentNullException))
            {
                _logger.LogError(ex, $"Erreur inattendue lors de la création de l'annonce: {ex.Message}");
                throw new ApplicationException("Une erreur inattendue est survenue lors de la création de l'annonce", ex);
            }
        }

        public void UpdateAnnonce(int id, AnnonceUpdateDto annonceUpdateDto)
        {
            try
            {
                var annonce = _context.Annonces.FirstOrDefault(a => a.Id == id);
                if (annonce != null)
                {
                    // Mapping du DTO de mise à jour vers l'entité Annonce
                    _mapper.Map(annonceUpdateDto, annonce);
                    _context.Annonces.Update(annonce);
                    _context.SaveChanges();
                    _logger.LogInformation($"Annonce {id} mise à jour avec succès");
                }
                else
                {
                    _logger.LogWarning($"Tentative de mise à jour d'une annonce inexistante: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la mise à jour de l'annonce {id}");
                throw;
            }
        }

        public void DeleteAnnonce(int id)
        {
            try
            {
                var annonce = _context.Annonces.Find(id);
                if (annonce != null)
                {
                    _context.Annonces.Remove(annonce);
                    _context.SaveChanges();
                    _logger.LogInformation($"Annonce {id} supprimée avec succès");
                }
                else
                {
                    _logger.LogWarning($"Tentative de suppression d'une annonce inexistante: {id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la suppression de l'annonce {id}");
                throw;
            }
        }
    }
}