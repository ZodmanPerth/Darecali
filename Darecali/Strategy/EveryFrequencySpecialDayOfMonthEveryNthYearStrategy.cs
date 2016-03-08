using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public class EveryFrequencySpecialDayOfMonthEveryNthYearStrategy : IRecurrenceStrategy
    {
        DateTime _currentDate;
        Frequency _frequency;
        SpecialDay _specialDay;
        int _month;
        int _n;

        public EveryFrequencySpecialDayOfMonthEveryNthYearStrategy(Frequency frequency = Frequency.First, SpecialDay specialDay = SpecialDay.WeekDay, int month = 1, int n = 1)
        {
            if (!Enum.IsDefined(typeof(Frequency), frequency)) throw new ArgumentException("frequency");
            if (!Enum.IsDefined(typeof(SpecialDay), specialDay)) throw new ArgumentException("specialDay");
            if (month < 1 || month > 12) throw new ArgumentOutOfRangeException("month");
            if (n < 1) throw new ArgumentOutOfRangeException("n must be a positive integer");

            _frequency = frequency;
            _specialDay = specialDay;
            _month = month;
            _n = n;
        }

        public void SetStartDate(DateTime startDate)
        {
            _currentDate = startDate.Date;
        }

        public DateTime GetNextDate()
        {
            int frequency = _frequency == Frequency.Last ? 99 : (int)_frequency;
            int occurrence = 0;
            DateTime? lastMatch = null;

            var thisYear = new DateTime(_currentDate.Year, _month, 1);
            if (_currentDate < thisYear)
                _currentDate = thisYear;
            else if (_currentDate.Month != _month)
                _currentDate = new DateTime(_currentDate.Year + _n, _month, 1);

            do
            {
                do
                {
                    while (!IsMatch() && _currentDate.Month == _month)
                        _currentDate = _currentDate.AddDays(1);

                    if (IsMatch())
                    {
                        occurrence += 1;
                        if (occurrence == frequency)
                            lastMatch = _currentDate.Date;
                        else
                        {
                            if (_frequency == Frequency.Last)
                                lastMatch = _currentDate.Date;
                            _currentDate = _currentDate.AddDays(1);
                        }
                    }
                } while (occurrence != frequency && _currentDate.Month == _month);

                if (lastMatch.HasValue)
                {
                    if (_currentDate.Month == _month)
                        _currentDate = _currentDate.AddDays(1 - _currentDate.Day)
                            .AddYears(_n);

                    return lastMatch.Value;
                }

                occurrence = 0;
                _currentDate = new DateTime(_currentDate.Year + _n, _month, 1);

            } while (true);
        }

        bool IsMatch()
        {
            switch (_specialDay)
            {
                case SpecialDay.Day:
                    return true;
                case SpecialDay.WeekDay:
                    return _currentDate.DayOfWeek != DayOfWeek.Saturday && _currentDate.DayOfWeek != DayOfWeek.Sunday;
                case SpecialDay.WeekendDay:
                    return _currentDate.DayOfWeek == DayOfWeek.Saturday || _currentDate.DayOfWeek == DayOfWeek.Sunday;
                default:
                    return _currentDate.DayOfWeek == (DayOfWeek)_specialDay;
            }
        }
    }
}
