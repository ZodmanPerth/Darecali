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
    public class EveryFrequencySpecialDayOfMonthEveryNthYearStrategyFixture
    {
        #region Exception tests

        [Test]
        public void ShouldThrowWhenFrequencyLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryFrequencySpecialDayOfMonthEveryNthYearStrategy(0);
            });
        }

        [Test]
        public void ShouldThrowWhenFrequencyGreaterThan4Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryFrequencySpecialDayOfMonthEveryNthYearStrategy((Frequency)10);
            });
        }

        [Test]
        public void ShouldThrowWhenSpecialDayLessThan0Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryFrequencySpecialDayOfMonthEveryNthYearStrategy(specialDay: (SpecialDay)(-1));
            });
        }

        [Test]
        public void ShouldThrowWhenSpecialDayGreaterThan9Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryFrequencySpecialDayOfMonthEveryNthYearStrategy(specialDay: (SpecialDay)10);
            });
        }

        [Test]
        public void ShouldThrowWhenMonthLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryFrequencySpecialDayOfMonthEveryNthYearStrategy(month: 0);
            });
        }

        [Test]
        public void ShouldThrowWhenMonthGreaterThan12Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryFrequencySpecialDayOfMonthEveryNthYearStrategy(month: 13);
            });
        }

        [Test]
        public void ShouldThrowWhenNLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryFrequencySpecialDayOfMonthEveryNthYearStrategy(n: 0);
            });
        }

        #endregion

        #region First date tests

        [Test]
        public void StartsBeforeMonthTest()
        {
            var startDate = new DateTime(2016, 01, 08);
            Factory.CreateController(startDate, "Y1,d,3,1")
                .First()
                .ShouldBe(new DateTime(2016, 03, 01), "should be March 1, 2016");
        }

        [Test]
        public void StartsSameDayTest()
        {
            var startDate = new DateTime(2016, 03, 01);
            Factory.CreateController(startDate, "Y1,d,3,1")
                .First()
                .ShouldBe(startDate, "should be March 1, 2016");
        }

        [Test]
        public void StartsAfterInSameMonthTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            Factory.CreateController(startDate, "Y1,d,3,1")
                .First()
                .ShouldBe(new DateTime(2016, 03, 08), "should be March 8, 2016");
        }

        [Test]
        public void StartsAfterInSameMonthWithNoPossibleMatchTest()
        {
            var startDate = new DateTime(2016, 03, 30);
            Factory.CreateController(startDate, "Y4,2,3,1") //4th Monday of March
                .First()
                .ShouldBe(new DateTime(2017, 03, 27), "should be March 27, 2017");
        }

        [Test]
        public void StartsAfterMonthTest()
        {
            var startDate = new DateTime(2016, 11, 11);
            Factory.CreateController(startDate, "Y1,d,3,3")
                .First()
                .ShouldBe(new DateTime(2019, 03, 01), "should be March 1, 2017");
        }

        #endregion

        #region Frequency Tests

        [Test]
        public void EveryFirstDayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,d,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 01), "should be April 1, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 01), "should be April 1, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 01), "should be April 1, 2018");
        }

        [Test]
        public void EverySecondDayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y2,d,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 02), "should be April 2, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 02), "should be April 2, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 02), "should be April 2, 2018");
        }

        [Test]
        public void EveryThirdDayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y3,d,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 03), "should be April 3, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 03), "should be April 3, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 03), "should be April 3, 2018");
        }

        [Test]
        public void EveryFourthDayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y4,d,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 04), "should be April 4, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 04), "should be April 4, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 04), "should be April 4, 2018");
        }

        [Test]
        public void EveryLastDayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "YL,d,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 30), "should be April 30, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 30), "should be April 30, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 30), "should be April 30, 2018");
        }

        #endregion

        #region SpecialDay Tests

        [Test]
        public void EveryFirstWeekDayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,wd,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 01), "should be Friday April 1, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 03), "should be Monday April 3, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 02), "should be Monday April 2, 2018");
        }

        [Test]
        public void EveryFirstWeekendDayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,we,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 02), "should be Saturday April 2, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 01), "should be Saturday April 1, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 01), "should be Sunday April 1, 2018");
        }

        [Test]
        public void EveryFirstSundayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,1,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 03), "should be Sunday April 3, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 02), "should be Sunday April 2, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 01), "should be Sunday April 1, 2018");
        }

        [Test]
        public void EveryFirstMondayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,2,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 04), "should be Monday April 4, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 03), "should be Monday April 3, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 02), "should be Monday April 2, 2018");
        }

        [Test]
        public void EveryFirstTuesdayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,3,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 05), "should be Tuesday, April 5, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 04), "should be Tuesday, April 4, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 03), "should be Tuesday, April 3, 2018");
        }

        [Test]
        public void EveryFirstWednesdayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,4,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 06), "should be Wednesday, April 6, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 05), "should be Wednesday, April 5, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 04), "should be Wednesday, April 4, 2018");
        }

        [Test]
        public void EveryFirstThursdayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,5,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 07), "should be Thursday, April 7, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 06), "should be Thursday, April 6, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 05), "should be Thursday, April 5, 2018");
        }

        [Test]
        public void EveryFirstFridayOfAprilEveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,6,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 01), "should be Friday, April 1, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 07), "should be Friday, April 7, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 06), "should be Friday, April 6, 2018");
        }

        [Test]
        public void EveryFirstSaturdayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y1,7,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 02), "should be Saturday, April 2, 2016");
            sut[1].ShouldBe(new DateTime(2017, 04, 01), "should be Saturday, April 1, 2017");
            sut[2].ShouldBe(new DateTime(2018, 04, 07), "should be Saturday, April 7, 2018");
        }

        #endregion

        #region Not Every Year Tests

        [Test]
        public void EverySecondTuesdayOfAprilEverySecondYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y2,3,4,2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 12), "should be Tuesday, April 12, 2016");
            sut[1].ShouldBe(new DateTime(2018, 04, 10), "should be Tuesday, April 10, 2018");
            sut[2].ShouldBe(new DateTime(2020, 04, 14), "should be Tuesday, April 14, 2020");
        }

        [Test]
        public void EverySecondTuesdayOfAprilEveryThirdYearTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y2,3,4,3")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 12), "should be Tuesday, April 12, 2016");
            sut[1].ShouldBe(new DateTime(2019, 04, 09), "should be Tuesday, April 09, 2019");
            sut[2].ShouldBe(new DateTime(2022, 04, 12), "should be Tuesday, April 12, 2022");
        }

        #endregion
    }
}
