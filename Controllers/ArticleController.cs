using Microsoft.AspNetCore.Mvc;
using ReStyleUp.DTOs.Article;
using ReStyleUp.Services;
using ReStyleUp.Services.Interfaces;

namespace ReStyleUp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        // GET: api/Articles
        [HttpGet]
        public ActionResult<IEnumerable<ArticleReadDto>> GetAllArticles()
        {
            var articles = _articleService.GetAllArticles();
            return Ok(articles);
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public ActionResult<ArticleReadDto> GetArticleById(int id)
        {
            var article = _articleService.GetArticleById(id);
            if (article == null)
                return NotFound();

            return Ok(article);
        }

        // GET: api/Articles/annonce/3
        [HttpGet("annonce/{annonceId}")]
        public ActionResult<IEnumerable<ArticleReadDto>> GetArticlesByAnnonceId(int annonceId)
        {
            var articles = _articleService.GetArticlesByAnnonceId(annonceId);
            return Ok(articles);
        }

        // POST: api/Articles
        [HttpPost]
        public IActionResult AddArticle([FromBody] ArticleCreateDto articleCreateDto)
        {
            _articleService.AddArticle(articleCreateDto);
            return Ok(new { message = "Article ajouté avec succès." });
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public IActionResult UpdateArticle(int id, [FromBody] ArticleUpdateDto articleUpdateDto)
        {
            _articleService.UpdateArticle(id, articleUpdateDto);
            var updatedArticle = _articleService.GetArticleById(id);
            return Ok(updatedArticle);
            
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public IActionResult DeleteArticle(int id)
        {
            _articleService.DeleteArticle(id);
            return Ok(new { message = "Article supprimé avec succès"});
        }
    }
}
