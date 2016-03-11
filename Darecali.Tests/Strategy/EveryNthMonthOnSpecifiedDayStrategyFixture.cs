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
    public class EveryNthMonthOnSpecifiedDayStrategyFixture
    {
        #region Out of range parameter tests

        [Test]
        public void ShouldThrowWhenDayLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthMonthOnSpecifiedDayStrategy(0);
            });
        }

        [Test]
        public void ShouldThrowWhenDayGreaterThan31Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthMonthOnSpecifiedDayStrategy(32);
            });
        }

        [Test]
        public void ShouldThrowWhenNLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthMonthOnSpecifiedDayStrategy(n: 0);
            });
        }

        #endregion

        #region First date tests

        [Test]
        public void StartsSameDayTest()
        {
            Factory.CreateController(DateTime.Today, "M" + DateTime.Today.Day)
                .First()
                .ShouldBe(DateTime.Today, "should be today");
        }

        [Test]
        public void StartsLaterDayTest()
        {
            var startDate = new DateTime(2016, 02, 24);
            Factory.CreateController(startDate, "M5")
                .First()
                .ShouldBe(new DateTime(2016, 03, 05), "should be March 05, 2016");
        }

        [Test]
        public void StartsEarlierDayTest()
        {
            var startDate = new DateTime(2016, 02, 24);
            Factory.CreateController(startDate, "M27")
                .First()
                .ShouldBe(new DateTime(2016, 02, 27), "should be February 27, 2016");
        }

        #endregion

        #region Nth month tests

        [Test]
        public void Day5EveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 24);
            var sut = Factory.CreateController(startDate, "M5")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 05), "should be March 5, 2016");
            sut[1].ShouldBe(new DateTime(2016, 04, 05), "should be April 5, 2016");
            sut[2].ShouldBe(new DateTime(2016, 05, 05), "should be May 5, 2016");
        }

        [Test]
        public void Day5EverySecondMonthTest()
        {
            var startDate = new DateTime(2016, 02, 24);
            var sut = Factory.CreateController(startDate, "M5,2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 05), "should be March 5, 2016");
            sut[1].ShouldBe(new DateTime(2016, 05, 05), "should be May 5, 2016");
            sut[2].ShouldBe(new DateTime(2016, 07, 05), "should be July 5, 2016");
        }

        [Test]
        public void Day5EveryThirdMonthTest()
        {
            var startDate = new DateTime(2016, 02, 24);
            var sut = Factory.CreateController(startDate, "M5,3")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 05), "should be March 5, 2016");
            sut[1].ShouldBe(new DateTime(2016, 06, 05), "should be June 5, 2016");
            sut[2].ShouldBe(new DateTime(2016, 09, 05), "should be Septeber 5, 2016");
        }

        #endregion

        #region Date not available every month tests

        [Test]
        public void EveryDay31EveryMonthTest()
        {
            var startDate = new DateTime(2016, 02, 24);
            var sut = Factory.CreateController(startDate, "M31")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 31), "should be March 31, 2016");
            sut[1].ShouldBe(new DateTime(2016, 05, 31), "should be May 31, 2016");
            sut[2].ShouldBe(new DateTime(2016, 07, 31), "should be July 31, 2016");
        }

        [Test]
        public void EveryDay31EverySecondMonthTest()
        {
            var startDate = new DateTime(2016, 02, 24);
            var sut = Factory.CreateController(startDate, "M31,2")
                .Take(4).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 31), "should be March 31, 2016");
            sut[1].ShouldBe(new DateTime(2016, 05, 31), "should be May 31, 2016");
            sut[2].ShouldBe(new DateTime(2016, 07, 31), "should be July 31, 2016");
            sut[3].ShouldBe(new DateTime(2016, 10, 31), "should be October 31, 2016");
        }

        #endregion

    }
}
