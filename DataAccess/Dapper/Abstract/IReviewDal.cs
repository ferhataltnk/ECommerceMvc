using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper.Abstract
{
    public interface IReviewDal
    {
        public Result<List<Review>> GetReviewsByProductId(int productId);
        public Task<Result<string>> AddReviewAsync(Review review);

    }
}
