using Microsoft.EntityFrameworkCore;
using ReStyleUp.Data;
using ReStyleUp.Services.Interfaces;
using ReStyleUp.Models;

namespace ReStyleUp.Services
{
    public class ArticleService : IArticleService

    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;

        }

        public IEnumerable<Article> GetAllArticles()
        {
            return _context.Articles.Include(a => a.Prix).ToList();
        }

        public Article GetArticleById(int id)
        {
            return _context.Articles.Include(a =>a.Prix).FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Article> GetArticleByName(string name)
        {
            return _context.Articles
                           .Where(a => a.Nom == name)
                           .Include(a => a.Prix)
                           .ToList();
        }

        public void AddArticle (Article article)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();
        }

        public void UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
            _context.SaveChanges();
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
