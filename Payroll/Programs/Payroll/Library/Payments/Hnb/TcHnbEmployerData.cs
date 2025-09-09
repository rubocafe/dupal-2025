using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ruchira Bomiriya
// 2016/09/19

namespace Payroll.Library.Payments.Hnb
{
    public class TcHnbEmployerData
    {
        public string AccountName { get; set; }
        public string DebitAccountNumber { get; set; }
        public DateTime DateOfCrediting { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }

        public TcHnbEmployerData(TcEmployerData employer)
        {
            AccountName = employer.AccountName;
            DebitAccountNumber = employer.AccountNumber;
            DateOfCrediting = employer.ValueDate;
            BankCode = "7083";
            BranchCode = employer.BranchCode;
        }
    }
}
