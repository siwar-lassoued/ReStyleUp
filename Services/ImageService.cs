using Microsoft.EntityFrameworkCore;
using ReStyleUp.Models;
using ReStyleUp.Data;
using ReStyleUp.Services.Interfaces;
using ReStyleUp.DTOs.Image;
using AutoMapper;

namespace ReStyleUp.Services
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ImageService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ImageReadDto> GetAllImages()
        {
            var images = _context.Images.Include(i => i.Annonce).ToList();
            return _mapper.Map<IEnumerable<ImageReadDto>>(images);
        }

        public ImageReadDto GetImageById(int id)
        {
            var image = _context.Images.Include(i => i.Annonce).FirstOrDefault(i => i.Id == id);
            return _mapper.Map<ImageReadDto>(image);
        }

        public IEnumerable<ImageReadDto> GetImageByUrl(string url)
        {
            var images = _context.Images
                                 .Where(i => i.Url == url)
                                 .Include(i => i.Annonce)
                                 .ToList();
            return _mapper.Map<IEnumerable<ImageReadDto>>(images);
        }

        public IEnumerable<ImageReadDto> GetImageByAnnonceId(int annonceId)
        {
            var images = _context.Images
                                 .Where(i => i.AnnonceId == annonceId)
                                 .Include(i => i.Annonce)
                                 .ToList();
            return _mapper.Map<IEnumerable<ImageReadDto>>(images);
        }

        public void AddImage(ImageCreateDto imageDto)
        {
            // Si un AnnonceId est spécifié, vérifiez qu'il existe
            if (imageDto.AnnonceId.HasValue)
            {
                var annonceExists = _context.Annonces.Any(a => a.Id == imageDto.AnnonceId.Value);
                if (!annonceExists)
                {
                    throw new ArgumentException("L'annonce spécifiée n'existe pas");
                }
            }

            var image = new Image
            {
                Url = imageDto.Url,
                AnnonceId = imageDto.AnnonceId
            };

            _context.Images.Add(image);
            _context.SaveChanges();
        }

        public void UpdateImage(int id, ImageUpdateDto imageDto)
        {
            var image = _context.Images.Find(id);
            if (image != null)
            {
                _mapper.Map(imageDto, image);
                _context.Images.Update(image);
                _context.SaveChanges();
            }
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
