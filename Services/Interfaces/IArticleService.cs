using ReStyleUp.Models;
namespace ReStyleUp.Services.Interfaces
{
    public interface IArticleService
    {
        IEnumerable<Article> GetAllArticles();
        Article GetArticleById(int id);
        Article GetArticleByName(string name);
        void AddArticle(Article article);
        void UpdateArticle(Article article);
        void DeleteArticle(int id);
    }
}
