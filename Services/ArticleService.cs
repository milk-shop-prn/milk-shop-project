using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;

        public ArticleService(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public void DeleteArticle(Article article)
            => articleRepository.DeleteArticle(article);

        public List<Article> GetAllArticles()
            => articleRepository.GetAllArticles();

        public Article GetArticleById(int articleId)
            => articleRepository.GetArticleById(articleId);

        public void SaveArticle(Article article)
            => articleRepository.SaveArticle(article);

        public void UpdateArticle(Article article)
            => articleRepository.UpdateArticle(article);
    }
}
