using Business.Services.Abstract;
using Core.Utilities.Results;
using DataAccess.Dapper.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly ICartLineService _cartLineService;

        public OrderManager(ICartLineService cartLineService)
        {
            _cartLineService = cartLineService;
        }

        public Result<bool> InsertOrderAndDetails()
        {
            throw new NotImplementedException();
        }

        //     public Result<bool> InsertOrderAndDetails()
        //     {
        //try
        //{
        //             List<CartLine> cartLines = _cartLineService.CartLines;

        //             foreach (var item in cartLines)
        //             {
        //                 item.Product.
        //             }

        //         }
        //catch (Exception exception)
        //{

        //	throw;
        //}}
    }
}

