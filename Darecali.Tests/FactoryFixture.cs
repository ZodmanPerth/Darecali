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

    }
}
