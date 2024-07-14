using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            this.reviewRepository = reviewRepository;
        }

        public void DeleteReview(Review review)
            => reviewRepository.DeleteReview(review);

        public List<Review> GetAllReviews()
            => reviewRepository.GetAllReviews();

        public Review GetReviewById(int reviewId)
            => reviewRepository.GetReviewById(reviewId);

        public void SaveReview(Review review)
            => reviewRepository.SaveReview(review);

        public void UpdateReview(Review review)
            => reviewRepository.UpdateReview(review);
    }
}
