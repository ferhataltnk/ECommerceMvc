using Core.Utilities.Results;
using Dapper;
using DataAccess.Dapper.Abstract;
using DataAccess.Dapper.Constants;
using Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Dapper.Concrete
{
    public class DpProductDal : IProductDal
    {
        private readonly IConfiguration _configuration;

        public DpProductDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public Result<List<Product>> GetProductById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
                {
                    connection.Open();
                    var products = connection.Query<Product>(
                        Query.QUERY_PRODUCTS_GET_BY_PRODUCTID,
                        param: new { productId = id }
                        );
                    connection.Close();
                    return new Result<List<Product>>(data: products.ToList(), message: "Ürünler başarıyla getirildi.", success: true);
                }
            }
            catch (Exception exception)
            {
                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken bir hata oluştu.{Environment.NewLine}Detay:{exception.Message}", success: false);
            }

        }


        public Result<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
                {
                    connection.Open();

                    var products = connection.Query<Product>(
                        Query.QUERY_PRODUCTS_GET_PRODUCTS_BY_CATEGORYID,
                        param: new { categoryId = categoryId }
                        );

                    connection.Close();
                    return new Result<List<Product>>(data: products.ToList(), message: "Ürünler başarıyla getirildi.", success: true);
                }
            }
            catch (Exception exception)
            {
                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken bir hata oluştu.{Environment.NewLine}Detay:{exception.Message}", success: false);
            }

        }



        public Result<List<Product>> GetProductsBySearchFilter(SearchFilterModel searchFilterModel)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
                {
                    connection.Open();
                    StringBuilder brandsStringBuilder = new StringBuilder();
                    StringBuilder colorsStringBuilder = new StringBuilder();
                    StringBuilder subCategoriesStringBuilder = new StringBuilder();

                    if (searchFilterModel.Brands != null)
                    {
                        foreach (var item in searchFilterModel.Brands)
                        {
                            brandsStringBuilder.Append(item);
                            brandsStringBuilder.Append(",");
                        }
                    }
                    else
                    {
                        var brands = connection.Query<string>(
                        sql: @"
                        Select BrandName 
                        From Brands B
                        INNER JOIN BrandOfSubCategories Bs
	                        ON B.BrandId=Bs.BrandId
                        INNER JOIN SubCategoriesOfCategories Sc
	                        ON Bs.SubCategoryId=Sc.SubCategoryId
                        WHERE Sc.CategoryId = @categoryId
                        ",
                        param: new
                        {
                            categoryId = searchFilterModel.CategoryId
                        }
                     );
                        foreach (var item in brands)
                        {
                            brandsStringBuilder.Append(item);
                            brandsStringBuilder.Append(",");
                        }
                    }


                    if (searchFilterModel.Colors != null)
                    {
                        foreach (var item in searchFilterModel.Colors)
                        {
                            colorsStringBuilder.Append(item);
                            colorsStringBuilder.Append(",");
                        }
                    }
                    else
                    {
                        var colors = connection.Query<string>(
                        sql: @"
                        Select ColorName 
                        From Colors 
                        ");

                        foreach (var item in colors)
                        {
                            colorsStringBuilder.Append(item);
                            colorsStringBuilder.Append(",");
                        }

                    }

                    if (searchFilterModel.SubCategories != null)
                    {
                        foreach (var item in searchFilterModel.SubCategories)
                        {
                            subCategoriesStringBuilder.Append(item);
                            subCategoriesStringBuilder.Append(",");
                        }
                    }
                    else
                    {
                        var subCategories = connection.Query<string>(
                            sql: @"
                            SELECT SubCategoryName
                            FROM SubCategories Sc
                            INNER JOIN SubCategoriesOfCategories SOC
                                ON Sc.SubCategoryId = SOC.SubCategoryId
                            INNER JOIN Categories C
                                ON C.CategoryId = SOC.CategoryId
                            WHERE C.CategoryId = @categoryId
                            ",
                            param: new
                            {
                                categoryId=searchFilterModel.CategoryId
                            }
                            );

                        foreach (var item in subCategories)
                        {
                            subCategoriesStringBuilder.Append(item);
                            subCategoriesStringBuilder.Append(",");
                        }

                    }

                    


                    var products = connection.Query<Product>(
                        Query.QUERY_PRODUCTS_GET_PRODUCTS_BY_SEARCHFILTER,
                        param: new
                        {
                            minPrice = searchFilterModel.MinPrice != null ? searchFilterModel.MinPrice : 0,
                            maxPrice = searchFilterModel.MaxPrice != null ? searchFilterModel.MaxPrice : 1000000,
                            brand = brandsStringBuilder.ToString(),
                            color = colorsStringBuilder.ToString(),
                            categoryId = searchFilterModel.CategoryId,
                            subCategories = subCategoriesStringBuilder.ToString(),
                        });

                    connection.Close();
                    return new Result<List<Product>>(data: products.ToList(), message: "Ürünler başarıyla getirildi.", success: true);
                }
            }
            catch (Exception exception)
            {
                return new Result<List<Product>>(data: null, message: $"Ürünler getirilirken bir hata oluştu.{Environment.NewLine}Detay:{exception.Message}", success: false);
            }

        }

    }
}