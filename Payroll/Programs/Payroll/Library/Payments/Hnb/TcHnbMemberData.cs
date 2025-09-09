using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ruchira Bomiriya
// 2016/09/19

namespace Payroll.Library.Payments.Hnb 
{
    public class TcHnbMemberData
    {
        public int ReferenceNumber { get; set; }
        public string AccountName { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string CrediAccountNumber { get; set; }
        public DateTime ValueDate { get; set; }
        public decimal Amount { get; set; }
        public string Comments { get; set; }

        public TcHnbMemberData(TcBankMemberData member)
        {
            ReferenceNumber = member.LineNumber;
            AccountName = member.NameWithInitials;
            BankCode = member.BankCode;
            BranchCode = member.BranchCode;
            CrediAccountNumber = member.AccountNumber;
            ValueDate = member.ValueDate;
            Amount = member.Amount;
            Comments = member.NIC;
        }
    }
}
