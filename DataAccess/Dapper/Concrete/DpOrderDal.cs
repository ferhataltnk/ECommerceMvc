using Core.Utilities.Results;
using Dapper;
using DataAccess.Dapper.Abstract;
using Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper.Concrete
{
    public class DpOrderDal : IOrderDal
    {
        private readonly IConfiguration _configuration;

        public DpOrderDal(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Result<bool> InsertOrderAndDetails(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("flyerConnectionString")))
                {
                    connection.Open();

                    using (var transaction = connection.BeginTransaction())
                    {

                        // Order tablosuna insert yapma
                        string insertOrderQuery = @"
                    INSERT INTO Orders (CustomerId, OrderDate, RequiredDate, ShippedDate, ShipAddress, ShipCity, ShipCountry, IsActive, IsDeleted, CreatedTime, CreatedUserId, ModifiedTime, ModifiedUserId, DeletedTime, DeletedUserId)
                    VALUES (@CustomerId, @OrderDate, @RequiredDate, @ShippedDate, @ShipAddress, @ShipCity, @ShipCountry, @IsActive, @IsDeleted, @CreatedTime, @CreatedUserId, @ModifiedTime, @ModifiedUserId, @DeletedTime, @DeletedUserId);
                    SELECT CAST(SCOPE_IDENTITY() AS int);";

                        int orderId = connection.QuerySingle<int>(insertOrderQuery, order, transaction);

                      
                        string insertOrderDetailQuery = @"
                    INSERT INTO OrderDetails (OrderId, ProductId, UnitPrice, Quantity)
                    VALUES (@OrderId, @ProductId, @UnitPrice, @Quantity);";

                        foreach (var detail in orderDetails)
                        {
                            detail.OrderId = orderId;
                            connection.Execute(insertOrderDetailQuery, detail, transaction);
                        }

                        transaction.Commit();

                        connection.Close();

                        return new Result<bool>(data: true, success: true, message: "Siparişler veritabanına başarıyla kaydedildi.");

                    }
                }
            }
            catch (Exception exception)
            {
                return new Result<bool>(data: false, success: false, message: $"Siparişler kaydedilirken beklenmedik bir hata oluştu.\n Detay:{exception.Message}");
            }
        }
    }
}
