using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        public void DeleteArticle(Article article)
            => ArticleDAO.Instance.DeleteArticle(article);

        public List<Article> GetAllArticles()
            => ArticleDAO.Instance.GetAllArticles();

        public Article GetArticleById(int articleId)
            => ArticleDAO.Instance.GetArticleById(articleId);

        public void SaveArticle(Article article)
            => ArticleDAO.Instance.SaveArticle(article);

        public void UpdateArticle(Article article)
            => ArticleDAO.Instance.UpdateArticle(article);
    }
}
