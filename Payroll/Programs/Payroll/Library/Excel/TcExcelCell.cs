using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.Excel
{
    public class TcExcelCell
    {
        public bool Exists { get; set; }
        public bool IsValid { get; set; }
        public object Value { get; set; }

        private static TcExcelCell emptyCell = null;

        public TcExcelCell(object value)
        {
            Value = value;
            Exists = true;
        }

        public static TcExcelCell Empty
        {
            get
            {
                if (emptyCell == null)
                {
                    emptyCell = new TcExcelCell("");
                    emptyCell.Exists = false;
                }

                return emptyCell;
            }
        }

        public string StringValue()
        {
            var value = default(string);

            if (Value != null)
            {
                value = Convert.ToString(Value);
            }
            IsValid = true;

            return value;
        }

        public double DoubleValue()
        {
            var value = default(double);

            if (Value != null)
            {
                try
                {
                    value = Convert.ToDouble(Value);
                    IsValid = true;
                }
                catch (Exception)
                {
                    IsValid = false;
                }
            }

            return value;
        }

        public int IntValue()
        {
            var value = default(int);

            if (Value != null)
            {
                try
                {
                    value = Convert.ToInt32(Value);
                    IsValid = true;
                }
                catch (Exception)
                {
                    IsValid = false;
                }
            }

            return value;
        }
    }
}
