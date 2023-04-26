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
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public Result<List<Product>> GetProducts()
        {
            try
            {
                var result = _productDal.GetProducts();
                return result.Success ?
                    new Result<List<Product>>(data: result.Data, message: "Ürünler başarıyla getirildi.",success:true) :
                    new Result<List<Product>>(data: result.Data, message: result.Message,success:false);
            }
            catch (Exception exception)
            {
                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken beklenmedik bir hata oluştu. {Environment.NewLine} Detay: {exception.Message}", success: false);
            }
           
        }
    }
}
