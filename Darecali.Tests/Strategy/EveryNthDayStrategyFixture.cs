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
    public class EveryNthDayStrategyFixture
    {
        #region Non-forwards tests

        [Test]
        public void BackwardsEveryDayTest()
        {
            var sut = Factory.CreateController(DateTime.Today, "D-1")
                .Take(3).ToList();
            sut[0].ShouldBe(DateTime.Today.AddDays(0), "should be today");
            sut[1].ShouldBe(DateTime.Today.AddDays(-1), "should be yesterday");
            sut[2].ShouldBe(DateTime.Today.AddDays(-2), "should be the day before yesterday");
        }

        [Test]
        public void StaticDayTest()
        {
            var sut = Factory.CreateController(DateTime.Today, "D0")
                .Take(3).ToList();
            sut[0].ShouldBe(DateTime.Today, "should be today");
            sut[1].ShouldBe(DateTime.Today, "should be today #2");
            sut[2].ShouldBe(DateTime.Today, "should be today #3");
        }

        #endregion

        [TestCase("D", TestName = "Implicit Every Day Test")]
        [TestCase("D1", TestName = "Explicit Every Day Test")]
        public void EveryDayTest(string definition)
        {
            var sut = Factory.CreateController(DateTime.Today, definition)
                .Take(3).ToList();
            sut[0].ShouldBe(DateTime.Today.AddDays(0), "should be today");
            sut[1].ShouldBe(DateTime.Today.AddDays(1), "should be tomorrow");
            sut[2].ShouldBe(DateTime.Today.AddDays(2), "should be the day after tomorrow");
        }

        [Test]
        public void EverySecondDayTest()
        {
            var sut = Factory.CreateController(DateTime.Today, "D2")
                .Take(3).ToList();
            sut[0].ShouldBe(DateTime.Today.AddDays(0), "should be today");
            sut[1].ShouldBe(DateTime.Today.AddDays(2), "should be today + 2");
            sut[2].ShouldBe(DateTime.Today.AddDays(4), "should be today + 4");
        }

        [Test]
        public void EveryThirdDayTest()
        {
            var sut = Factory.CreateController(DateTime.Today, "D3")
                .Take(3).ToList();
            sut[0].ShouldBe(DateTime.Today.AddDays(0), "should be today");
            sut[1].ShouldBe(DateTime.Today.AddDays(3), "should be today + 3");
            sut[2].ShouldBe(DateTime.Today.AddDays(6), "should be today + 6");
        }
    }
}
