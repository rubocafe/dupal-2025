using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-02

namespace Payroll.Library.MetaData
{
    public class TcMasterMetaDataRow : TcPropertyNameRow
    {
        public TcMasterMetaDataRow(int index, string name, string type) 
            : base(index, name, type)
        {
        }

        public static TcMasterMetaDataRow New(int index, string name, string type)
        {
            TcMasterMetaDataRow row = new TcMasterMetaDataRow(index, name, type);

            return row;
        }
    }
}
