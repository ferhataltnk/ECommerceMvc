using Business.Services.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
           
            return View();
        }
    }
}
