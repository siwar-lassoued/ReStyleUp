using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReStyleUp.DTOs.Utilisateur;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilisateurController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;

        public UtilisateurController(IUtilisateurService utilisateurService)
        {
            _utilisateurService = utilisateurService;
        }

        // GET: api/Utilisateur
        [HttpGet]
        public ActionResult<IEnumerable<IdentityUser>> GetAllUtilisateurs()
        {
            var utilisateurs = _utilisateurService.GetUsersList();
            return Ok(utilisateurs);
        }

        // GET: api/Utilisateur/5
        [HttpGet("{id}")]
        public ActionResult<UtilisateurReadDto> GetUtilisateurById(int id)
        {
            var utilisateur = _utilisateurService.GetUtilisateurById(id);
            if (utilisateur == null)
                return NotFound();

            return Ok(utilisateur);
        }

        // POST: api/Utilisateur
        [HttpPost]
        public IActionResult AddUtilisateur([FromBody] UtilisateurCreateDto utilisateurCreateDto)
        {
            var utilisateur = _utilisateurService.AddUtilisateur(utilisateurCreateDto);
            return CreatedAtAction(nameof(GetUtilisateurById), new { id = utilisateur.Id }, utilisateur);

        }


        // PUT: api/Utilisateur/5
        [HttpPut("{id}")]
        public IActionResult UpdateUtilisateur(int id, [FromBody] UtilisateurUpdateDto utilisateurUpdateDto)
        {
            var existing = _utilisateurService.GetUtilisateurById(id);
            if (existing == null)
                return NotFound();

            _utilisateurService.UpdateUtilisateur(id, utilisateurUpdateDto);

            // Récupérer les nouvelles infos mises à jour
            var updatedUtilisateur = _utilisateurService.GetUtilisateurById(id);
            return Ok(updatedUtilisateur); // retourne les nouvelles données
        }


        // DELETE: api/Utilisateur/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUtilisateur(int id)
        {
            var utilisateur = _utilisateurService.GetUtilisateurById(id);
            if (utilisateur == null)
                return NotFound();

            _utilisateurService.DeleteUtilisateur(id);

            return Ok(new { message = "Utilisateur supprimé avec succès" });
        }

    }
}
