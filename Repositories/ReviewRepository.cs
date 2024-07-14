using BusinessObjects.Models;
using DataAccessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public void DeleteReview(Review review)
            => ReviewDAO.Instance.DeleteReview(review);

        public List<Review> GetAllReviews()
            => ReviewDAO.Instance.GetAllReviews();

        public Review GetReviewById(int reviewId)
            => ReviewDAO.Instance.GetReviewById(reviewId);

        public void SaveReview(Review review)
            => ReviewDAO.Instance.SaveReview(review);

        public void UpdateReview(Review review)
            => ReviewDAO.Instance.UpdateReview(review);
    }
}
