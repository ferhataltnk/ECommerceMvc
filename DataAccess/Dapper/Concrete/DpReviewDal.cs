using Dapper;
using DataAccess.Dapper.Abstract;
using DataAccess.Dapper.Constants;
using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper.Concrete
{
    public class DpReviewDal : IReviewDal
    {
        private readonly IConfiguration _configuration;

        public DpReviewDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<Review> GetReviewsByProductId(int productId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
            {
                connection.Open();

                var reviews = connection.Query<Review>(
                    Query.QUERY_REVIEWS_GET_REVIEW_BY_PRODUCTID,
                    param: new
                    {
                        productId = productId
                    }
                    );

                connection.Close();
                return reviews.ToList();
            }

        }


        //Method ismine Async eklenecek!!!
        public async Task AddReview(Review review)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
            {
                connection.Open();

                connection.ExecuteReader(Query.QUERY_REVIEWS_INSERT_INTO_REVIEW,
                    param:new {ProductId=review.ProductId,
                               UserId=review.UserId,
                               Comment=review.Comment,
                               Rating=review.Rating
                                });

                connection.Close();

            }
        }
    }
}
