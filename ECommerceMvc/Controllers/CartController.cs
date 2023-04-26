using Business.Services.Abstract;
using ECommerceMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace ECommerceMvc.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;


        public CartController(IProductService productService, ICartService cartService)
        {
            _productService = productService;
            _cartService = cartService;
  
        }
        public IActionResult Index()
        {
            //return View(GetCart());
            return View(_cartService.GetCart());
        }
        
        public IActionResult AddToCart(int ProductId,int quantity)
        {
            var product = _productService.GetProducts().FirstOrDefault(i=>i.ProductId== ProductId);

            if(product != null)
            {
                _cartService.GetCart().AddProduct(product,quantity);
            }
           
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var product = _productService.GetProducts().FirstOrDefault(i => i.ProductId == productId);

            if (product != null)
            {
                _cartService.GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }



        public ActionResult ClearCart()
        {
            
            _cartService.GetCart().Clear();
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




    }
}
