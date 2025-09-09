using Payroll.General;
using Payroll.UI.Business.Analyze;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ruchira Bomiriya
// 2016/09/24

namespace Payroll.Library.Payments
{
    public class TcBankMemberData
    {
        public int LineNumber { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string NameWithInitials { get; set; }
        public string NIC { get; set; }
        public decimal Amount { get; set; }
        public DateTime ValueDate { get; set; }

        public TcBankMemberData(TcBusinessAnalyzedRow row)
        {
            LineNumber = row.LineNumber;
            BankCode = row.BankCode;
            BranchCode = row.BranchCode;
            AccountNumber = row.AccountNumber;
            NameWithInitials = row.NameWithInitials;
            NIC = row.NIC;
            Amount = row.BankTransferAmount;
            ValueDate = DateTime.Now;
        }

        public bool IsValid()
        {
            bool isValid = false;

            if (TcValidator.IsValidBankCode(BankCode) &&
                TcValidator.IsValidBranchCode(BranchCode) &&
                TcValidator.IsValidBankAccountNumber(AccountNumber))
            {
                isValid = true;
            }

            return isValid;
        }
    }
}
