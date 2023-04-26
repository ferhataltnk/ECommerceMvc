using Business.Services.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Services.Concrete
{
    public class CartLineManager : ICartLineService
    {


        private List<CartLine> _cartLines = new List<CartLine>();
        public List<CartLine> CartLines
        {
            get { return _cartLines; }
        }
        

        public void AddProduct(Product product, int quantity = 1)
        {
            var line = _cartLines.FirstOrDefault(i => i.Product.ProductId == product.ProductId);
            if (line == null)
            {
                _cartLines.Add(new CartLine() { Product = product, Quantity = quantity });

            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void DeleteProduct(Product product)
        {
            _cartLines.RemoveAll(i => i.Product.ProductId == product.ProductId);
        }
        public double TotalPrice()
        {
            return _cartLines.Sum(i => i.Product.Price * i.Quantity);
        }

        public void Clear()
        {
            _cartLines.Clear();
        }


       
    }
}
