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
            #region Expected Results

            List<DateTime> expectedResults = new List<DateTime>()
            {
                new DateTime(2016, 03, 07),
                new DateTime(2016, 03, 21),
                new DateTime(2016, 04, 04),
            };

            #endregion;

            DateTime startDate = new DateTime(2016, 03, 07);  //Monday
            var sut = Factory.CreateController(startDate, "W2,2")
                .Take(expectedResults.Count).ToList();

            for (int i = 0; i < expectedResults.Count; i++)
                sut[i].ShouldBe(expectedResults[i], string.Format("at {0}, should be {1}", i, expectedResults[i].ToString("D")));
        }

        [Test]
        public void Yearly_NoParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2017, 01, 01), "should be January 1, 2017");
            sut[1].ShouldBe(new DateTime(2018, 01, 01), "should be January 1, 2018");
            sut[2].ShouldBe(new DateTime(2019, 01, 01), "should be January 1, 2019");
        }

        [Test]
        public void Yearly_OneParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 01), "should be November 1, 2016");
            sut[1].ShouldBe(new DateTime(2017, 11, 01), "should be November 1, 2017");
            sut[2].ShouldBe(new DateTime(2018, 11, 01), "should be November 1, 2018");
        }

        [Test]
        public void Yearly_TwoParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11,11")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 11), "should be November 11, 2016");
            sut[1].ShouldBe(new DateTime(2017, 11, 11), "should be November 11, 2017");
            sut[2].ShouldBe(new DateTime(2018, 11, 11), "should be November 11, 2018");
        }

        [Test]
        public void Yearly_ThreeParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11,11,3")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 11), "should be November 11, 2016");
            sut[1].ShouldBe(new DateTime(2019, 11, 11), "should be November 11, 2019");
            sut[2].ShouldBe(new DateTime(2022, 11, 11), "should be November 11, 2022");
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
