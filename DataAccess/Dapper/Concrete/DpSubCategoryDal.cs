using Core.Utilities.Results;
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
    public class DpSubCategoryDal : ISubCategoryDal
    {
        private readonly IConfiguration _configuration;

        public DpSubCategoryDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Result<List<string>> GetSubCategoriesByCategoryId(int categoryId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
                {
                    connection.Open();

                    IEnumerable<string> subCategories = connection.Query<string>(
                        Query.QUERY_SUBCATEGORIES_GET_SUBCATEGORIES_BY_CATEGORYID,
                        param: new
                        {
                            categoryId = categoryId
                        }
                        );

                    connection.Close();
                    return new Result<List<string>>(data: subCategories.ToList(), message: "Alt kategori isimleri başarıyla getirildi.", success: false);
                }
            }
            catch (Exception exception)
            {
                return new Result<List<string>>(data: null, message: $"Alt kategori isimleri getirilirken bir hata oluştu.{Environment.NewLine}Detay:{exception.Message}", success: false);
            }

        }

    }
}
