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
            var images = _context.Images.Include(i => i.Article).ToList();
            return _mapper.Map<IEnumerable<ImageReadDto>>(images);
        }

        public ImageReadDto GetImageById(int id)
        {
            var image = _context.Images.Include(i => i.Article).FirstOrDefault(i => i.Id == id);
            return _mapper.Map<ImageReadDto>(image);
        }

        public IEnumerable<ImageReadDto> GetImageByUrl(string url)
        {
            var images = _context.Images
                                 .Where(i => i.Url == url)
                                 .Include(i => i.Article)
                                 .ToList();
            return _mapper.Map<IEnumerable<ImageReadDto>>(images);
        }

        public IEnumerable<ImageReadDto> GetImageByArticleId(int articleId)
        {
            var images = _context.Images
                                 .Where(i => i.ArticleId == articleId)
                                 .Include(i => i.Article)
                                 .ToList();
            return _mapper.Map<IEnumerable<ImageReadDto>>(images);
        }

        public void AddImage(ImageCreateDto imageDto)
        {
            // Si un ArticleId est spécifié, vérifiez qu'il existe
            if (imageDto.ArticleId.HasValue)
            {
                var articleExists = _context.Articles.Any(a => a.Id == imageDto.ArticleId.Value);
                if (!articleExists)
                {
                    throw new ArgumentException("L'article spécifiée n'existe pas");
                }
            }

            var image = new Image
            {
                Url = imageDto.Url,
                ArticleId = imageDto.ArticleId
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
