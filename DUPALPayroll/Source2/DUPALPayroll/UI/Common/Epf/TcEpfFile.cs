using DUPALPayroll.Library;

// Harshan Nishantha
// 2013-12-23

namespace DUPALPayroll.UI.Common.Epf
{
    public class TcEpfFile
    {
        public TcBindingList<TcEpfRow> Rows { get; set; }

        public TcEpfFile()
        {
            Rows = new TcBindingList<TcEpfRow>();
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (TcEpfRow row in Rows)
            {
                total += row.TotalContribution;
            }

            return total;
        }
    }
}
