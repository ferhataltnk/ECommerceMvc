using Core.Utilities.Results;
using Entities;

namespace Business.Services.Abstract
{
    public interface ICartLineService
    {
        public Task<Result<bool>> AddProduct(Product product, int quantity = 1);
        public Task<Result<bool>> DeleteProduct(Product product);
        public Task<Result<bool>> Clear();
        public Result<double> TotalPrice();
    }
}
