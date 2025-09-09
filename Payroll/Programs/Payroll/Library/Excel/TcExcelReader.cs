using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-07

namespace Payroll.Library.Excel
{
    public class TcExcelReader
    {
        public TcExcel Excel { get; set; }
        public TcExcelTable Table { get; set; }
        public bool FileExists { get; set; }
        public bool TableExists { get; set; }
        public TcOperationState ColumnsState { get; set; }
        public TcOperationState State { get; set; }

        public TcExcelReader(string filePath, string sheetName, int tableIndex, List<string> columns, bool readFirstSheet = false)
        {
            Table = new TcExcelTable();
            Table.AddColumns(columns);

            if (File.Exists(filePath))
            {
                FileExists = true;
                Excel = new TcExcel(filePath, true);
                Excel.ReadSheet(sheetName, readFirstSheet);

                var table = Excel.GetDataTable(tableIndex);
                if (table != null)
                {
                    TableExists = true;
                    Table.Load(table);
                    ColumnsState = Table.HasAllColumns(table);
                }
            }
            else
            {
                FileExists = false;
                TableExists = false;
            }

            State = new TcOperationState();
            if (!FileExists)
            {
                var message = string.Format("File [{0}] does not exist", filePath);
                State.Succeeded = false;
                State.Message = message;
            }
            else if (!TableExists)
            {
                var message = string.Format("File [{0}] does not have any data", filePath);
                State.Succeeded = false;
                State.Message = message;
            }
            else
            {
                if (ColumnsState.Succeeded)
                {
                    State.Succeeded = true;
                    State.Message = "";
                }
                else
                {
                    var message = string.Format("File [{0}] does not have some required columns{1}{2}",
                    filePath, Environment.NewLine, ColumnsState.Message);
                    State.Succeeded = false;
                    State.Message = message;
                }
            }
        }
    }
}
