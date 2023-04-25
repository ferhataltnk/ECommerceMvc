using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IReviewService
    {
        public List<Review> GetReviewsByProductId(int productId);
        public  Task AddReview(Review review);
    }
}
