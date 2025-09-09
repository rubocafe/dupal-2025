using Payroll.Library.Excel;
using Payroll.Library.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-13

namespace Payroll.UI.Common
{
    public abstract class TcBaseRow
    {
        public int LineNumber { get; set; }
        public Dictionary<string, object> Data { get; set; }

        public abstract void LoadToVariables();

        public TcBaseRow()
        {
            Data = new Dictionary<string, object>();
        }

        public void Set(string key, object value)
        {
            if (Data.ContainsKey(key))
            {
                Data[key] = value;
            }
            else
            {
                Data.Add(key, value);
            }
        }

        public object Get(string key)
        {
            object value = null;

            if (Data.ContainsKey(key))
            {
                value = Data[key];
            }

            return value;
        }

        public void LoadFrom(int index, TcExcelTableRow row)
        {
            LineNumber = index;
            foreach (var cell in row.Cells)
            {
                var key = cell.Key;
                if (!string.IsNullOrEmpty(key))
                {
                    key = key.ToUpper();
                }

                var value = cell.Value.Value;
                Set(key, value);
            }

            LoadToVariables();
        }

        public object GetValue(string key, string type)
        {
            object value = Get(key);
            if (value != null)
            {
                var temp = value.ToString();
                if (type == TcMetaDataType.Number)
                {
                    int result = 0;
                    if (int.TryParse(temp, out result))
                    {
                        return result;
                    }
                }
                else if (type == TcMetaDataType.BigNumber)
                {
                    long result = 0;
                    if (long.TryParse(temp, out result))
                    {
                        return result;
                    }
                }
                else if (type == TcMetaDataType.Money)
                {
                    decimal result = 0;
                    if (decimal.TryParse(temp, out result))
                    {
                        return result;
                    }
                }
                else if (type == TcMetaDataType.Decimal)
                {
                    double result = 0;
                    if (double.TryParse(temp, out result))
                    {
                        return result;
                    }
                }
                else if (type == TcMetaDataType.Date || type == TcMetaDataType.DateTime)
                {
                    DateTime result = DateTime.MinValue;
                    if (DateTime.TryParse(temp, out result))
                    {
                        return result;
                    }
                }
                else
                {
                    return temp;
                }
            }

            return null;
        }
    }
}
