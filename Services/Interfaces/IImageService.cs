using ReStyleUp.DTOs.Image;
using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface IImageService
    {
        public IEnumerable<ImageReadDto> GetAllImages();
        public ImageReadDto GetImageById(int id);
        public void AddImage(ImageCreateDto image);
        public void UpdateImage(int id, ImageUpdateDto imageDto);
        public void DeleteImage(int id);
    }
}
