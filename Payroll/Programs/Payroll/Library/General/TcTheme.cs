using Payroll.Library.MetaData;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-08-08

namespace Payroll.Library.General
{
    public class TcTheme
    {
        // DataGridView
        private static Color DATAGRIDVIEW_GRID_COLOR = Color.Black;
        private static Color DATAGRIDVIEW_ALTERNATE_ROW_COLOR = Color.AliceBlue;

        public static void FormatGrid(DataGridView dataGridView)
        {
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView.GridColor = DATAGRIDVIEW_GRID_COLOR;
            dataGridView.RowTemplate.Height = 24;
            dataGridView.RowTemplate.DefaultCellStyle.Padding = new Padding(3);
            //dataGridView.RowTemplate.DefaultCellStyle.Alignment     = DataGridViewContentAlignment.MiddleLeft;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = DATAGRIDVIEW_ALTERNATE_ROW_COLOR;
        }

        public static void FormatGridCurrencyDisplayColumn(DataGridViewTextBoxColumn column)
        {
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Format = "N2";

            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public static void FormatGridDateDisplayColumn(DataGridViewTextBoxColumn column)
        {
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Format = "MMM dd, yyyy";

            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public static void FormatGridDateMonthDisplayColumn(DataGridViewTextBoxColumn column)
        {
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Format = "MMM yyyy";

            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public static void FormatGridNumberDisplayColumn(DataGridViewTextBoxColumn column)
        {
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        public static void FormatGridFullDateDisplayColumn(DataGridViewTextBoxColumn column)
        {
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            column.DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        public static void DisplayErrorLabel(Label label, string error)
        {
            label.ForeColor = Color.Brown;
            label.Font      = new Font(label.Font, FontStyle.Bold);
            label.Text      = error;
        }

        public static void DisplaySuccessLabel(Label label, string message)
        {
            label.ForeColor = Color.Green;
            label.Font = new Font(label.Font, FontStyle.Bold);
            label.Text = message;
        }

        public static void DisplayHighlightedLabel(Label label, string message)
        {
            label.ForeColor = Color.Black;
            label.Font = new Font(label.Font, FontStyle.Bold);
            label.Text = message;
        }

        public static void DisplayInfoLabel(Label label, string info)
        {
            label.ForeColor = SystemColors.ControlText;
            label.Font      = new Font(label.Font, FontStyle.Regular);
            label.Text      = info;
        }

        public static void SetLabelToDisplayBoldNormalText(Label label)
        {
            label.ForeColor = SystemColors.ControlText;
            label.Font = new Font(label.Font, FontStyle.Bold);
        }

        public static string GridColumnNameFromkey(string key)
        {
            var name = string.Format("{0}Column", key.Replace(" ", "_"));
            return name;
        }

        public static DataGridViewTextBoxColumn AddColumnToGrid(DataGridView grid, string key, 
            string name, string type, int width = 100, string format = null)
        {
            var columnName = GridColumnNameFromkey(key);
            var column = new DataGridViewTextBoxColumn();

            column.Name = columnName;
            column.HeaderText = name;
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            column.Width = width;
            column.MinimumWidth = width;

            if (type == TcMetaDataType.Text)
            {
                // Do nothing
            }
            else if (type == TcMetaDataType.Money)
            {
                FormatGridCurrencyDisplayColumn(column);
            }
            else if (type == TcMetaDataType.Date)
            {
                FormatGridDateDisplayColumn(column);
            }
            else if (type == TcMetaDataType.DateTime)
            {
                FormatGridFullDateDisplayColumn(column);
            }
            else
            {
                FormatGridNumberDisplayColumn(column);
            }

            if (format != null)
            {
                column.DefaultCellStyle.Format = format;
            }

            grid.Columns.Add(column);
            return column;
        }

        internal static DataGridViewTextBoxColumn AddEmptyColumn(DataGridView grid)
        {
            var columnName = GridColumnNameFromkey("EMPTY");
            var column = new DataGridViewTextBoxColumn();

            column.Name = columnName;
            column.HeaderText = "";
            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            grid.Columns.Add(column);
            return column;
        }

        internal static void AddIndexColumnToGrid(DataGridView grid)
        {
            var column = AddColumnToGrid(grid, "INDEX", "Index", TcMetaDataType.Number);
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
        }
    }
}
