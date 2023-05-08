using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper.Abstract
{
    public interface IOrderDal
    {
        public Result<bool> InsertOrderAndDetails(Order order, List<OrderDetail> orderDetails);
    }
}
