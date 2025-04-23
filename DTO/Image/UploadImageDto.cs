using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

public class UploadImageDto
{
    [Required]
    [SwaggerSchema("Fichier image à uploader")]
    public IFormFile File { get; set; }

    [SwaggerSchema("ID de l'annonce associée (optionnel)", Nullable = true)]
    public int? AnnonceId { get; set; }
}