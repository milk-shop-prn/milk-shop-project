using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IArticleService
    {
        Article GetArticleById(int articleId);

        List<Article> GetAllArticles();

        void SaveArticle(Article article);

        void UpdateArticle(Article article);

        void DeleteArticle(Article article);
    }
}
