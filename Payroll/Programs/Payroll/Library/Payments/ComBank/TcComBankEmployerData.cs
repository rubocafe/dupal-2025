using Payroll.Library;
using Payroll.Library.General;
using System;

// Harshan Nishantha
// 2013-09-17

namespace Payroll.Library.Payments.ComBank
{
    public class TcComBankEmployerData
    {
        public string OriginatingBank;
        public string OriginatingBranch;
        public string OriginatingAccount;
        public string OriginatingAccountName;
        public string Particulars;
        public string Reference;
        public DateTime ValueDate;

        public TcComBankEmployerData(TcEmployerData employer)
        {
            OriginatingBank = "7056";
            OriginatingBranch = employer.BranchCode;
            OriginatingAccount = employer.AccountNumber;
            OriginatingAccountName = employer.AccountName;
            Particulars = "0";
            Reference = employer.Remarks;
            ValueDate = employer.ValueDate;
        }
    }
}
