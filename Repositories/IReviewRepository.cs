﻿using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IReviewRepository
    {
        Review GetReviewById(int reviewId);

        List<Review> GetAllReviews();

        void SaveReview(Review review);

        void UpdateReview(Review review);

        void DeleteReview(Review review);
    }
}
