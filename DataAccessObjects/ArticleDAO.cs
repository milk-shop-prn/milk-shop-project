using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects
{
    public class ArticleDAO
    {
        private static ArticleDAO instance = null!;
        private static readonly object lockObject = new object();

        private ArticleDAO() { }

        public static ArticleDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ArticleDAO();
                    }
                    return instance;
                }
            }
        }

        public Article GetArticleById(int articleId)
        {
            using var db = new MilkShopContext();
            return db.Articles.SingleOrDefault(a => a.ArticleId == articleId);
        }

        public List<Article> GetAllArticles()
        {
            using var db = new MilkShopContext();
            return db.Articles.ToList();
        }

        public void SaveArticle(Article article)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Articles.Add(article);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateArticle(Article article)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry(article).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteArticle(Article article)
        {
            try
            {
                using var db = new MilkShopContext();
                var existingArticle = db.Articles.Find(article.ArticleId);
                if (existingArticle != null)
                {
                    db.Articles.Remove(existingArticle);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
