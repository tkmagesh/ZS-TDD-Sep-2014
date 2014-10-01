using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POSSystem
{
    public class SaleRegister : ISaleEventListener
    {
        private readonly IProductService _productService;
        public decimal Total { get; private set; }

        public SaleRegister(IProductService productService)
        {
            _productService = productService;
        }

        public void NewSaleInitialized()
        {
            Total = 0;
        }

        public void EndSale()
        {
            throw new NotImplementedException();
        }

        public void AddProduct(string barCode, int quantity)
        {
            var manufacturerCode = barCode.Substring(0, 7);
            var productCode = barCode.Substring(7);

            var product = _productService.GetProductInfo(manufacturerCode, productCode);
            Total += product.UnitCost*quantity;
        }
    }
}
