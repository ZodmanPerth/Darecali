using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public class EveryDayOMonthEveryNthMonthStrategy : IRecurrenceStrategy
    {
        DateTime _currentDate;
        int _dayOfMonth;
        int _n;
        bool _hasMovedNext;

        public EveryDayOMonthEveryNthMonthStrategy(int dayOfMonth = 1, int n = 1)
        {
            if (dayOfMonth < 1 || dayOfMonth > 31)
                throw new ArgumentOutOfRangeException("dayOfMonth is outside of the valid range");
            if (n< 1 )
                throw new ArgumentOutOfRangeException("n must be a positive integer");

            _dayOfMonth = dayOfMonth;
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
            {
                _currentDate = new DateTime(_currentDate.Year, _currentDate.Month, 1);
                _currentDate = _currentDate.AddMonths(_n);
            }

            while (_currentDate.Day != _dayOfMonth)
                _currentDate = _currentDate.AddDays(1);
            return _currentDate;
        }
    }
}
