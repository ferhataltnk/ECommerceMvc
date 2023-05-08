using Core.Utilities.Results;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IProductService
    {
       
        public Task<Result<List<Product>>> GetProductsBySearchFilterAsync(SearchFilterModel searchFilterModel);
        public Task<Result<List<Product>>> GetProductById(int id);
    }
}
