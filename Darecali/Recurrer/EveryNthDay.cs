using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Recurrer
{
    public class EveryNthDay
    {
        DateTime _currentDate;
        int _n;
        bool _hasMovedNext;

        public EveryNthDay(DateTime startDate, int n = 1)
        {
            _currentDate = startDate;
            _n = n;
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
