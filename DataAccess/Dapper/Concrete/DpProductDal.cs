using Core.Utilities.Results;
using Dapper;
using DataAccess.Dapper.Abstract;
using DataAccess.Dapper.Constants;
using Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DataAccess.Dapper.Concrete
{
    public class DpProductDal:IProductDal
    {
        private readonly IConfiguration _configuration;

        public DpProductDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Result<List<Product>> GetProducts()
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
                {
                    connection.Open();

                    var products = connection.Query<Product>(
                        Query.QUERY_PRODUCTS_GET_ALL_PRODUCTS
                        );

                    connection.Close();
                    return new Result<List<Product>>(data: products.ToList(), message: "Ürünler başarıyla getirildi.", success: false);
                }
            }
            catch (Exception exception)
            {
                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken bir hata oluştu.{Environment.NewLine}Detay:{exception.Message}", success: false);
            }

        }
    }
}