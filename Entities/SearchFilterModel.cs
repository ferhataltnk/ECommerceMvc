using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class SearchFilterModel
    {
        public int CategoryId { get; set; }
        public List<string>? SubCategories { get; set; }
        public List<string>? Brands { get; set; }
        public List<string>? Colors { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }


    }
}
