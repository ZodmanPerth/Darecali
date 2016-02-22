using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public class EveryNthWeekOnSpecificDaysStrategy : IRecurrenceStrategy
    {
        DateTime _currentDate;
        int _n;
        bool _hasMovedNext;
        List<DayOfWeek> _daysOfWeek;
        DayOfWeek _startDay;

        public EveryNthWeekOnSpecificDaysStrategy(DayOfWeekFlags daysOfWeek, int n = 1)
        {
            _n = n;
            _daysOfWeek = GetDaysOfWeek(daysOfWeek).ToList();
        }

        IEnumerable<DayOfWeek> GetDaysOfWeek(DayOfWeekFlags daysOfWeek)
        {
            for (int i = 0; i < 7; i++)
                if (((int)daysOfWeek & (int)Math.Pow(2, i)) != 0)
                    yield return (DayOfWeek)i;
        }

        public void SetStartDate(DateTime startDate)
        {
            _currentDate = startDate.Date;
            _startDay = _currentDate.DayOfWeek;
            _hasMovedNext = false;
        }

        public DateTime GetNextDate()
        {
            if (_hasMovedNext || !IsMatch())
                do
                {
                    _currentDate = _currentDate.AddDays(1);
                    if (_currentDate.DayOfWeek == _startDay)
                        _currentDate = _currentDate.AddDays((_n - 1) * 7);
                } while (!IsMatch());

            _hasMovedNext = true;
            return _currentDate;
        }

        bool IsMatch()
        {
            return _daysOfWeek.Contains(_currentDate.DayOfWeek);
        }
    }

    [Flags]
    public enum DayOfWeekFlags
    {
        //NOTE: Powers of two where the power aligns with DayOfWeek

        Sunday = 1 << 0,
        Monday = 1 << 1,
        Tuesday = 1 << 2,
        Wednesday = 1 << 3,
        Thursday = 1 << 4,
        Friday = 1 << 5,
        Saturday = 1 << 6,

        WeekDays = Monday | Tuesday | Wednesday | Thursday | Friday,
        WeekendDays = Saturday | Sunday,
        EveryDay = WeekDays | WeekendDays,
    }
}
