using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReStyleUp.DTOs.Commande;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class CommandeController : ControllerBase
    {
        private readonly ICommandeService _commandeService;

        public CommandeController(ICommandeService commandeService)
        {
            _commandeService = commandeService;
        }

        // GET: api/Commande
        [HttpGet]
        public ActionResult<IEnumerable<CommandeReadDto>> GetAllCommandes()
        {
            var commandes = _commandeService.GetAllCommandes();
            return Ok(commandes);
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
        public IActionResult AddCommande([FromBody] CommandeCreateDto commandeCreateDto)
        {
            _commandeService.AddCommande(commandeCreateDto);
            return Ok(new { message = "Commande créée avec succès." });
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
