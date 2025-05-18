using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

public class UploadImageDto
{
    [Required]
    [SwaggerSchema("Fichier image à uploader")]
    public IFormFile File { get; set; }

    [SwaggerSchema("ID de l'article associée (optionnel)", Nullable = true)]
    public int? ArticleId { get; set; }
}