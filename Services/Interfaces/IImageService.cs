using ReStyleUp.Models;
namespace ReStyleUp.Services.Interfaces
{
    public interface IImageService
    {
        public IEnumerable<Image> GetAllImages();
        public Image GetImageById(int id);
        public IEnumerable<Image> GetImageByUrl(string url);
        public IEnumerable<Image> GetImageByAnnonceId(int annonceId);
        public void AddImage(Image image);
        public void UpdateImage(Image image);
        public void DeleteImage(int id);
    }
}
