using Entities;

namespace ECommerceMvc.Models
{
    public class ProductViewModel
    {
        public List<Product> productListModel { get; set; }
        public SearchFilterModel searchFilterModel { get; set; }
        public List<string> subCategoryModel { get; set; }

    }
}
