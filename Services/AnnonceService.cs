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

        public IEnumerable<AnnonceReadDto> GetAnnoncesByUtilisateurId(string utilisateurId)
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

        public async Task<int> AddAnnonce(AnnonceCreateDto annonceCreateDto)
        {
            try
            {
                // Validation des données d'entrée
                if (annonceCreateDto == null)
                    throw new ArgumentNullException(nameof(annonceCreateDto), "Les données de l'annonce ne peuvent pas être nulles.");

                if (string.IsNullOrWhiteSpace(annonceCreateDto.Titre))
                    throw new ArgumentException("Le titre de l'annonce est requis.");

                if (string.IsNullOrWhiteSpace(annonceCreateDto.Description))
                    throw new ArgumentException("La description de l'annonce est requise.");

                if (annonceCreateDto.Prix <= 0)
                    throw new ArgumentException("Le prix de l'annonce doit être supérieur à zéro.");

                // Vérifier si l'utilisateur existe dans IdentityUsers
                var utilisateurId = annonceCreateDto.UtilisateurId;
                _logger.LogInformation($"Recherche de l'utilisateur avec l'ID {utilisateurId}");

                var identityUser = await _userManager.FindByIdAsync(utilisateurId);
                if (identityUser == null)
                {
                    _logger.LogWarning($"L'utilisateur avec l'ID {utilisateurId} n'existe pas dans IdentityUsers");
                    throw new ArgumentException($"L'utilisateur avec l'ID {utilisateurId} n'existe pas");
                }

                _logger.LogInformation($"Utilisateur Identity trouvé: {identityUser.UserName}");

                // Création de l'annonce
                var annonce = new Annonce
                {
                    Titre = annonceCreateDto.Titre,
                    Description = annonceCreateDto.Description,
                    Prix = annonceCreateDto.Prix,
                    UtilisateurId = utilisateurId,
                    DatePublication = DateTime.Now
                };

                _logger.LogInformation($"Ajout de l'annonce : {annonce.Titre}");

                _context.Annonces.Add(annonce);

                try
                {
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Annonce créée avec succès. ID: {annonce.Id}");
                    return annonce.Id;
                }
                catch (DbUpdateException dbEx)
                {
                    _logger.LogError(dbEx, "Erreur lors de l'enregistrement de l'annonce.");
                    foreach (var entry in dbEx.Entries)
                    {
                        _logger.LogError($"Entité en erreur : {entry.Entity.GetType().Name}, État : {entry.State}");
                    }

                    throw new ApplicationException("Erreur lors de l'enregistrement de l'annonce dans la base de données", dbEx);
                }
            }
            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Erreur de validation des données : " + argEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la création de l'annonce.");
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