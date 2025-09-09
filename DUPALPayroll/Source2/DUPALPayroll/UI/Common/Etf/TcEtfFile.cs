using DUPALPayroll.Library;

// Harshan Nishantha
// 2013-12-23

namespace DUPALPayroll.UI.Common.Etf
{
    public class TcEtfFile
    {
        public TcBindingList<TcEtfDetailRow> Rows { get; set; }
        public TcEtfHeaderRow HeaderRow { get; set; }

        public TcEtfFile()
        {
            Rows = new TcBindingList<TcEtfDetailRow>();
            HeaderRow  = null;
        }

        public void GenerateHeaderRow()
        {
            if (Rows.Count > 0)
            {
                TcEtfDetailRow topRow = Rows[0];
                HeaderRow = new TcEtfHeaderRow();

                HeaderRow.EmployerNumber = topRow.EmployerNumber;
                HeaderRow.From           = topRow.From;
                HeaderRow.To             = topRow.To;
                HeaderRow.TotalMembers   = Rows.Count;

                decimal total = GetTotal();
                HeaderRow.TotalContribution = total;
            }
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (TcEtfDetailRow row in Rows)
            {
                total += row.TotalContribution;
            }

            return total;
        }
    }
}
