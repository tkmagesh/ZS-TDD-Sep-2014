using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreetingApp
{
    public interface ITimeService
    {
        DateTime GetTime();
    }

    public class TimeService : ITimeService
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }


    public class Greeter
    {
        ITimeService _timeService;

        public Greeter(ITimeService timeService)
        {
            _timeService = timeService;
        }
        public string Greet(string name)
        {
            if (_timeService.GetTime().Hour < 12)
            {
                return string.Format("Good Morning {0}!", name);
            }
            else
            {
                return string.Format("Good Evening {0}!", name);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var timeService = new TimeService();
            var greeter = new Greeter(timeService);
            Console.WriteLine("Enter your name:");
            var name = Console.ReadLine();
            var greetMsg = greeter.Greet(name);
            Console.WriteLine(greetMsg);
            Console.ReadLine();
        }
    }
}
