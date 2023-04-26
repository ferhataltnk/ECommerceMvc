using Business.Services.Abstract;
using Core.Utilities.Results;
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
        
        public Result<List<Review>> GetReviewsByProductId(int productId)
        {
            try
            {
                var result = _reviewDal.GetReviewsByProductId(productId);
                return result.Success ?
                    new Result<List<Review>>(data: result.Data, message: "Ürün yorumları başarıyla getirildi.", success: true) :
                    new Result<List<Review>>(data: result.Data, message: result.Message, success: true);


            }
            catch (Exception exception)
            {
                return new Result<List<Review>>(data: null, message: $"Ürün yorumları getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
            }

        }


        public async Task<Result<string>> AddReview(Review review)
        {
            try
            {
                await _reviewDal.AddReviewAsync(review);
                return new Result<string>(data: null, message: "Yorum başarıyla eklendi.", success: true);

            }
            catch (Exception exception)
            {
                return new Result<string>(data: null, message: $"Yorum eklenirken beklenmedik bir hata oluştu.{Environment.NewLine} Detay: {exception.Message}", success: false);
            }

        }
    }
}
