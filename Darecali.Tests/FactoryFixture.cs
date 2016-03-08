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

        #region Every Nth Year tests

        [Test]
        public void NthYear_NoParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2017, 01, 01), "should be January 1, 2017");
            sut[1].ShouldBe(new DateTime(2018, 01, 01), "should be January 1, 2018");
            sut[2].ShouldBe(new DateTime(2019, 01, 01), "should be January 1, 2019");
        }

        [Test]
        public void NthYear_OneParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 01), "should be November 1, 2016");
            sut[1].ShouldBe(new DateTime(2017, 11, 01), "should be November 1, 2017");
            sut[2].ShouldBe(new DateTime(2018, 11, 01), "should be November 1, 2018");
        }

        [Test]
        public void NthYear_TwoParamTest()
        {
            var startDate = new DateTime(2016, 03, 03);
            var sut = Factory.CreateController(startDate, "Y11,11")
                .Take(3).ToList();
            sut[0].ShouldBe(new DateTime(2016, 11, 11), "should be November 11, 2016");
            sut[1].ShouldBe(new DateTime(2017, 11, 11), "should be November 11, 2017");
            sut[2].ShouldBe(new DateTime(2018, 11, 11), "should be November 11, 2018");
        }

        [Test]
        public void NthYear_ThreeParamTest()
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
    }
}
