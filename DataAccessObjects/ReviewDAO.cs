using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessObjects
{
    public class ReviewDAO
    {
        private static ReviewDAO instance = null!;
        private static readonly object lockObject = new object();

        private ReviewDAO() { }

        public static ReviewDAO Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ReviewDAO();
                    }
                    return instance;
                }
            }
        }

        public Review GetReviewById(int reviewId)
        {
            using var db = new MilkShopContext();
            return db.Reviews.SingleOrDefault(r => r.ReviewId == reviewId);
        }

        public List<Review> GetAllReviews()
        {
            using var db = new MilkShopContext();
            return db.Reviews.ToList();
        }

        public void SaveReview(Review review)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Reviews.Add(review);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateReview(Review review)
        {
            try
            {
                using var db = new MilkShopContext();
                db.Entry(review).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteReview(Review review)
        {
            try
            {
                using var db = new MilkShopContext();
                var existingReview = db.Reviews.Find(review.ReviewId);
                if (existingReview != null)
                {
                    db.Reviews.Remove(existingReview);
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
