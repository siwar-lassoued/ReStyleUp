using Microsoft.AspNetCore.Mvc;
using ReStyleUp.DTOs.Annonce;
using ReStyleUp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ReStyleUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class AnnoncesController : ControllerBase
    {
        private readonly IAnnonceService _annonceService;
        private readonly ILogger<AnnoncesController> _logger;

        public AnnoncesController(
            IAnnonceService annonceService,
            ILogger<AnnoncesController> logger)
        {
            _annonceService = annonceService;
            _logger = logger;
        }

        // GET: api/Annonces
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<AnnonceReadDto>> GetAllAnnonces()
        {
            try
            {
                _logger.LogInformation("Récupération de toutes les annonces");
                var annonces = _annonceService.GetAllAnnonces();
                return Ok(annonces);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de toutes les annonces");
                return StatusCode(500, "Erreur lors de la récupération des annonces");
            }
        }

        // GET: api/Annonces/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<AnnonceReadDto> GetAnnonceById(int id)
        {
            try
            {
                _logger.LogInformation($"Récupération de l'annonce avec l'ID {id}");
                var annonce = _annonceService.GetAnnonceById(id);
                if (annonce == null)
                {
                    _logger.LogWarning($"Annonce avec l'ID {id} non trouvée");
                    return NotFound($"Annonce avec l'ID {id} non trouvée");
                }
                return Ok(annonce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la récupération de l'annonce avec l'ID {id}");
                return StatusCode(500, $"Erreur lors de la récupération de l'annonce avec l'ID {id}");
            }
        }

        // POST: api/Annonces
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult CreateAnnonce([FromBody] AnnonceCreateDto annonceDto)
        {
            try
            {
                // Validation
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Modèle invalide lors de la création d'une annonce");
                    return BadRequest(ModelState);
                }

                // Log information about the request
                _logger.LogInformation($"Tentative de création d'une annonce: {annonceDto.Titre} " +
                                       $"pour l'utilisateur {annonceDto.UtilisateurId}");

                // Get current user id from claims
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _logger.LogInformation($"Utilisateur courant: {currentUserId}");

                // Check if the current user is Admin or the same as the user ID in the DTO
                bool isAdmin = User.IsInRole("Admin");
                bool isCurrentUser = currentUserId == annonceDto.UtilisateurId.ToString();

                // If not admin and trying to create an annonce for a different user
                if (!isAdmin && !isCurrentUser)
                {
                    _logger.LogWarning($"L'utilisateur {currentUserId} a essayé de créer une annonce " +
                                     $"pour un autre utilisateur {annonceDto.UtilisateurId}");
                    return Forbid("Vous ne pouvez pas créer une annonce pour un autre utilisateur");
                }

                var annonceId = _annonceService.AddAnnonce(annonceDto);
                _logger.LogInformation($"Annonce créée avec succès, ID: {annonceId}");

                return CreatedAtAction(nameof(GetAnnonceById), new { id = annonceId }, annonceDto);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Argument null lors de la création d'une annonce");
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Argument invalide lors de la création d'une annonce");
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Erreur applicative lors de la création d'une annonce");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la création d'une annonce");
                return StatusCode(500, "Une erreur inattendue est survenue lors de la création de l'annonce. " +
                                      "Veuillez contacter l'administrateur système.");
            }
        }

        // PUT: api/Annonces/5
        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult UpdateAnnonce(int id, [FromBody] AnnonceUpdateDto annonceUpdateDto)
        {
            try
            {
                _logger.LogInformation($"Tentative de mise à jour de l'annonce {id}");

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Modèle invalide lors de la mise à jour d'une annonce");
                    return BadRequest(ModelState);
                }

                // Get the existing annonce to check ownership
                var existingAnnonce = _annonceService.GetAnnonceById(id);
                if (existingAnnonce == null)
                {
                    _logger.LogWarning($"Annonce avec l'ID {id} non trouvée lors d'une tentative de mise à jour");
                    return NotFound($"Annonce avec l'ID {id} non trouvée");
                }

                // Get current user id from claims
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Check if the current user is Admin or the owner of the annonce
                bool isAdmin = User.IsInRole("Admin");
                bool isOwner = currentUserId == existingAnnonce.UtilisateurId.ToString();

                if (!isAdmin && !isOwner)
                {
                    _logger.LogWarning($"L'utilisateur {currentUserId} a essayé de mettre à jour " +
                                     $"l'annonce {id} qui appartient à {existingAnnonce.UtilisateurId}");
                    return Forbid("Vous ne pouvez pas modifier une annonce qui ne vous appartient pas");
                }

                _annonceService.UpdateAnnonce(id, annonceUpdateDto);
                var updatedAnnonce = _annonceService.GetAnnonceById(id);
                _logger.LogInformation($"Annonce {id} mise à jour avec succès");

                return Ok(updatedAnnonce);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la mise à jour de l'annonce {id}");
                return StatusCode(500, $"Erreur lors de la mise à jour de l'annonce {id}");
            }
        }

        // DELETE: api/Annonces/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAnnonce(int id)
        {
            try
            {
                _logger.LogInformation($"Tentative de suppression de l'annonce {id}");

                var existingAnnonce = _annonceService.GetAnnonceById(id);
                if (existingAnnonce == null)
                {
                    _logger.LogWarning($"Annonce avec l'ID {id} non trouvée lors d'une tentative de suppression");
                    return NotFound($"Annonce avec l'ID {id} non trouvée");
                }

                _annonceService.DeleteAnnonce(id);
                _logger.LogInformation($"Annonce {id} supprimée avec succès");

                return Ok(new { message = "Annonce supprimée avec succès" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erreur lors de la suppression de l'annonce {id}");
                return StatusCode(500, $"Erreur lors de la suppression de l'annonce {id}");
            }
        }
    }
}