using Darecali.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali
{
    public class RecurrenceController
    {
        DateTime _startDate;
        IRecurrenceStrategy _strategy;
        DateTime? _endDate;
        int? _numberOfOccurrences;

        public RecurrenceController(DateTime startDate, IRecurrenceStrategy strategy, DateTime? endDate = null, int? numberOfOccurrences = null)
        {
            if (strategy == null) throw new ArgumentNullException("strategy");
            if (numberOfOccurrences <= 0) throw new ArgumentOutOfRangeException("numberOfOccurrences");

            _startDate = startDate;
            _strategy = strategy;
            _endDate = endDate;
            _numberOfOccurrences = numberOfOccurrences;

            _strategy.SetStartDate(startDate);
        }

        public DateTime GetNextDate()
        {
            return _strategy.GetNextDate();
        }
    }
}
