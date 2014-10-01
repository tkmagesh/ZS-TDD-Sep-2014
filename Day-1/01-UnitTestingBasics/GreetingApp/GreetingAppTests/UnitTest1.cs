using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GreetingApp;
using Moq;

namespace GreetingAppTests
{

    /*
    public class TimeServiceForMorning : ITimeService
    {
        public DateTime GetTime()
        {
            return new DateTime(2014, 09, 30, 9, 0, 0);
        }
    }

    public class TimeServiceForEvening : ITimeService
    {
        public DateTime GetTime()
        {
            return new DateTime(2014, 09, 30, 17, 0, 0);
        }
    }
     */

    [TestClass]
    public class GreeterTests
    {
        [TestMethod]
        public void Should_Greet_Good_Morning_For_Morning()
        {
            //Arrange
            var mockery = new Mock<ITimeService>();
            mockery.Setup(ts => ts.GetTime()).Returns(new DateTime(2014, 09, 30, 9, 0, 0));

            var morningTimeService = mockery.Object;
            var greeter = new GreetingApp.Greeter(morningTimeService);
            var name = "Magesh";
            var expectedResult = string.Format("Good Morning {0}!",name);

            //Act
            var greetMsg = greeter.Greet(name);

            //Assert
            Assert.AreEqual(expectedResult, greetMsg);
        }

        [TestMethod]
        public void Should_Greet_Good_Morning_For_Evening()
        {
            //Arrange
            var mockery = new Mock<ITimeService>();
            mockery.Setup(ts => ts.GetTime()).Returns(new DateTime(2014, 09, 30, 15, 0, 0));

            var eveningTimeService = mockery.Object;
            var greeter = new GreetingApp.Greeter(eveningTimeService);
            var name = "Magesh";
            var expectedResult = string.Format("Good Evening {0}!", name);

            //Act
            var greetMsg = greeter.Greet(name);

            //Assert
            Assert.AreEqual(expectedResult, greetMsg);
        }
    }
}
