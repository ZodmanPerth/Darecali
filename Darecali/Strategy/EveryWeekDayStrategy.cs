using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public class EveryWeekDayStrategy : IRecurrenceStrategy
    {
        DateTime _currentDate;
        bool _hasMovedNext;

        public void SetStartDate(DateTime startDate)
        {
            _currentDate = startDate.Date;
            _hasMovedNext = false;
        }

        public DateTime GetNextDate()
        {
            if (!_hasMovedNext)
            {
                if (_currentDate.DayOfWeek == DayOfWeek.Saturday)
                    _currentDate = _currentDate.AddDays(2);
                else if (_currentDate.DayOfWeek == DayOfWeek.Sunday)
                    _currentDate = _currentDate.AddDays(1);
                _hasMovedNext = true;
            }
            else
                _currentDate = _currentDate.AddDays
                (
                    _currentDate.DayOfWeek == DayOfWeek.Friday ? 3 : 1
                );
            return _currentDate;
        }
    }
}
