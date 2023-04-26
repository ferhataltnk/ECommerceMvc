using Business.Services.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;

        public ProductController(IProductService productService, IReviewService reviewService)
        {
            _productService = productService;
            _reviewService = reviewService;
        }

        public IActionResult Details(int id)
        {
            var result = _productService.GetProducts().Data.Where(i=>i.ProductId== id).ToList();
            var reviews = _reviewService.GetReviewsByProductId(id);
            ViewData["Review"] = reviews.Data.ToList();
            return View(result.FirstOrDefault());
        }


        public IActionResult AddReview (Review review)
        {   
             _reviewService.AddReview(review);
           // return RedirectToAction("Details", "Product", review.ProductId);
            return View("/Views/Shared/ProductDetailPartial/CommentSuccessPartial.cshtml");


        }
    }
}
