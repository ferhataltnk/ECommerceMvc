using Business.Services.Concrete;
using Core.Utilities.Results;

namespace Business.Services.Abstract
{
    public interface ICartService
    {
        public Result<CartLineManager> GetCart();
    }
}