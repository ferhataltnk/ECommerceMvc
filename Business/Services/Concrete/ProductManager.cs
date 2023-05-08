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
    public class ProductManager : IProductService
    {
        private readonly ILogger _logger;
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal, ILogger logger)
        {
            _productDal = productDal;
            _logger = logger;
        }

        public async Task<Result<List<Product>>> GetProductsBySearchFilterAsync(SearchFilterModel searchFilterModel)
        {
            try
            {
                if (searchFilterModel.MaxPrice == null && searchFilterModel.Colors == null && searchFilterModel.Brands == null && searchFilterModel.SubCategories == null && searchFilterModel.MinPrice == null)
                {
                    return await GetProductsByCategoryIdAsync(searchFilterModel.CategoryId);
                }
                else if(searchFilterModel.MinPrice !=null && searchFilterModel.Colors!=null && searchFilterModel.Brands!=null && searchFilterModel.SubCategories != null && searchFilterModel.MaxPrice!=null && searchFilterModel.CategoryId != null)
                {
                    return await GetProductsBySearchFilter(searchFilterModel);
                }
                else if (searchFilterModel.MinPrice == null && searchFilterModel.Colors == null && searchFilterModel.Brands == null && searchFilterModel.SubCategories == null && searchFilterModel.MaxPrice == null && searchFilterModel.CategoryId != null)
                {
                    return await GetProductsByCategoryIdAsync(searchFilterModel.CategoryId);
                }
                return await GetProductsBySearchFilter(searchFilterModel);            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(GetProductsBySearchFilterAsync)} Get Error Message: {exception.Message}");
                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
            }

        }




        public async Task<Result<List<Product>>> GetProductById(int id)
        {
            try
            {
                var result = _productDal.GetProductById(id);
                _logger.Information("Ürünler başarıyla getirildi");
                return result.Success ?
                    new Result<List<Product>>(data: result.Data, message: "Ürünler başarıyla getirildi.", success: true) :
                    new Result<List<Product>>(data: new List<Product>(), message: result.Message, success: false);
            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(GetProductById)} Get Error Message: {exception.Message}");
                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
            }
        }










        //private async Task<Result<List<Product>>> GetProductsAsync()
        //{
        //    try
        //    {
        //        var result = _productDal.GetProducts();
        //        _logger.Information("Ürünler başarıyla getirildi");
        //        return result.Success ?
        //            new Result<List<Product>>(data: result.Data, message: "Ürünler başarıyla getirildi.",success:true) :
        //            new Result<List<Product>>(data: new List<Product>(), message: result.Message,success:false);
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.Error($"{GetType().FullName}.{nameof(GetProductsAsync)} Get Error Message: {exception.Message}");
        //        return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
        //    }          
        //}

        private async Task<Result<List<Product>>> GetProductsByCategoryIdAsync(int categoryId)
        {
            try
            {
                var result = _productDal.GetProductsByCategoryId(categoryId);
                _logger.Information("Ürünler başarıyla getirildi");
                return result.Success ?
                    new Result<List<Product>>(data: result.Data, message: "Ürünler başarıyla getirildi.", success: true) :
                    new Result<List<Product>>(data: new List<Product>(), message: result.Message, success: false);
            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(GetProductsByCategoryIdAsync)} Get Error Message: {exception.Message}");
                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
            }
        }


        private async Task<Result<List<Product>>> GetProductsBySearchFilter(SearchFilterModel searchFilterModel)
        {
            try
            {
                var result = _productDal.GetProductsBySearchFilter(searchFilterModel);
                _logger.Information("Ürünler başarıyla getirildi");
                return result.Success ?
                    new Result<List<Product>>(data: result.Data, message: "Ürünler başarıyla getirildi.", success: true) :
                    new Result<List<Product>>(data: new List<Product>(), message: result.Message, success: false);
            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(GetProductsBySearchFilter)} Get Error Message: {exception.Message}");

                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
            }
        }



        //public Result<List<Product>> GetProducts()
        //{
        //    try
        //    {
        //        var result = _productDal.GetProducts();
        //        _logger.Information("Ürünler başarıyla getirildi");
        //        return result.Success ?
        //            new Result<List<Product>>(data: result.Data, message: "Ürünler başarıyla getirildi.", success: true) :
        //            new Result<List<Product>>(data: new List<Product>(), message: result.Message, success: false);
        //    }
        //    catch (Exception exception)
        //    {
        //        _logger.Error($"{GetType().FullName}.{nameof(GetProducts)} Get Error Message: {exception.Message}");
        //        return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
        //    }
        //}

    }
}
