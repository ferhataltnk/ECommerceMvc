using Business.Services.Abstract;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class CartManager :ICartService
    {

        private const string CartKey = "Cart";
        private readonly IMemoryCache _cache;

        public CartManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public CartLineManager GetCart()
        {

            if (_cache.TryGetValue(CartKey, out CartLineManager existingCart))
            {
                return existingCart;
            }
            var cart = new CartLineManager();
            _cache.Set(CartKey, cart);
            return cart;
        }
    }
}
