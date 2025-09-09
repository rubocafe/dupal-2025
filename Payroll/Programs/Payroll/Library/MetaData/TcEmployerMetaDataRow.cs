using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-02

namespace Payroll.Library.MetaData
{
    public class TcEmployerMetaDataRow : TcPropertyNameRow
    {
        public string Value { get; set; }

        public TcEmployerMetaDataRow(int index, string name, string value) 
            :base(index, name, TcMetaDataType.Text)
        {
            Value = value;
        }

        public static TcEmployerMetaDataRow New(int index, string name, string value)
        {
            TcEmployerMetaDataRow row = new TcEmployerMetaDataRow(index, name, value);

            return row;
        }
    }
}
