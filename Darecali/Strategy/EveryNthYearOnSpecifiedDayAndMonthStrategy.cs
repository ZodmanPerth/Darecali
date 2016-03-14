using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public class EveryNthYearOnSpecifiedDayAndMonthStrategy : IRecurrenceStrategy
    {
        DateTime _currentDate;
        int _n;
        int _month;
        int _day;
        bool _hasMovedNext;
        bool _isFebruary29th;

        public EveryNthYearOnSpecifiedDayAndMonthStrategy(int month = 1, int day = 1, int n = 1)
        {
            if (month < 1 || month > 12) throw new ArgumentOutOfRangeException("month is outside the valid range");
            if (day < 1 || day > 31) throw new ArgumentOutOfRangeException("day is outside the valid range");
            if (n < 1) throw new ArgumentOutOfRangeException("n must be a positive integer");

            //GUARD: Ensure the day/month combination is valid, throws otherwise
            var date = new DateTime(2016, month, day);

            _isFebruary29th = month == 2 && day == 29;

            _n = n;
            _month = month;
            _day = day;
        }

        public void SetStartDate(DateTime startDate)
        {
            _currentDate = startDate.Date;
            _hasMovedNext = false;
        }

        public DateTime GetNextDate()
        {
            if (_isFebruary29th)
            {
                if (!_hasMovedNext)
                {
                    if
                    (
                        !DateTime.IsLeapYear(_currentDate.Year)
                        || _currentDate.Month > _month
                        || (_currentDate.Month == _month && _currentDate.Day > _day)
                    )
                        do
                            _currentDate = _currentDate.AddYears(1);
                        while (!DateTime.IsLeapYear(_currentDate.Year));

                    _currentDate = new DateTime(_currentDate.Year, 2, 29);
                    _hasMovedNext = true;
                    return _currentDate;
                }

                do
                    _currentDate = _currentDate.AddYears(_n);
                while (!DateTime.IsLeapYear(_currentDate.Year));
                _currentDate = new DateTime(_currentDate.Year, 2, 29);
                return _currentDate;
            }
            else
            {
                if (!_hasMovedNext)
                {
                    var thisYearDate = new DateTime(_currentDate.Year, _month, _day);
                    _currentDate = _currentDate <= thisYearDate
                        ? thisYearDate
                        : thisYearDate.AddYears(_n);

                    _hasMovedNext = true;
                    return _currentDate;
                }

                _currentDate = _currentDate.AddYears(_n);
                return _currentDate;
            }
        }
    }
}
