using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darecali.Strategy
{
    public interface IRecurrenceStrategy
    {
        void SetStartDate(DateTime startDate);
        DateTime GetNextDate();
    }
}
