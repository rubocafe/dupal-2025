using DUPALPayroll.General;
using DUPALPayroll.Library;
using System.Windows.Forms;

// Harshan Nishantha
// 2013-11-19

namespace DUPALPayroll.UI.Common.PayMaster
{   
    public class TcPayMasterRowsValidator<T> where T : TiPayMasterDestination
    {
        public TcPayMasterOriginData Origin { get; set; }
        public TcBindingList<T> PaymasterDataList { get; set; }
        public TcBindingList<T> ValidRows { get; set; }
        public TcBindingList<T> InvalidRows { get; set; }
        public decimal Total { get; set; }

        public TcPayMasterRowsValidator(TcPayMasterOriginData originData, TcBindingList<T> paymasterDataList)
        {
            Origin = originData;
            this.PaymasterDataList = paymasterDataList;

            ValidRows = new TcBindingList<T>();
            InvalidRows = new TcBindingList<T>();
            Total = 0;
        }

        public void Validate()
        {
            ValidRows.Clear();
            InvalidRows.Clear();
            Total = 0;

            foreach (T data in PaymasterDataList)
            {
                TcPayMasterDestinationData destination = data.GetPayMasterDestinationData();
                TcPayMasterRow row = TcPayMasterRow.GetCreditRow(Origin, destination);

                if (IsValidRow(row))
                {
                    Total += TcDecimal.GetDecimalFromText(row.Amount, 2);
                    ValidRows.Add(data);
                }
                else
                {
                    InvalidRows.Add(data);
                }
            }
        }

        private bool IsValidRow(TcPayMasterRow row)
        {
            if (TcValidator.IsValidBankCode(row.DestinationBank) &&
                TcValidator.IsValidBranchCode(row.DestinationBranch) &&
                TcValidator.IsValidBankAccountNumber(row.DestinationAccount))
            {
                return true;
            }

            return false;
        }

        public void SetDisplaySummaryLabel(Label label)
        {
            label.Text = string.Format("Bank Payment Total: {0}", Total.ToString("N2"));
        }
    }
}
