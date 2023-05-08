using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dapper.Abstract
{
    public interface ISubCategoryDal
    {
        public Result<List<string>> GetSubCategoriesByCategoryId(int categoryId);
    }
}
