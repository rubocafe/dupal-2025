using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-08-26

namespace Payroll.Library.Csv
{
    public class TcCsvValueDecorder
    {
        public static int GetInt(string value)
        {
            int result = 0;

            if (!string.IsNullOrEmpty(value))
            {
                int.TryParse(value, out result);
            }

            return result;
        }

        public static decimal GetDecimal(string value)
        {
            decimal result = 0;

            if (!string.IsNullOrEmpty(value))
            {
                decimal.TryParse(value, out result);
            }

            return result;
        }

        public static Nullable<DateTime> GetDate(string value)
        {
            Nullable<DateTime> result = null;

            if (!string.IsNullOrEmpty(value))
            {
                DateTime temp = DateTime.Now;
                if (DateTime.TryParse(value, out temp))
                {
                    result = temp;
                }
            }

            return result;
        }
    }
}
