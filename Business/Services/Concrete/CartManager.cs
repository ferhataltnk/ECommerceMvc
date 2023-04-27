using Business.Services.Abstract;
using Core.Utilities.Results;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
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
        private readonly ILogger _logger;


        public CartManager(IMemoryCache cache, ILogger logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public Result<CartLineManager> GetCart()
        {
            try
            {
                if (_cache.TryGetValue(CartKey, out CartLineManager existingCart))
                {
                    //return existingCart;
                    _logger.Information("Sepet önbellekten başarıyla getirildi.");
                    return new Result<CartLineManager>(data:existingCart,message:"Sepet önbellekten başarıyla getirildi.",success:true);
                }
                var cart = new CartLineManager(_logger);
                _cache.Set(CartKey, cart);
                _logger.Information("Sepet oluşturularak başarıyla getirildi.");
                return new Result<CartLineManager>(data:cart, message: "Sepet oluşturularak başarıyla getirildi.", success: true);
            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(GetCart)} Get Error Message: {exception.Message}");
                return new Result<CartLineManager>(data: null, message: exception.Message, success: false);
            }

            
        }
    }
}
