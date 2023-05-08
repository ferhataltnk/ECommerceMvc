using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IOrderService
    {
        public Result<bool> InsertOrderAndDetails();
    }
}
