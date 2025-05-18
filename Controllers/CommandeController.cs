using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.DTOs.Commande;
using ReStyleUp.Models;
using ReStyleUp.Services;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class CommandeController : ControllerBase
    {
        private readonly ICommandeService _commandeService;
        private readonly ILogger<CommandeController> _logger;


        public CommandeController(ICommandeService commandeService, ILogger<CommandeController> logger)
        {
            _commandeService = commandeService;
            _logger = logger;
        }

        // GET: api/Commande
        [HttpGet]
        [Authorize(Roles = "Admin,User")]

        public ActionResult<IEnumerable<CommandeReadDto>> GetAllCommandes()
        {
            

            try
            {
                _logger.LogInformation("Récupération de toutes les commandes");
                var commandes = _commandeService.GetAllCommandes();
                return Ok(commandes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de toutes les commandes");
                return StatusCode(500, "Erreur lors de la récupération des commandes");
            }
        }

        // GET: api/Commande/5
        [HttpGet("{id}")]
        public ActionResult<CommandeReadDto> GetCommandeById(int id)
        {
            var commande = _commandeService.GetCommandeById(id);
            if (commande == null)
                return NotFound();

            return Ok(commande);
        }

        // POST: api/Commande
        [HttpPost]
        [Authorize(Roles = "User")]
      
        public async Task<IActionResult> AddCommande([FromBody] CommandeCreateDto commandeCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Modèle invalide lors de la création d'une commande");
                    return BadRequest(ModelState);
                }

                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _logger.LogInformation($"Utilisateur courant: {currentUserId}");

                bool isAdmin = User.IsInRole("Admin");
                bool isCurrentUser = currentUserId == commandeCreateDto.UtilisateurId.ToString();

                if (!isAdmin && !isCurrentUser)
                {
                    _logger.LogWarning($"L'utilisateur {currentUserId} a essayé de créer une commande " +
                                     $"pour un autre utilisateur {commandeCreateDto.UtilisateurId}");
                    return Forbid("Vous ne pouvez pas créer une commande pour un autre utilisateur");
                }

                // ✅ Appel au service pour créer la commande
                var newCommandeId = await _commandeService.AddCommande(commandeCreateDto);

                _logger.LogInformation($"Commande créée avec succès, ID: {newCommandeId}");

                return CreatedAtAction(nameof(GetCommandeById), new { id = newCommandeId }, null);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Argument null lors de la création d'une commande");
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Argument invalide lors de la création d'une commande");
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex)
            {
                _logger.LogError(ex, "Erreur applicative lors de la création d'une commande");
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue lors de la création d'une commande");
                return StatusCode(500, "Une erreur inattendue est survenue lors de la création de la commande. " +
                                      "Veuillez contacter l'administrateur système.");
            }
        }

        // PUT: api/Commande/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public IActionResult UpdateCommande(int id, [FromBody] CommandeUpdateDto commandeUpdateDto)
        {
            _commandeService.UpdateCommande(id, commandeUpdateDto);

            var updatedCommande = _commandeService.GetCommandeById(id);
            return Ok(updatedCommande);
        }

        // DELETE: api/Commande/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteCommande(int id)
        {
            _commandeService.DeleteCommande(id);
            return Ok(new {massage = "Commande supprimé avec suucès"});
        }
    }
}
