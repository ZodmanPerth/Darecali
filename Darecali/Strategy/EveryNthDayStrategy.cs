using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public class EveryNthDayStrategy : IRecurrenceStrategy
    {
        DateTime _currentDate;
        int _n;
        bool _hasMovedNext;

        public EveryNthDayStrategy(int n = 1)
        {
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
                _currentDate = _currentDate.AddDays(_n);
            return _currentDate;
        }
    }
}
