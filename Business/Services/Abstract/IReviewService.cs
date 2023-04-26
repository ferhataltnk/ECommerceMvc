using Core.Utilities.Results;
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
        public Result<List<Review>> GetReviewsByProductId(int productId);
        public  Task<Result<string>> AddReview(Review review);
    }
}
