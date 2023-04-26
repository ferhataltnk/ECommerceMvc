using Business.Services.Concrete;

namespace Business.Services.Abstract
{
    public interface ICartService
    {
        public CartLineManager GetCart();
    }
}