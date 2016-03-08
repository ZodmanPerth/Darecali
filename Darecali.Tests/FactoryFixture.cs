using Darecali.Strategy;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Tests
{
    [Category("Factory")]
    public class FactoryFixture
    {
        #region Basic Factory Tests

        [Test]
        public void CreateControllerWithNullStrategyThrows()
        {
            ShouldThrowExtensions.ShouldThrow<ArgumentNullException>(() =>
            {
                Factory.CreateController(DateTime.Today, (IRecurrenceStrategy)null);
            });
        }

        [Test]
        public void CreateControllerWithInvalidStrategyDefinitionThrows()
        {
            ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                Factory.CreateController(DateTime.Today, "ZZZ");
            });
        }

        #endregion

        [Test]
        public void Daily_TooManyParamsTest()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("D5,2");
            });
        }

        #region Valid strategy definition parameter tests

        [Test]
        public void Daily_NoParamTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "D")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 08));
            sut[1].ShouldBe(new DateTime(2016, 03, 09));
            sut[2].ShouldBe(new DateTime(2016, 03, 10));
        }

        [Test]
        public void Daily_OneParamTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "D2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 08));
            sut[1].ShouldBe(new DateTime(2016, 03, 10));
            sut[2].ShouldBe(new DateTime(2016, 03, 12));
        }

        [Test]
        public void Weekly_NoParamTest()
        {
            var sut = Factory.CreateController(DateTime.Today, "W")
                .Take(21).ToList();
            for (int i = 0; i < 21; i++)
                sut[i].ShouldBe(DateTime.Today.AddDays(i), "should be today + " + i);
        }

        [Test]
        public void Weekly_OneParamTest()
        {
            DateTime startDate = new DateTime(2016, 02, 22);
            var sut = Factory.CreateController(startDate, "W2")
                .Take(21).ToList();
            for (int i = 0; i < 7; i++)
                sut[i].ShouldBe(startDate.AddDays(i), "should be startDate + " + i);
            for (int i = 0; i < 7; i++)
                sut[7 + i].ShouldBe(startDate.AddDays(14 + i), "should be startDate + 2 weeks + " + i);
            for (int i = 0; i < 7; i++)
                sut[14 + i].ShouldBe(startDate.AddDays(28 + i), "should be startDate + 4 weeks + " + i);
        }

        [Test]
        public void WeeklyTwoParamTest()
        {
            DateTime startDate = new DateTime(2016, 03, 07);  //Monday
            var sut = Factory.CreateController(startDate, "W2,2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 03, 07));
            sut[1].ShouldBe(new DateTime(2016, 03, 21));
            sut[2].ShouldBe(new DateTime(2016, 04, 04));
        }

        [Test]
        public void MonthlyNoParamTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "M")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 01));
            sut[1].ShouldBe(new DateTime(2016, 05, 01));
            sut[2].ShouldBe(new DateTime(2016, 06, 01));
        }

        [Test]
        public void MonthlyOneParamTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "M2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 02));
            sut[1].ShouldBe(new DateTime(2016, 05, 02));
            sut[2].ShouldBe(new DateTime(2016, 06, 02));
        }

        [Test]
        public void MonthlyTwoParamTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "M2,2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 04, 02));
            sut[1].ShouldBe(new DateTime(2016, 06, 02));
            sut[2].ShouldBe(new DateTime(2016, 08, 02));
        }

        [Test]
        public void Yearly_NoParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2017, 01, 01));
            sut[1].ShouldBe(new DateTime(2018, 01, 01));
            sut[2].ShouldBe(new DateTime(2019, 01, 01));
        }

        [Test]
        public void Yearly_OneParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 01));
            sut[1].ShouldBe(new DateTime(2017, 11, 01));
            sut[2].ShouldBe(new DateTime(2018, 11, 01));
        }

        [Test]
        public void Yearly_TwoParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11,11")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 11));
            sut[1].ShouldBe(new DateTime(2017, 11, 11));
            sut[2].ShouldBe(new DateTime(2018, 11, 11));
        }

        [Test]
        public void Yearly_ThreeParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11,11,3")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 11));
            sut[1].ShouldBe(new DateTime(2019, 11, 11));
            sut[2].ShouldBe(new DateTime(2022, 11, 11));
        }

        [Test]
        public void Yearly_FourParamTest()
        {
            var startDate = new DateTime(2016, 03, 08);
            var sut = Factory.CreateController(startDate, "Y2,2,2,2")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2018, 02, 12));
            sut[1].ShouldBe(new DateTime(2020, 02, 10));
            sut[2].ShouldBe(new DateTime(2022, 02, 14));
        }

        #endregion

        #region Yearly with frequency, special day, month, and n tests

        #region Exceptions

        [Test]
        public void YearlyFrequency_ShouldThrowWhenFrequencyLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("Y0,d,1,1");
            });
        }

        [Test]
        public void YearlyFrequency_ShouldThrowWhenFrequencyGreaterThan4Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("Y5,d,1,1");
            });
        }

        [Test]
        public void YearlyFrequency_ShouldThrowWhenSpecialDayLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("Y1,0,1,1");
            });
        }

        [Test]
        public void YearlyFrequency_ShouldThrowWhenSpecialDayGreaterThan7Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("Y1,8,1,1");
            });
        }

        [Test]
        public void YearlyFrequency_ShouldThrowWhenFrequencyNotValidLetterTest()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("Y1,aa,1,1");
            });
        }

        [Test]
        public void YearlyFrequency_ShouldThrowWhenMonthLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("Y1,1,0,1");
            });
        }

        [Test]
        public void YearlyFrequency_ShouldThrowWhenMonthGreaterThan12Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("Y1,1,13,1");
            });
        }

        [Test]
        public void YearlyFrequency_ShouldThrowWhenNLessThan1Test()
        {
            Shouldly.ShouldThrowExtensions.ShouldThrow<InvalidStrategyDefinitionException>(() =>
            {
                var sut = Factory.CreateStrategy("Y1,1,13,-1");
            });
        }

        #endregion

        #endregion
    }
}
