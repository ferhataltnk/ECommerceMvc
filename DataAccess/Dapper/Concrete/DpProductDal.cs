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

        public List<Product> GetProducts()
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
            {
                connection.Open();

                var products = connection.Query<Product>(
                    Query.QUERY_PRODUCTS_GET_ALL_PRODUCTS
                    );

                connection.Close();
                return products.ToList();
            }
        }
    }
}