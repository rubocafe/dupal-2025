using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-04

namespace Payroll.Library.Excel
{
    public class TcExcelValueDecorder
    {
        public static string GetString(object value)
        {
            string result = null;

            if (value != null)
            {
                result = value.ToString();
            }

            return result;
        }

        public static int GetInt(object value)
        {
            int result = 0;

            if (value != null)
            {
                var stringValue = value.ToString();
                int.TryParse(stringValue, out result);
            }

            return result;
        }

        public static bool GetBool(object value)
        {
            bool result = false;

            if (value != null)
            {
                var stringValue = value.ToString();
                if (stringValue.Equals("TRUE", StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                }
            }

            return result;
        }

        public static decimal GetDecimal(object value)
        {
            decimal result = 0;

            if (value != null)
            {
                var stringValue = value.ToString();
                decimal.TryParse(stringValue, out result);
            }

            return result;
        }

        public static double GetDouble(object value)
        {
            double result = 0;

            if (value != null)
            {
                var stringValue = value.ToString();
                double.TryParse(stringValue, out result);
            }

            return result;
        }

        public static Nullable<DateTime> GetDate(object value)
        {
            Nullable<DateTime> result = null;

            if (value != null)
            {
                DateTime temp = DateTime.Now;
                var stringValue = value.ToString();
                if (DateTime.TryParse(stringValue, out temp))
                {
                    result = temp;
                }
            }

            return result;
        }
    }
}
