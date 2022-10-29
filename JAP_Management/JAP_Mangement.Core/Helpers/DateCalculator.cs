using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Core.Helpers
{
    public static class DateCalculator
    {
        public static DateTime CalculateEndDate(DateTime date, int duration)
        {
            var workingDays = duration / 8;
            int i = 0;
            while (i != workingDays)
            {
                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    date = date.AddDays(2);
                    continue;
                }
                else if (date.DayOfWeek == DayOfWeek.Sunday)
                {
                    date = date.AddDays(1);
                    continue;
                }
                date = date.AddDays(1);
                i++;
            };
            if (date.DayOfWeek == DayOfWeek.Saturday)
                date = date.AddDays(-1);
            return date;
        }
    }
}
