using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-04

namespace Payroll.Library.MetaData
{
    public class TcPropertyNameRow
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public TcPropertyNameRow(int index, string name, string type)
        {
            Index = index;
            Name = name;
            Type = type;
        }

        public string UpperName
        {
            get
            {
                string upperName = null;
                if (Name != null)
                {
                    upperName = Name.ToUpper();
                }

                return upperName;
            }
        }

        public string PropertyName
        {
            get
            {
                string propertyName = null;
                if (UpperName != null)
                {
                    propertyName = UpperName.Replace(" ", "");
                }

                return propertyName;
            }
        }
    }
}
