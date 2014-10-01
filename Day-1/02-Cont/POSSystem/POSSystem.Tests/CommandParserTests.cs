using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace POSSystem.Tests
{
    [TestClass]
    public class CommandParserTests
    {
        [TestMethod]
        public void NewSaleInitialize_Event_Triggered_When_NewSale_Command_Parsed()
        {
            //Arrange
            var mockery = new Moq.Mock<ISaleEventListener>();
            //mockery.Setup(se => se.NewSaleInitialized()).Verifiable();

            var commandParser = new CommandParser(mockery.Object);

            //Act
            commandParser.Parse("Command:NewSale");

            //Assert
            mockery.Verify(se => se.NewSaleInitialized(),Times.Once());
        }

        [TestMethod]
        public void EndSale_Event_Triggered_When_EndSale_Command_Parsed()
        {
            //Arrange
            var mockery = new Moq.Mock<ISaleEventListener>();
            //mockery.Setup(se => se.NewSaleInitialized()).Verifiable();

            var commandParser = new CommandParser(mockery.Object);

            //Act
            commandParser.Parse("Command:EndSale");

            //Assert
            mockery.Verify(se => se.EndSale(), Times.Once());
        }
        [TestMethod]
        public void AddProduct_Event_Triggered_When_Input_Command_Parsed()
        {
            //Arrange
            var mockery = new Moq.Mock<ISaleEventListener>();
            //mockery.Setup(se => se.NewSaleInitialized()).Verifiable();

            var commandParser = new CommandParser(mockery.Object);

            //Act
            commandParser.Parse("Input: Barcode=100008888560, Quantity =10");

            //Assert
            mockery.Verify(se => se.AddProduct("100008888560", 10), Times.Once());
        }
    }
}
