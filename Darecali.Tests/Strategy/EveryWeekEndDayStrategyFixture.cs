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
    public class EveryWeekEndDayStrategyFixture
    {
        [Test]
        public void WeekendDaysTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 27),
                new DateTime(2016, 02, 28),

                new DateTime(2016, 03, 05),
                new DateTime(2016, 03, 06),

                new DateTime(2016, 03, 12),
                new DateTime(2016, 03, 13),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 27);  //Saturday
            var sut = Factory.CreateController(startDate, "Dwe")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekendDaysStartingWednesdayTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 27),
                new DateTime(2016, 02, 28),

                new DateTime(2016, 03, 05),
                new DateTime(2016, 03, 06),

                new DateTime(2016, 03, 12),
                new DateTime(2016, 03, 13),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 24);  //Wednesday
            var sut = Factory.CreateController(startDate, "Dwe")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekendDaysStartingSaturdayTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 27),
                new DateTime(2016, 02, 28),

                new DateTime(2016, 03, 05),
                new DateTime(2016, 03, 06),

                new DateTime(2016, 03, 12),
                new DateTime(2016, 03, 13),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 27);  //Saturday
            var sut = Factory.CreateController(startDate, "Dwe")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void WeekendDaysStartingSundayTest()
        {
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 02, 28),

                new DateTime(2016, 03, 05),
                new DateTime(2016, 03, 06),

                new DateTime(2016, 03, 12),
                new DateTime(2016, 03, 13),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 02, 28);  //Sunday
            var sut = Factory.CreateController(startDate, "Dwe")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }
    }
}
