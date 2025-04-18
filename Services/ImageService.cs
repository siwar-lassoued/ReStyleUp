using Microsoft.EntityFrameworkCore;
using ReStyleUp.Models;
using ReStyleUp.Data;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;
        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Image> GetAllImages()
        {
            return _context.Images.Include(i => i.Id).ToList();
        }
        public Image GetImageById(int id)
        {
            return _context.Images.Include(i => i.Id).FirstOrDefault(i => i.Id == id);
        }
        public IEnumerable<Image> GetImageByUrl(string url)
        {
            return _context.Images
                           .Where(i => i.Url == url)
                           .Include(i => i.Id)
                           .ToList();
        }
        public IEnumerable<Image> GetImageByAnnonceId(int annonceId)
        {
            return _context.Images
                           .Where(i => i.AnnonceId == annonceId)
                           .Include(i => i.Id)
                           .ToList();
        }
        public void AddImage(Image image)
        {
            _context.Images.Add(image);
            _context.SaveChanges();
        }
        public void UpdateImage(Image image)
        {
            _context.Images.Update(image);
            _context.SaveChanges();
        }
        public void DeleteImage(int id)
        {
            var image = _context.Images.Find(id);
            if (image != null)
            {
                _context.Images.Remove(image);
                _context.SaveChanges();
            }
        }
    }
}
