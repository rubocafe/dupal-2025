using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Harshan Nishantha
// 2015-11-03

namespace Payroll.Library.MetaData
{
    public class TcSalaryMetaDataRow : TcPropertyNameRow
    {
        public string Category { get; set; }

        public TcSalaryMetaDataRow(int index, string name, string category, string type)
            : base(index, name, type)
        {
            Category = category;
        }

        public static TcSalaryMetaDataRow New(int index, string name, string category, string type)
        {
            TcSalaryMetaDataRow row = new TcSalaryMetaDataRow(index, name, category, type);

            return row;
        }
    }
}
