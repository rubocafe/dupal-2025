using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.Date
{
    public class TcYearMonth
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public TcYearMonth()
        {
            DateTime now = DateTime.Now;

            Year    = now.Year;
            Month   = now.Month;
        }

        public static TcYearMonth OfNow()
        {
            TcYearMonth yearMonth = new TcYearMonth();

            return yearMonth;
        }

        public static TcYearMonth OfLastMonth()
        {
            TcYearMonth yearMonth = new TcYearMonth();
            DateTime lastMonth = DateTime.Now.AddMonths(-1);

            yearMonth.Year  = lastMonth.Year;
            yearMonth.Month = lastMonth.Month;

            return yearMonth;
        }

        public static TcYearMonth OfDateTime(DateTime dateTime)
        {
            TcYearMonth yearMonth = new TcYearMonth();

            yearMonth.Year  = dateTime.Year;
            yearMonth.Month = dateTime.Month;

            return yearMonth;
        }

        public static TcYearMonth LoadSafelyFromText(string text)
        {
            TcYearMonth yearMonth = OfNow();
            yearMonth.LoadFromText(text);

            return yearMonth;
        }

        public bool LoadFromText(string text)
        {
            if (!string.IsNullOrEmpty(text) &&
                text.Length == 7)
            {
                string yearString   = text.Substring(0, 4);
                string monthString  = text.Substring(5, 2);

                int year;
                int month;
                if (int.TryParse(yearString, out year) &&
                    int.TryParse(monthString, out month))
                {
                    if (month > 0 && month <= 12)
                    {
                        Year = year;
                        Month = month;

                        return true;
                    }
                }
            }

            return false;
        }

        public override string ToString()
        {
            string value = ToDate().ToString("yyyy-MM");

            return value;
        }

        public DateTime ToDate()
        {
            DateTime date = new DateTime(Year, Month, 1);

            return date;
        }
    }
}
