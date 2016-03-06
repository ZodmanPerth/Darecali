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
    public class EveryNthYearOnSpecificMonthAndDayStrategyFixture
    {
        #region Exception tests

        [Test]
        public void ShouldThrowWhenMonthLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthYearOnSpecificMonthAndDayStrategy(0);
            });
        }

        [Test]
        public void ShouldThrowWhenMonthGreaterThan12Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthYearOnSpecificMonthAndDayStrategy(13);
            });
        }

        [Test]
        public void ShouldThrowWhenDayLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthYearOnSpecificMonthAndDayStrategy(day: 0);
            });
        }

        [Test]
        public void ShouldThrowWhenDayGreaterThan31Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthYearOnSpecificMonthAndDayStrategy(day: 32);
            });
        }

        [Test]
        public void ShouldThrowWhenNLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthYearOnSpecificMonthAndDayStrategy(n: 0);
            });
        }

        [TestCase(2, 30)]
        [TestCase(2, 31)]
        public void ShouldThrowWhenInvalidDateFebruaryTest(int month, int day)
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthYearOnSpecificMonthAndDayStrategy(month, day);
            });
        }

        public void ShouldNotThrowWhenFebruary29Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldNotThrow(() =>
            {
                var sut = new EveryNthYearOnSpecificMonthAndDayStrategy(2, 29);
            });
        }

        [TestCase(9)]
        [TestCase(4)]
        [TestCase(6)]
        [TestCase(11)]
        public void ShouldThrowWhenInvalidDateMonthsWithOnly30DaysTest(int month)
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<ArgumentOutOfRangeException>(() =>
            {
                var sut = new EveryNthYearOnSpecificMonthAndDayStrategy(month, 31);
            });
        }

        #endregion

        #region First date tests

        [Test]
        public void StartsSameDayTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            Factory.CreateController(startDate, "Y3,3")
                .First()
                .ShouldBe(startDate, "should be March 3, 2016");
        }

        [Test]
        public void StartsEarlierInYearTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            Factory.CreateController(startDate, "Y11,11")
                .First()
                .ShouldBe(new DateTime(2016, 11, 11), "should be November 11, 2016");
        }

        [Test]
        public void StartsLaterInYearTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            Factory.CreateController(startDate, "Y1,1")
                .First()
                .ShouldBe(new DateTime(2017, 01, 01), "should be January 1, 2017");
        }

        #endregion

        #region Factory Parameter tests

        [Test]
        public void NoParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2017, 01, 01), "should be January 1, 2017");
            sut[1].ShouldBe(new DateTime(2018, 01, 01), "should be January 1, 2018");
            sut[2].ShouldBe(new DateTime(2019, 01, 01), "should be January 1, 2019");
        }

        [Test]
        public void OneParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 01), "should be November 1, 2016");
            sut[1].ShouldBe(new DateTime(2017, 11, 01), "should be November 1, 2017");
            sut[2].ShouldBe(new DateTime(2018, 11, 01), "should be November 1, 2018");
        }

        [Test]
        public void TwoParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11,11")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 11), "should be November 11, 2016");
            sut[1].ShouldBe(new DateTime(2017, 11, 11), "should be November 11, 2017");
            sut[2].ShouldBe(new DateTime(2018, 11, 11), "should be November 11, 2018");
        }

        [Test]
        public void ThreeParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11,11,3")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 11), "should be November 11, 2016");
            sut[1].ShouldBe(new DateTime(2019, 11, 11), "should be November 11, 2019");
            sut[2].ShouldBe(new DateTime(2022, 11, 11), "should be November 11, 2022");
        }

        #endregion

        [Test]
        public void EveryOctober6EveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y10,6")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 10, 06), "should be October 6, 2016");
            sut[1].ShouldBe(new DateTime(2017, 10, 06), "should be October 6, 2017");
            sut[2].ShouldBe(new DateTime(2018, 10, 06), "should be October 6, 2018");
        }

        [Test]
        public void EveryOctober6EveryTwoYearsTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y10,6,2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 10, 06), "should be October 6, 2016");
            sut[1].ShouldBe(new DateTime(2018, 10, 06), "should be October 6, 2018");
            sut[2].ShouldBe(new DateTime(2020, 10, 06), "should be October 6, 2020");
        }

        [Test]
        public void EveryOctober6EveryThreeYearsTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y10,6,3")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 10, 06), "should be October 6, 2016");
            sut[1].ShouldBe(new DateTime(2019, 10, 06), "should be October 6, 2019");
            sut[2].ShouldBe(new DateTime(2022, 10, 06), "should be October 6, 2022");
        }

        #region Date not available every year tests (Feb 29 tests)

        [Test]
        public void EveryFebruary29EveryYearTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y2,29,1")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2020, 02, 29), "should be February 29, 2020");
            sut[1].ShouldBe(new DateTime(2024, 02, 29), "should be February 29, 2024");
            sut[2].ShouldBe(new DateTime(2028, 02, 29), "should be February 29, 2028");
        }

        [Test]
        public void EveryFebruary29EverySecondYearTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y2,29,2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2020, 02, 29), "should be February 29, 2020");
            sut[1].ShouldBe(new DateTime(2024, 02, 29), "should be February 29, 2024");
            sut[2].ShouldBe(new DateTime(2028, 02, 29), "should be February 29, 2028");
        }

        [Test]
        public void EveryFebruary29EveryThirdYearTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y2,29,3")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2020, 02, 29), "should be February 29, 2020");
            sut[1].ShouldBe(new DateTime(2032, 02, 29), "should be February 29, 2032");
            sut[2].ShouldBe(new DateTime(2044, 02, 29), "should be February 29, 2044");
        }

        [Test]
        public void Skips100YearsNot1000YearsFebruary29Test()
        {
            var startDate = new DateTime(2196, 01, 01);
            var sut = Factory.CreateController(startDate, "Y2,29")
                .Take(2).ToList();
            sut[0].ShouldBe(new DateTime(2196, 02, 29), "should be February 29, 2196");
            sut[1].ShouldBe(new DateTime(2204, 02, 29), "should be February 29, 2204");
        }

        #endregion
    }
}
