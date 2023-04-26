using Core.Utilities.Results;
using Dapper;
using DataAccess.Dapper.Abstract;
using DataAccess.Dapper.Constants;
using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
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
        public Result<List<Review>> GetReviewsByProductId(int productId)
        {
            try
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
                    return new Result<List<Review>>(data: reviews.ToList(), message: "Ürüne ait yorumlar başarıyla getirildi.", success: false);
                }
            }
            catch (Exception exception)
            {
                return new Result<List<Review>>(data: null, message: $"Ürüne ait yorumlar getirilirken bir hata oluştu.{Environment.NewLine}Detay:{exception.Message}", success: false);
            }

        }


        
        public async Task<Result<string>> AddReviewAsync(Review review)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
                {
                    connection.Open();
                     await connection.ExecuteReaderAsync(Query.QUERY_REVIEWS_INSERT_INTO_REVIEW,
                                    param: new
                                    {
                                        ProductId = review.ProductId,
                                        UserId = review.UserId,
                                        Comment = review.Comment,
                                        Rating = review.Rating
                                    });
                    connection.Close();
                    return new Result<string>(data: null, message:"Yorum başarıyla eklendi.", success: true);
                }
            }
            catch (Exception exception)
            {
                return new Result<string>(data: null, message: $"Yorum eklenirken beklenmedik bir hata oluştu.{Environment.NewLine} Detay: {exception.Message}", success: false); 
            }
           
         
        }
    }
}
