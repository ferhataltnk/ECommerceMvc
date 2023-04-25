using Business.Services.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace ECommerceMvc.ViewComponents.CommentForm
{
    public class CommentFormViewComponent : ViewComponent
    {
       
        public IViewComponentResult Invoke(int productId)
        {
            ViewBag.ProductId = productId;
            Review review =new Review();
            return View(review);
        }
       
    }
}
