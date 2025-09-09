using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.Excel
{
    public class TcExcelTableRow
    {
        public Dictionary<string, TcExcelCell> Cells { get; set; }

        public TcExcelTableRow()
        {
            Cells = new Dictionary<string, TcExcelCell>();
        }

        public void AddCell(string columnName, TcExcelCell newCell)
        {
            if (!Cells.ContainsKey(columnName))
            {
                Cells.Add(columnName, newCell);
            }
        }

        public TcExcelCell GetCell(string columnName)
        {
            if (Cells.ContainsKey(columnName))
            {
                return Cells[columnName];
            }

            return TcExcelCell.Empty;
        }
    }
}
