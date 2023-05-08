using Business.Services.Abstract;
using ECommerceMvc.Models;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IReviewService _reviewService;
        private readonly ISubCategoryService _subCategoryService;

        public ProductController(IProductService productService, IReviewService reviewService, ISubCategoryService subCategoryService)
        {
            _productService = productService;
            _reviewService = reviewService;
            _subCategoryService = subCategoryService;
        }

        public IActionResult Index(SearchFilterModel searchFilterModel)
        {
            ProductViewModel productViewModel = new ProductViewModel();

        
            var filteredValue = _productService.GetProductsBySearchFilterAsync(searchFilterModel);
            productViewModel.productListModel = filteredValue.Result.Data;
            productViewModel.subCategoryModel= _subCategoryService.GetSubCategoriesByCategoryId(searchFilterModel.CategoryId).Data;


            return View(productViewModel);
        }


        public IActionResult Details(int id)
        {
            var result = _productService.GetProductById(id).Result.Data;
            var reviews = _reviewService.GetReviewsByProductId(id);
            ViewData["Review"] = reviews.Data.ToList();
            return View(result.FirstOrDefault());
        }


        public IActionResult AddReview (Review review)
        {   
             _reviewService.AddReview(review);
            return View("/Views/Shared/ProductDetailPartial/CommentSuccessPartial.cshtml");
        }
    }
}
