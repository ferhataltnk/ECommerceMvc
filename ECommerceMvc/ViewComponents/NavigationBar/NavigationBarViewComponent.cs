using Business.Services.Abstract;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace ECommerceMvc.ViewComponents.NavigationBar
{
    public class NavigationBarViewComponent : ViewComponent
    {
        private readonly ISubCategoryService _subCategoryService;

        public NavigationBarViewComponent(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        public IViewComponentResult Invoke()
        {
            SubCategoryModel subCategoryModel = new SubCategoryModel();

            subCategoryModel.ManCategories = _subCategoryService.GetSubCategoriesByCategoryId(2).Data;
            subCategoryModel.WomanCategories = _subCategoryService.GetSubCategoriesByCategoryId(1).Data;
            subCategoryModel.ElectronicCategories = _subCategoryService.GetSubCategoriesByCategoryId(3).Data;

            return View(subCategoryModel);
        }
    }
}
