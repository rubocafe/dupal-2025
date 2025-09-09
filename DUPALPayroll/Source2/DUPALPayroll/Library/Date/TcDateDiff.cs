using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-10-14

namespace DUPALPayroll.Library.Date
{
    public class TcDateDiff
    {
        private DateTime startDate;
        private DateTime endDate;

        public int Years { get; private set; }
        public int Months { get; private set; }
        public int Days { get; private set; }

        public TcDateDiff(DateTime startDate, DateTime endDate)
        {
            this.startDate  = startDate;
            this.endDate    = endDate;
            Calculate();
        }

        private void Calculate()
        {
            bool startIsGreaterThanEnd = false;

            Years = 0;
            Months = 0;
            Days = 0;

            DateTime start = startDate; 
            DateTime end = endDate;

            if (startDate > endDate)
            {
                startIsGreaterThanEnd = true;

                start = endDate;
                end = startDate;
            }

            if (end.Day >= start.Day)
            {
                Days = end.Day - start.Day;
            }
            else
            {
                end = end.AddMonths(-1);
                Days = (end.Day + DateTime.DaysInMonth(end.Year, end.Month)) - start.Day;
            }

            if (end.Month >= start.Month)
            {
                Months = end.Month - start.Month;
            }
            else
            {
                end = end.AddYears(-1);
                Months = (end.Month + 12) - start.Month;
            }

            Years = end.Year - start.Year;

            if (startIsGreaterThanEnd)
            {
                Years *= (-1);
                Months *= (-1);
                Days *= (-1);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}", Years.ToString().PadLeft(2, '0'), Months.ToString().PadLeft(2, '0'), Days.ToString().PadLeft(2, '0'));
        }
    }
}
