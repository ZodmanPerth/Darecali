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
    public class EveryNthMonthOnFrequencySpecialDayStrategyFixture
    {
        #region Out of range parameter tests

        [Test]
        public void ShouldThrowWhenFrequencyLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryNthMonthOnFrequencySpecialDayStrategy(0);
            });
        }

        [Test]
        public void ShouldThrowWhenFrequencyGreaterThan5Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryNthMonthOnFrequencySpecialDayStrategy((Frequency)6);
            });
        }

        [Test]
        public void ShouldThrowWhenSpecialDayLessThan0Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryNthMonthOnFrequencySpecialDayStrategy(specialDay: (SpecialDay)(-1));
            });
        }

        [Test]
        public void ShouldThrowWhenSpecialDayGreaterThan9Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentException>(() =>
            {
                var sut = new EveryNthMonthOnFrequencySpecialDayStrategy(specialDay: (SpecialDay)10);
            });
        }

        [Test]
        public void ShouldThrowWhenNLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthMonthOnFrequencySpecialDayStrategy(n: 0);
            });
        }

        #endregion

        #region First date tests

        [Test]
        public void StartsSameDayTest()
        {
            Factory.CreateController(DateTime.Today, "M1,d,1")
                .First()
                .ShouldBe(DateTime.Today, "should be today");
        }

        [Test]
        public void WeekdayWhenMonthStartsOnAWeekendDayTest()
        {
            var startDate = new DateTime(2016, 05, 01);  //Sunday
            Factory.CreateController(startDate, "M1,wd,1")
                .First()
                .ShouldBe(new DateTime(2016, 05, 02), "should be Monday, May 2, 2016");
        }

        [Test]
        public void WeekendDayWhenStartsOnASundayTest()
        {
            var startDate = new DateTime(2016, 05, 01);  //Sunday
            Factory.CreateController(startDate, "M1,we,1")
                .First()
                .ShouldBe(new DateTime(2016, 05, 01), "should be Sunday, May 1, 2016");
        }

        [Test]
        public void NoMatchInFirstMonthTest()
        {
            var startDate = new DateTime(2016, 02, 29);
            Factory.CreateController(startDate, "M1,we,1")
                .First()
                .ShouldBe(new DateTime(2016, 03, 05), "should be Saturday, March 5, 2016");
        }

        [Test]
        public void NoMatchInFirstMonthTest2()
        {
            var startDate = new DateTime(2016, 02, 28);
            Factory.CreateController(startDate, "M2,we,1")
                .First()
                .ShouldBe(new DateTime(2016, 03, 06), "should be Sunday, March 6, 2016");
        }

        #endregion

        #region Frequency tests

        [Test]
        public void FirstDayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,d,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 22), "should be Monday, February 22, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 01), "should be Tuesday, March 1, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 01), "should be Friday, April 1, 2016");
        }

        [Test]
        public void SecondDayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M2,d,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 23), "should be Tuesday, February 23, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 02), "should be Wednesday, March 2, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 02), "should be Saturday, April 2, 2016");
        }

        [Test]
        public void ThirdDayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M3,d,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 24), "should be Tuesday, February 23, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 03), "should be Thursday, March 3, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 03), "should be Sunday, April 3, 2016");
        }

        [Test]
        public void FourthDayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M4,d,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 25), "should be Wednesday, February 24, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 04), "should be Thursday, March 4, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 04), "should be Sunday, April 4, 2016");
        }

        [Test]
        public void LastDayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "ML,d,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 29), "should be Monday, February 29, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 31), "should be Thursday, March 31, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 30), "should be Saturday, April 30, 2016");
        }

        #endregion

        #region SpecialDay tests

        [Test]
        public void FirstWeekDayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,wd,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 22), "should be Monday, February 22, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 01), "should be Tuesday, March 1, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 01), "should be Friday, April 1, 2016");
        }

        [Test]
        public void FirstWeekendDayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,we,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 27), "should be Saturday, February 28, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 05), "should be Saturday, March 5, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 02), "should be Saturday, April 2, 2016");
        }

        [Test]
        public void FirstSundayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,1,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 28), "should be Sunday, February 28, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 06), "should be Sunday, March 6, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 03), "should be Sunday, April 3, 2016");
        }

        [Test]
        public void FirstMondayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,2,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 22), "should be Monday, February 22, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 07), "should be Monday, March 7, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 04), "should be Monday, April 4, 2016");
        }

        [Test]
        public void FirstTuesdayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,3,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 23), "should be Tuesday, February 23, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 01), "should be Tuesday, March 1, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 05), "should be Tuesday, April 5, 2016");
        }

        [Test]
        public void FirstWednesdayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,4,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 24), "should be Wednesday, February 24, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 02), "should be Wednesday, March 2, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 06), "should be Wednesday, April 6, 2016");
        }

        [Test]
        public void FirstThursdayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,5,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 25), "should be Thursday, February 25, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 03), "should be Thursday, March 3, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 07), "should be Thursday, April 7, 2016");
        }

        [Test]
        public void FirstFridayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,6,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 26), "should be Friday, February 26, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 04), "should be Friday, March 4, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 01), "should be Friday, April 1, 2016");
        }

        [Test]
        public void FirstSaturdayOfEveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "M1,7,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 02, 27), "should be Saturday, February 27, 2016");
            sut[1].ShouldBe(new DateTime(2016, 03, 05), "should be Saturday, March 5, 2016");
            sut[2].ShouldBe(new DateTime(2016, 04, 02), "should be Saturday, April 2, 2016");
        }

        #endregion

        #region Nth month tests

        [Test]
        public void SecondTuesdayOfEverySecondMonthTest()
        {
            var startDate = new DateTime(2016, 03, 01);
            var sut = Factory.CreateController(startDate, "M2,3,2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 08), "should be Tuesday, March 8, 2016");
            sut[1].ShouldBe(new DateTime(2016, 05, 10), "should be Tuesday, May 10, 2016");
            sut[2].ShouldBe(new DateTime(2016, 07, 12), "should be Tuesday, July 12, 2016");
        }

        [Test]
        public void SecondTuesdayOfEveryThirdMonthTest()
        {
            var startDate = new DateTime(2016, 03, 01);
            var sut = Factory.CreateController(startDate, "M2,3,3")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 08), "should be Tuesday, March 8, 2016");
            sut[1].ShouldBe(new DateTime(2016, 06, 14), "should be Tuesday, June 14, 2016");
            sut[2].ShouldBe(new DateTime(2016, 09, 13), "should be Tuesday, September 13, 2016");
        }

        #endregion
    }
}
