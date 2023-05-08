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
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly ILogger _logger;
        private readonly ISubCategoryDal _subCategoryDal;
        public SubCategoryManager(ISubCategoryDal subCategoryDal, ILogger logger)
        {
            _subCategoryDal = subCategoryDal;
            _logger = logger;
        }
        public Result<List<string>> GetSubCategoriesByCategoryId(int categoryId)
        {
            try
            {
                var result = _subCategoryDal.GetSubCategoriesByCategoryId(categoryId);
                _logger.Information(result.Message);
                return result.Success ?
                    new Result<List<string>>(data: result.Data, message: "Alt kategori isimleri başarıyla getirildi.", success: true) :
                    new Result<List<string>>(data: result.Data, message: result.Message, success: true);


            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(GetSubCategoriesByCategoryId)} Get Error Message: {exception.Message}");
                return new Result<List<string>>(data: null, message: $"Alt kategori isimleri getirilirken bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
            }
        }
    }
}
