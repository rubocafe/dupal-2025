using Payroll.General;
using Payroll.Library;
using Payroll.Library.General;
using Payroll.UI.Controls;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-11-19

namespace Payroll.Library.Payments
{   
    public class TcPaymentsValidator<T> where T : TiMemberData
    {
        public TcEmployerData Employer { get; set; }
        public TcBindingList<T> Rows { get; set; }
        public TcBindingList<T> ValidRows { get; set; }
        public TcBindingList<T> InvalidRows { get; set; }
        public decimal Total { get; set; }

        public TcPaymentsValidator(TcEmployerData employer, TcBindingList<T> rows)
        {
            Employer = employer;
            Rows = rows;

            ValidRows = new TcBindingList<T>();
            InvalidRows = new TcBindingList<T>();
            Total = 0;
        }

        public void Validate()
        {
            ValidRows.Clear();
            InvalidRows.Clear();
            Total = 0;

            foreach (T row in Rows)
            {
                TcBankMemberData memberData = row.BankMemberData();

                if (memberData.IsValid())
                {
                    Total += decimal.Round(memberData.Amount, 2);
                    ValidRows.Add(row);
                }
                else
                {
                    InvalidRows.Add(row);
                }
            }
        }

        public void SetDisplaySummaryLabel(Label label)
        {
            label.Text = string.Format("Bank Payment Total: {0}", Total.ToString("N2"));
        }
    }
}
