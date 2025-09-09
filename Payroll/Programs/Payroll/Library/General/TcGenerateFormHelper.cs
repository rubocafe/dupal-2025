using Payroll.Library.Payments;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-11-20

namespace Payroll.Library.General
{
    public class TcGenerateFormHelper
    {
        public static void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView grid = sender as DataGridView;

            BindingSource source = grid.DataSource as BindingSource;
            if (source != null)
            {
                List<TcMandatoryCondition> list = source.DataSource as List<TcMandatoryCondition>;
                if (list != null)
                {
                    TcMandatoryCondition condition = list[e.RowIndex];
                    DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];

                    switch (condition.RowDisplayType)
                    {
                        case TeRowDisplaytype.Normal:
                            cell.Style.ForeColor = Color.Black;
                            break;
                        case TeRowDisplaytype.Error:
                            cell.Style.ForeColor = Color.Brown;
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
