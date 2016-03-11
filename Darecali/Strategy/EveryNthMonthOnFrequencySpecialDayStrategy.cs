using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public class EveryNthMonthOnFrequencySpecialDayStrategy : IRecurrenceStrategy
    {
        DateTime _currentDate;
        Frequency _frequency;
        SpecialDay _specialDay;
        int _n;

        public EveryNthMonthOnFrequencySpecialDayStrategy(Frequency frequency = Frequency.First, SpecialDay specialDay = SpecialDay.WeekDay, int n = 1)
        {
            if (!Enum.IsDefined(typeof(Frequency), frequency)) throw new ArgumentException("frequency");
            if (!Enum.IsDefined(typeof(SpecialDay), specialDay)) throw new ArgumentException("specialDay");
            if (n < 1) throw new ArgumentOutOfRangeException("n must be a positive integer");

            _frequency = frequency;
            _specialDay = specialDay;
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
            int currentMonth = _currentDate.Month;
            DateTime? lastMatch = null;

            do
            {
                do
                {
                    while (!IsMatch() && _currentDate.Month == currentMonth)
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
                } while (occurrence != frequency && _currentDate.Month == currentMonth);

                if (lastMatch.HasValue)
                {
                    if (_currentDate.Month == currentMonth)
                        _currentDate = _currentDate.AddDays(1 - _currentDate.Day)
                            .AddMonths(_n);

                    return lastMatch.Value;
                }

                occurrence = 0;
                currentMonth = _currentDate.Month;

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
