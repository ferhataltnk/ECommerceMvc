using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper.Abstract
{
    public interface IProductDal
    {
        public Result<List<Product>> GetProducts();
    }
}
