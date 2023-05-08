using Business.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult InsertOrder()
        { 
            return RedirectToAction("Index", "Cart");
        }
    }
}
