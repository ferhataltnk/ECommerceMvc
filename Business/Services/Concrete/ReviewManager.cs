using Business.Services.Abstract;
using Core.Utilities.Results;
using DataAccess.Dapper.Abstract;
using Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    
    public class ReviewManager : IReviewService
    {
        private readonly ILogger _logger;
        private readonly IReviewDal _reviewDal;
        public ReviewManager(IReviewDal reviewDal, ILogger logger)
        {
            _reviewDal = reviewDal;
            _logger = logger;
        }

        public Result<List<Review>> GetReviewsByProductId(int productId)
        {
            try
            {
                var result = _reviewDal.GetReviewsByProductId(productId);
                _logger.Information(result.Message);
                return result.Success ?
                    new Result<List<Review>>(data: result.Data, message: "Ürün yorumları başarıyla getirildi.", success: true) :
                    new Result<List<Review>>(data: result.Data, message: result.Message, success: true);


            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(GetReviewsByProductId)} Get Error Message: {exception.Message}");
                return new Result<List<Review>>(data: null, message: $"Ürün yorumları getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
            }

        }


        public async Task<Result<string>> AddReview(Review review)
        {
            try
            {
                await _reviewDal.AddReviewAsync(review);
                _logger.Information("Yorum başarıyla eklendi.");
                return new Result<string>(data: null, message: "Yorum başarıyla eklendi.", success: true);

            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(AddReview)} Get Error Message: {exception.Message}");
                return new Result<string>(data: null, message: $"Yorum eklenirken beklenmedik bir hata oluştu.{Environment.NewLine} Detay: {exception.Message}", success: false);
            }

        }
    }
}
