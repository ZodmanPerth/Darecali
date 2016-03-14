using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public class EveryNthMonthOnSpecifiedDayStrategy : IRecurrenceStrategy
    {
        DateTime _currentDate;
        int _day;
        int _n;
        bool _hasMovedNext;

        public EveryNthMonthOnSpecifiedDayStrategy(int day = 1, int n = 1)
        {
            if (day < 1 || day > 31) throw new ArgumentOutOfRangeException("day is outside the valid range");
            if (n < 1) throw new ArgumentOutOfRangeException("n must be a positive integer");

            _day = day;
            _n = n;
        }

        public void SetStartDate(DateTime startDate)
        {
            _currentDate = startDate.Date;
            _hasMovedNext = false;
        }

        public DateTime GetNextDate()
        {
            if (!_hasMovedNext)
                _hasMovedNext = true;
            else
                _currentDate = new DateTime(_currentDate.Year, _currentDate.Month, 1)
                    .AddMonths(_n);

            while (_currentDate.Day != _day)
                _currentDate = _currentDate.AddDays(1);
            return _currentDate;
        }
    }
}
