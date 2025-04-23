using Microsoft.AspNetCore.Mvc;
using ReStyleUp.Services.Interfaces;
using ReStyleUp.DTOs.Image;
using Swashbuckle.AspNetCore.Annotations;
using ReStyleUp.Models;
using ReStyleUp.DTOs.Annonce;
using ReStyleUp.Services;

namespace ReStyleUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _env;

        public ImageController(IImageService imageService, IWebHostEnvironment env)
        {
            _imageService = imageService;
            _env = env;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Upload une image", Description = "Téléverse une image avec option pour l'associer à une annonce")]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageDto model)
        {
            if (model.File == null || model.File.Length == 0)
                return BadRequest("Aucun fichier uploadé");

            // Validation du type de fichier
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(model.File.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
                return BadRequest("Seuls les formats JPG, JPEG, PNG et GIF sont acceptés");

            // Créer un nom unique
            var uniqueFileName = $"{Guid.NewGuid()}{extension}";

            // Chemin de sauvegarde (supprimé la déclaration en double)
            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads");
            Directory.CreateDirectory(uploadsPath);
            var filePath = Path.Combine(uploadsPath, uniqueFileName);
            // Sauvegarde physique
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.File.CopyToAsync(stream);
            }

            // Sauvegarde en base
            var imageDto = new ImageCreateDto
            {
                Url = $"/uploads/{uniqueFileName}",
                AnnonceId = model.AnnonceId
            };

            try
            {
                _imageService.AddImage(imageDto);
                return Ok(new
                {
                    Url = imageDto.Url,
                    FileName = model.File.FileName,
                    Size = model.File.Length,
                    AnnonceId = model.AnnonceId
                });
            }
            catch (ArgumentException ex)
            {
                System.IO.File.Delete(filePath);
                return BadRequest(new
                {
                    Error = ex.Message,
                    Solution = "Soit spécifiez un AnnonceId valide, soit laissez le champ vide"
                });
            }
        }
            // GET: api/Image
            [HttpGet]
         public ActionResult<IEnumerable<ImageReadDto>> GetAllImages()
        {
            var images = _imageService.GetAllImages();
            return Ok(images);
        }
        // PUT: api/Image/5
        [HttpPut("{id}")]
        public IActionResult UpdateImage(int id, [FromBody] ImageUpdateDto imageUpdateDto)
        {
            _imageService.UpdateImage(id, imageUpdateDto);

            var updatedImage = _imageService.GetImageById(id);
            return Ok(updatedImage);
        }
        // DELETE: api/Image/5
        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            _imageService.DeleteImage(id);
            return Ok(new { message = "Image supprimée avec succès"});
        }
    }
}
