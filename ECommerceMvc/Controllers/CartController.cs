using Business.Services.Abstract;
using ECommerceMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace ECommerceMvc.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMemoryCache _cache;
        private const string CartKey = "Cart";

        
        public CartController(IProductService productService, IMemoryCache cache)
        {
            _productService = productService;
            _cache = cache;
        }
        public IActionResult Index()
        {
            return View(GetCart());
        }
        
        public IActionResult AddToCart(int ProductId,int quantity)
        {
            var product = _productService.GetProducts().FirstOrDefault(i=>i.ProductId== ProductId);

            if(product != null)
            {
                GetCart().AddProduct(product,quantity);
            }
           
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var product = _productService.GetProducts().FirstOrDefault(i => i.ProductId == productId);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }



        public ActionResult ClearCart()
        {
            GetCart().Clear();
            return RedirectToAction("Index");
        }



        //[NonAction]
        //public Cart GetCart()
        //{
        //    byte[] cartData = HttpContext.Session.Get("Cart");

        //    if (cartData == null)
        //    {
        //        Cart cart = new Cart();

        //        HttpContext.Session.Set("Cart", JsonSerializer.SerializeToUtf8Bytes(cart));

        //        return cart;
        //    }

        //    string json = Encoding.UTF8.GetString(cartData);
        //    Cart existingCart = JsonSerializer.Deserialize<Cart>(json);
        //    return existingCart;
        //}



            [NonAction]
            public Cart GetCart()
            {
                if (_cache.TryGetValue(CartKey, out Cart existingCart))
                {
                    return existingCart;
                }

                var cart = new Cart();
                _cache.Set(CartKey, cart);
                return cart;
            }
     









    }
}
