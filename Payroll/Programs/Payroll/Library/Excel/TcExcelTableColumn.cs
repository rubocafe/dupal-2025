using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.Excel
{
    public class TcExcelTableColumn
    {
        public string Name { get; set; }

        public TcExcelTableColumn(string name)
        {
            Name = name;
        }
    }
}
