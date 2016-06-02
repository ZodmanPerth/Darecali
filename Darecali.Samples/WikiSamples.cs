using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Samples
{
    public static class WikiSamples
    {
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

    }
}
