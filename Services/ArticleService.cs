using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.DTOs.Article;
using ReStyleUp.Models;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArticleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<ArticleReadDto> GetAllArticles()
        {
            var articles = _context.Articles.ToList();
            return _mapper.Map<IEnumerable<ArticleReadDto>>(articles);
        }

        public ArticleReadDto GetArticleById(int id)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            return article == null ? null : _mapper.Map<ArticleReadDto>(article);
        }

        public IEnumerable<ArticleReadDto> GetArticlesByAnnonceId(int annonceId)
        {
            var articles = _context.Articles
                                   .Where(a => a.AnnonceId == annonceId)  
                                   .ToList();

            return _mapper.Map<IEnumerable<ArticleReadDto>>(articles);
        }

        public void AddArticle(ArticleCreateDto articleCreateDto)
        {
            var article = _mapper.Map<Article>(articleCreateDto);
            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        public void UpdateArticle(int id, ArticleUpdateDto articleUpdateDto)
        {
            var article = _context.Articles.FirstOrDefault(a => a.Id == id);
            if (article != null)
            {
                _mapper.Map(articleUpdateDto, article);
                _context.Articles.Update(article);
                _context.SaveChanges();
            }
        }

        public void DeleteArticle(int id)
        {
            var article = _context.Articles.Find(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                _context.SaveChanges();
            }
        }
    }
}
