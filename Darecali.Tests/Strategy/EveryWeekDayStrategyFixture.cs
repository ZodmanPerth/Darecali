using Darecali.Strategy;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Tests.Strategy
{
    [Category("Strategy")]
    public class EveryWeekDayStrategyFixture
    {
        [Test]
        public void WeekdaysTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 22),
                new DateTime(2016, 02, 23),
                new DateTime(2016, 02, 24),
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 26),

                new DateTime(2016, 02, 29),
                new DateTime(2016, 03, 01),
                new DateTime(2016, 03, 02),
                new DateTime(2016, 03, 03),
                new DateTime(2016, 03, 04),

                new DateTime(2016, 03, 07),
                new DateTime(2016, 03, 08),
                new DateTime(2016, 03, 09),
                new DateTime(2016, 03, 10),
                new DateTime(2016, 03, 11),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 22);  //Monday
            var sut = Factory.CreateController(startDate, "Dwd")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekdaysStartingWednesdayTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 24),
                new DateTime(2016, 02, 25),
                new DateTime(2016, 02, 26),

                new DateTime(2016, 02, 29),
                new DateTime(2016, 03, 01),
                new DateTime(2016, 03, 02),
                new DateTime(2016, 03, 03),
                new DateTime(2016, 03, 04),

                new DateTime(2016, 03, 07),
                new DateTime(2016, 03, 08),
                new DateTime(2016, 03, 09),
                new DateTime(2016, 03, 10),
                new DateTime(2016, 03, 11),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 24);  //Wednesday
            var sut = Factory.CreateController(startDate, "Dwd")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekdaysStartingSaturdayTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 29),
                new DateTime(2016, 03, 01),
                new DateTime(2016, 03, 02),
                new DateTime(2016, 03, 03),
                new DateTime(2016, 03, 04),

                new DateTime(2016, 03, 07),
                new DateTime(2016, 03, 08),
                new DateTime(2016, 03, 09),
                new DateTime(2016, 03, 10),
                new DateTime(2016, 03, 11),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 27);  //Saturday
            var sut = Factory.CreateController(startDate, "Dwd")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekdaysStartingSundayTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 29),
                new DateTime(2016, 03, 01),
                new DateTime(2016, 03, 02),
                new DateTime(2016, 03, 03),
                new DateTime(2016, 03, 04),

                new DateTime(2016, 03, 07),
                new DateTime(2016, 03, 08),
                new DateTime(2016, 03, 09),
                new DateTime(2016, 03, 10),
                new DateTime(2016, 03, 11),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 28);  //Sunday
            var sut = Factory.CreateController(startDate, "Dwd")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }
    }
}
