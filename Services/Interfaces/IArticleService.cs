using ReStyleUp.DTOs.Article;
using ReStyleUp.Models;

namespace ReStyleUp.Services.Interfaces
{
    public interface IArticleService
    {
        public IEnumerable<ArticleReadDto> GetAllArticles();
        public ArticleReadDto GetArticleById(int id);
        public IEnumerable<ArticleReadDto> GetArticlesByAnnonceId(int annonceId);
        public void AddArticle(ArticleCreateDto article);
        public void UpdateArticle(int id, ArticleUpdateDto articleUpdateDto);  
        public void DeleteArticle(int id);
    }
}
