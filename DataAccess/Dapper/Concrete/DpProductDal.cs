using Dapper;
using DataAccess.Dapper.Abstract;
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
                    "SELECT * FROM PRODUCTS"
                    );

                connection.Close();
                return products.ToList();
            }
        }
    }
}