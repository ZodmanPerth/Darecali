using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Tests
{
    [Category("RecurrenceController")]
    public class RecurrenceControllerFixture
    {
        [Test]
        public void TerminatesAtEndDate()
        {
            var endDateOffset = 3;
            var sut = Factory.CreateController(DateTime.Today, "D", DateTime.Today.AddDays(endDateOffset))
                .ToList();
            sut.Count.ShouldBe(endDateOffset + 1, string.Format("Only expecting {0} dates", endDateOffset + 1));
            sut.Last().ShouldBe(DateTime.Today.AddDays(endDateOffset));
        }

        [Test]
        public void TerminatesAfter10Occurrences()
        {
            var numberOfOccurrences = 10;
            var sut = Factory.CreateController(DateTime.Today, "D", numberOfOccurrences: numberOfOccurrences)
                .ToList();
            sut.Count.ShouldBe(numberOfOccurrences, string.Format("Only expecting {0} dates", numberOfOccurrences));
        }

        [Test]
        public void TerminatesDueToOccurrencesOverEndDate()
        {
            var endDate = DateTime.Today.AddDays(100);
            var numberOfOccurrences = 10;
            var sut = Factory.CreateController(DateTime.Today, "D", endDate, numberOfOccurrences)
                .ToList();

            sut.Count.ShouldBe(numberOfOccurrences, string.Format("Only expecting {0} dates", numberOfOccurrences));
            sut.Last().ShouldBeLessThan(endDate, "endDate should not have been reached");
        }

        [Test]
        public void TerminatesDueToEndDateOverOccurrences()
        {
            var endDate = DateTime.Today.AddDays(3);
            var numberOfOccurrences = 10;
            var sut = Factory.CreateController(DateTime.Today, "D", endDate, numberOfOccurrences)
                .ToList();

            sut.Last().ShouldBe(endDate, "endDate should be last");
            sut.Count.ShouldBeLessThan(numberOfOccurrences, string.Format("Expecting less than {0} dates", numberOfOccurrences));
        }
    }
}
