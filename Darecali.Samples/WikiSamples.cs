using Darecali.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Samples
{
    public static class WikiSamples
    {
        #region Factory Samples

        public static void FactoryCreateControllerSample()
        {
            var startDate = new DateTime(2016, 06, 08);
            var controller = Factory.CreateController(startDate, "D");
            foreach (var date in controller.Take(3))
                Console.WriteLine(date.ToShortDateString());
        }

        public static void FactoryCreateStrategySample()
        {
            var strategy = Factory.CreateStrategy("D");
            Console.WriteLine(strategy);
        }

        public static void FactoryGetStrategyDefinitionUsageSample()
        {
            Console.WriteLine(Factory.GetStrategyDefinitionUsage());
        }

        #endregion

        #region Recurrence Strategies

        public static void RecurEvery3DaysSample()
        {
            var startDate = new DateTime(2016, 06, 04);
            var controller = Factory.CreateController(startDate, "D3");
            foreach (var date in controller.Take(4))
                Console.WriteLine(date.ToShortDateString());
        }

        public static void RecurEveryWeekdaySample()
        {
            var startDate = new DateTime(2016, 06, 08);
            var controller = Factory.CreateController(startDate, "Dwd");
            foreach (var date in controller.Take(4))
                Console.WriteLine(date.ToLongDateString());
        }

        public static void RecurEveryWeekendDaySample()
        {
            var startDate = new DateTime(2016, 06, 08);
            var controller = Factory.CreateController(startDate, "Dwe");
            foreach (var date in controller.Take(4))
                Console.WriteLine(date.ToLongDateString());
        }

        public static void RecurEveryWeekdayEverySecondWeekSample()
        {
            var startDate = new DateTime(2016, 06, 06);
            var controller = Factory.CreateController(startDate, "W2");
            foreach (var date in controller.Take(10))
                Console.WriteLine(date.ToLongDateString());
        }

        public static void RecurEveryMonWedFriEverySecondWeekSample()
        {
            var startDate = new DateTime(2016, 06, 06);
            var days = (int)(DayOfWeekFlags.Monday | DayOfWeekFlags.Wednesday | DayOfWeekFlags.Friday);
            var controller = Factory.CreateController(startDate, "W" + days + ",2");
            foreach (var date in controller.Take(6))
                Console.WriteLine(date.ToLongDateString());
        }

        public static void RecurThe6thOfEveryFourMonthsSample()
        {
            var startDate = new DateTime(2016, 06, 06);
            var controller = Factory.CreateController(startDate, "M6,4");
            foreach (var date in controller.Take(4))
                Console.WriteLine(date.ToShortDateString());
        }

        public static void RecurTheLastWeekdayOfEveryMonthSample()
        {
            var startDate = new DateTime(2016, 06, 06);
            var controller = Factory.CreateController(startDate, "ML,wd,1");
            foreach (var date in controller.Take(3))
                Console.WriteLine(date.ToLongDateString());
        }

        public static void RecurEvery4YearsOnFebruary29Sample()
        {
            var startDate = new DateTime(2016, 06, 06);
            var controller = Factory.CreateController(startDate, "Y2,29,4");
            foreach (var date in controller.Take(4))
                Console.WriteLine(date.ToShortDateString());
        }

        public static void RecurEveryFourthTuesdayOfOctoberEverySecondYearTest()
        {
            var startDate = new DateTime(2016, 06, 07);
            var controller = Factory.CreateController(startDate, "Y4,3,10,2");
            foreach (var date in controller.Take(3))
                Console.WriteLine(date.ToLongDateString());
        }

        #endregion

        #region Terminating sequences

        public static void RecurEveryWeekdayUntilTheFirstOfJuly()
        {
            var startDate = new DateTime(2016, 06, 07);
            var terminationDate = new DateTime(2016, 07, 01);
            var controller = Factory.CreateController(startDate, "Dwd", terminationDate);
            foreach (var date in controller)
                Console.WriteLine(date.ToLongDateString());
        }

        public static void RecurEveryWeekday7Times()
        {
            var startDate = new DateTime(2016, 06, 07);
            var controller = Factory.CreateController(startDate, "Dwd", numberOfOccurrences: 7);
            foreach (var date in controller)
                Console.WriteLine(date.ToLongDateString());
        }

        public static void RecurEveryWeekdayAndTerminateAtTheFirstOfJulyOrAfter15Times()
        {
            var startDate = new DateTime(2016, 06, 07);
            var terminationDate = new DateTime(2016, 07, 01);
            var controller = Factory.CreateController(startDate, "Dwd", terminationDate, 15);
            foreach (var date in controller)
                Console.WriteLine(date.ToLongDateString());
        }

        #endregion
    }
}
