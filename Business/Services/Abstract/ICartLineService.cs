using Entities;

namespace Business.Services.Abstract
{
    public interface ICartLineService
    {
        public void AddProduct(Product product, int quantity = 1);
        public void DeleteProduct(Product product);
        public void Clear();

    }
}
