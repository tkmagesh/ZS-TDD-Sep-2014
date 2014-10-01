using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathLib.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void Should_Add_Two_Numbers()
        {
            //Arrange
            var calculator = new Calculator();
            int number1 = 10,
                number2 = 20,
                expectedResult = 30;

            //Act
            var result = calculator.Add(number1, number2);

            //Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
