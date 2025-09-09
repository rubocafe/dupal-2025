using Payroll.Library;
using Payroll.Library.General;
using System;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.Library.Payments.ComBank
{
    public class TcComBankMemberData
    {
        public int LineNumber { get; set; }

        public string DestinationBank { get; set; }
        public string DestinationBranch { get; set; }
        public string DestinationAccount { get; set; }
        public string DestinationAccountName { get; set; }
        public string Particulars { get; set; }
        public decimal Amount { get; set; }

        public TcComBankMemberData(TcBankMemberData member)
        {
            LineNumber = member.LineNumber;
            DestinationBank = member.BankCode;
            DestinationBranch = member.BranchCode;
            DestinationAccount = member.AccountNumber;
            DestinationAccountName = member.NameWithInitials;
            Particulars = member.NIC;
            Amount = member.Amount;
        }

        public TcComBankMemberData(TcEmployerData employer, decimal amount)
        {
            DestinationBank = employer.BankCode;
            DestinationBranch = employer.BranchCode;
            DestinationAccount = employer.AccountNumber;
            DestinationAccountName = employer.AccountName;
            Particulars = "0";
            Amount = amount;
        }
    }
}
