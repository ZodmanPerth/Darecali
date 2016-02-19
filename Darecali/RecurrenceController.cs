using Darecali.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Darecali
{
    public class RecurrenceController : IEnumerable<DateTime>
    {
        DateTime _startDate;
        IRecurrenceStrategy _strategy;
        DateTime? _endDate;
        int? _numberOfOccurrences;

        public RecurrenceController(DateTime startDate, IRecurrenceStrategy strategy, DateTime? endDate = null, int? numberOfOccurrences = null)
        {
            if (strategy == null) throw new ArgumentNullException("strategy");
            if (numberOfOccurrences <= 0) throw new ArgumentOutOfRangeException("numberOfOccurrences");

            _startDate = startDate.Date;
            _strategy = strategy;
            _endDate = endDate.HasValue ? endDate.Value.Date : (DateTime?)null;
            _numberOfOccurrences = numberOfOccurrences;

            _strategy.SetStartDate(startDate);
        }

        public IEnumerator<DateTime> GetEnumerator()
        {
            if (_numberOfOccurrences.HasValue)
                for (int i = 0; i < _numberOfOccurrences; i++)
                {
                    var nextDate = _strategy.GetNextDate();
                    if (_endDate.HasValue && nextDate > _endDate)
                        break;
                    yield return nextDate;
                }
            else
                while (true)
                {
                    var nextDate = _strategy.GetNextDate();
                    if (_endDate.HasValue && nextDate > _endDate)
                        break;
                    yield return nextDate;
                }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
