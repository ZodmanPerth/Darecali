using Darecali.Recurrer;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Tests.RecurrerTests
{
    public class EveryNthDayFixture
    {
        #region Non-forwards tests

        [Test]
        public void Unsupported_BackwardsEveryDayTest()
        {
            var sut = new EveryNthDay(DateTime.Today, -1);
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(0), "should be today");
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(-1), "should be yesterday");
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(-2), "should be the day before yesterday");
        }

        [Test]
        public void Unsupported_StaticDayTest()
        {
            var sut = new EveryNthDay(DateTime.Today, 0);
            sut.GetNextDate().ShouldBe(DateTime.Today, "should be today");
            sut.GetNextDate().ShouldBe(DateTime.Today, "should be today #2");
            sut.GetNextDate().ShouldBe(DateTime.Today, "should be today #3");
        }

        #endregion

        [Test]
        public void EveryDayTest()
        {
            var sut = new EveryNthDay(DateTime.Today);
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(0), "should be today");
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(1), "should be tomorrow");
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(2), "should be the day after tomorrow");
        }

        [Test]
        public void EverySecondDayTest()
        {
            var sut = new EveryNthDay(DateTime.Today,2);
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(0), "should be today");
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(2), "should be today + 2");
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(4), "should be today + 4");
        }

        [Test]
        public void EveryThirdDayTest()
        {
            var sut = new EveryNthDay(DateTime.Today, 3);
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(0), "should be today");
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(3), "should be today + 3");
            sut.GetNextDate().ShouldBe(DateTime.Today.AddDays(6), "should be today + 6");
        }
    }
}
