using Microsoft.AspNetCore.Mvc;
using ReStyleUp.DTOs.Annonce;
using ReStyleUp.Services;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnoncesController : ControllerBase
    {
        private readonly IAnnonceService _annonceService;

        public AnnoncesController(IAnnonceService annonceService)
        {
            _annonceService = annonceService;
        }

        // GET: api/Annonces
        [HttpGet]
        public ActionResult<IEnumerable<AnnonceReadDto>> GetAllAnnonces()
        {
            var annonces = _annonceService.GetAllAnnonces();
            return Ok(annonces);
        }

        // GET: api/Annonces/5
        [HttpGet("{id}")]
        public ActionResult<AnnonceReadDto> GetAnnonceById(int id)
        {
            var annonce = _annonceService.GetAnnonceById(id);
            if (annonce == null)
                return NotFound();

            return Ok(annonce);
        }

        // POST: api/Annonces
        [HttpPost]
        public IActionResult CreateAnnonce([FromBody] AnnonceCreateDto annonceDto)
        {
            try
            {
                var annonceId = _annonceService.AddAnnonce(annonceDto);
                return CreatedAtAction(nameof(GetAnnonceById), new { id = annonceId }, annonceDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erreur interne du serveur");
            }
        }


        // PUT: api/Annonces/5
        [HttpPut("{id}")]
        public IActionResult UpdateAnnonce(int id, [FromBody] AnnonceUpdateDto annonceUpdateDto)
        {
            _annonceService.UpdateAnnonce(id, annonceUpdateDto);

            var updatedAnnonce = _annonceService.GetAnnonceById(id);
            return Ok(updatedAnnonce); 
        }

        // DELETE: api/Annonces/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAnnonce(int id)
        {
            _annonceService.DeleteAnnonce(id);
            return Ok(new { message = "Annonce supprimé avec succès" });
        }
    }
}
