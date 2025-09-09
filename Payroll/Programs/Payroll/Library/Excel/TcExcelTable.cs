using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.Library.Excel
{
    public class TcExcelTable
    {
        public List<TcExcelTableColumn> Columns { get; set; }
        public List<TcExcelTableRow> Rows { get; set; }

        private Dictionary<string, TcExcelTableColumn> columnIndex = new Dictionary<string, TcExcelTableColumn>();

        public TcExcelTable()
        {
            Columns = new List<TcExcelTableColumn>();
            Rows = new List<TcExcelTableRow>();
        }

        public void AddColumn(string columnName)
        {
            var column = new TcExcelTableColumn(columnName);
            if (!columnIndex.ContainsKey(columnName))
            {
                columnIndex.Add(column.Name, column);
                Columns.Add(column);
            }
        }

        public void Load(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                var newRow = new TcExcelTableRow();
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    var columnName = table.Columns[j].Caption;
                    var value = table.Rows[i][j];

                    var newCell = new TcExcelCell(value);
                    newRow.AddCell(columnName, newCell);
                }

                Rows.Add(newRow);
            }
        }

        public HashSet<string> GetColumnsNotFound(DataTable table)
        {
            HashSet<string> columnsNotFound = new HashSet<string>();

            foreach (var expectedColumn in Columns)
            {
                if (!table.Columns.Contains(expectedColumn.Name))
                {
                    columnsNotFound.Add(expectedColumn.Name);
                }
            }

            return columnsNotFound;
        }

        public TcOperationState HasAllColumns(DataTable table)
        {
            TcOperationState state = new TcOperationState();

            var notFound = GetColumnsNotFound(table);
            if (notFound.Count > 0)
            {
                var line = "";
                foreach (var item in notFound)
                {
                    line = string.Format("{0}{1},", line, item);
                }
                line = line.TrimEnd(",".ToCharArray());

                state.Succeeded = false;
                state.Message = line;
            }
            else
            {
                state.Succeeded = true;
                state.Message = "";
            }

            return state;
        }

        public void AddColumns(List<string> columns)
        {
            foreach (var column in columns)
            {
                AddColumn(column);
            }
        }
    }
}
