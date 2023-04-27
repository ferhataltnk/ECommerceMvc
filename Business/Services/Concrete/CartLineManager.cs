using Business.Services.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Core.Utilities.Results;
using Serilog;

namespace Business.Services.Concrete
{
    public class CartLineManager : ICartLineService
    {
        ILogger _logger;

        public CartLineManager(ILogger logger)
        {
            _logger = logger;
        }

        private List<CartLine> _cartLines = new List<CartLine>();
        public List<CartLine> CartLines
        {
            get { return _cartLines; }
        }


        public async Task<Result<bool>> AddProduct(Product product, int quantity = 1)
        {
            try
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
                _logger.Information("Ürünler sepete başarıyla eklendi.");
                return new Result<bool>(data: true, message: "Ürün sepete başarıyla eklendi.", success: true);
            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(AddProduct)} Get Error Message: {exception.Message}");
                return new Result<bool>(data: false, message: $"Ürün sepete eklenirken beklenmedik bir hata ile karşılaşıldı.{Environment.NewLine} Detay: {exception.Message}", success: false);
            }

        }

        public async Task<Result<bool>> DeleteProduct(Product product)
        {
            try
            {
                _cartLines.RemoveAll(i => i.Product.ProductId == product.ProductId);
                _logger.Information("Ürün sepetten başarıyla silindi..");
                return new Result<bool>(data: true, message: "Ürün sepetten başarıyla silindi..", success: true);
            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(DeleteProduct)} Get Error Message: {exception.Message}");
                return new Result<bool>(data: false, message: $"Sepetten ürün silinirken beklenmedik bir hata ile karşılaşıldı.{Environment.NewLine} Detay: {exception.Message}", success: false);
            }

        }

        public Result<double> TotalPrice()
        {
            try
            {
                var totalPrice = _cartLines.Sum(i => i.Product.Price * i.Quantity);
                _logger.Information("Toplam fiyat başarıyla hesaplandı.");
                return new Result<double>(data: totalPrice, message: "Toplam fiyat başarıyla hesaplandı.", success: true);
            }
                 catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(DeleteProduct)} Get Error Message: {exception.Message}");
                return new Result<double>(data: 0, message: $"Toplam sepet tutarı hesaplanırken beklenmedik bir hata oluştu.{Environment.NewLine} Detay: {exception.Message}", success: false);
            }

        }

    


        public async Task<Result<bool>> Clear()
        {
            try
            {
                _cartLines.Clear();
                _logger.Information("Sepet başarıyla temizlendi.");
                return new Result<bool>(data: true, message: "Sepet başarıyla temizlendi.", success: true);
            }
            catch (Exception exception)
            {
                _logger.Error($"{GetType().FullName}.{nameof(AddProduct)} Get Error Message: {exception.Message}");
                return new Result<bool>(data: false, message: $"Sepet temizlenirken bir hata oluştu.{Environment.NewLine} Detay: {exception.Message}", success: false);
            }
        }



    }
}
