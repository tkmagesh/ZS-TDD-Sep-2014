using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace POSSystem.Tests
{
    [TestClass]
    public class SaleRegisterTests
    {
        [TestMethod]
        public void Should_Reset_Sale_On_NewSale_Event()
        {
            var productServiceMockery = new Moq.Mock<IProductService>();
            var saleRegister = new SaleRegister(productServiceMockery.Object);

            saleRegister.NewSaleInitialized();

            Assert.AreEqual(0,saleRegister.Total);
        }

        [TestMethod]
        public void Should_Retreve_Product_Information_From_ProductService_When_A_New_Product_is_Added()
        {
            var productInfo = new Product { Code = "88560", Description = "Dummy Product", UnitCost = 10 };
            var productServiceMockery = new Moq.Mock<IProductService>();
            productServiceMockery.Setup(ps => ps.GetProductInfo("1000088", "88560")).Returns(productInfo);
            var saleRegister = new SaleRegister(productServiceMockery.Object);

            saleRegister.NewSaleInitialized();

            saleRegister.AddProduct("100008888560", 10);

            productServiceMockery.Verify(ps => ps.GetProductInfo("1000088", "88560"));

        }

        [TestMethod]
        public void Should_Update_Total_When_A_New_Product_Is_Added()
        {
            var productInfo = new Product{Code = "88560", Description = "Dummy Product", UnitCost = 10};
            var productServiceMockery = new Moq.Mock<IProductService>();
            productServiceMockery.Setup(ps => ps.GetProductInfo("1000088", "88560")).Returns(productInfo);
            var saleRegister = new SaleRegister(productServiceMockery.Object);
            saleRegister.NewSaleInitialized();

            saleRegister.AddProduct("100008888560", 10);

            Assert.AreEqual(100, saleRegister.Total);

        }
    }
}
