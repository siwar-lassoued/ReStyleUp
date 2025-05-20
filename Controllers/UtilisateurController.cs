using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReStyleUp.DTOs.Utilisateur;
using ReStyleUp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReStyleUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;

        public UtilisateurController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        // GET: api/Utilisateur
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        public ActionResult<IEnumerable<IdentityUser>> GetAllUtilisateurs()
        {
            var utilisateurs = _utilisateurService.GetUsersList();
            return Ok(utilisateurs);
        }

        // GET: api/Utilisateur/5 (string ID for GUID)
        [HttpGet("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<UtilisateurReadDto>> GetUtilisateurById(string id)
        {
            // Determine if the ID is a GUID (for new system) or an integer (for legacy system)
            if (Guid.TryParse(id, out Guid guidId))
            {
                // Use the identity user lookup for GUID IDs
                var utilisateur = await _utilisateurService.GetIdentityUserById(id);
                if (utilisateur == null)
                    return NotFound();
                return Ok(utilisateur);
            }
            else if (int.TryParse(id, out int intId))
            {
                // Use the legacy lookup for integer IDs
                var utilisateur = _utilisateurService.GetUtilisateurById(intId);
                if (utilisateur == null)
                    return NotFound();
                return Ok(utilisateur);
            }

            return BadRequest("Invalid ID format");
        }

        // PUT: api/Utilisateur/5
        [HttpPut("{id}")]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> UpdateUtilisateur(string id, [FromBody] UtilisateurUpdateDto utilisateurUpdateDto)
        {
            if (!Guid.TryParse(id, out _))
            {
                return BadRequest("Invalid ID format");
            }

            try
            {
                await _utilisateurService.UpdateUtilisateur(id, utilisateurUpdateDto);
                var updatedUtilisateur = await _utilisateurService.GetIdentityUserById(id);
                return Ok(updatedUtilisateur);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Utilisateur/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUtilisateur(string id)
        {
            if (Guid.TryParse(id, out Guid guidId))
            {
                // Use the identity user approach for GUID IDs
                var utilisateur = await _utilisateurService.GetIdentityUserById(id);
                if (utilisateur == null)
                    return NotFound();

                await _utilisateurService.DeleteUtilisateur(id);
                return Ok(new { message = "Utilisateur supprimé avec succès" });
            }
            else if (int.TryParse(id, out int intId))
            {
                // Legacy delete path for integer IDs
                var utilisateur = _utilisateurService.GetUtilisateurById(intId);
                if (utilisateur == null)
                    return NotFound();

                // Note: You'll need to add an overload of DeleteUtilisateur for int IDs
                // or convert int to string here
                return BadRequest("Deletion usingg legacy IDs is not supported yet");
            }

            return BadRequest("Invalid ID format");
        }
    }
}