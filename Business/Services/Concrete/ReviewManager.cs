using Business.Services.Abstract;
using DataAccess.Dapper.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class ReviewManager : IReviewService
    {
        private readonly IReviewDal _reviewDal;
        public ReviewManager(IReviewDal reviewDal)
        {
            _reviewDal = reviewDal;
        }
        
        public List<Review> GetReviewsByProductId(int productId)
        {
            return _reviewDal.GetReviewsByProductId(productId);
        }

        public async Task AddReview(Review review)
        {
            _reviewDal.AddReview(review);
        }
    }
}
