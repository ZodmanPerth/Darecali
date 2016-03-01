using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public enum Frequency
    {
        First = 1, Second, Third, Fourth, Last
    }

    public enum SpecialDay
    {
        //NOTE: Specific days align with DayOfWeek
        Sunday = 0, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday,

        Day,
        WeekDay,
        WeekendDay,
    }
}
