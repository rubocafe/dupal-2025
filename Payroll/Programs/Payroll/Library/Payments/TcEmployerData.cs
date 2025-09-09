using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ruchira Bomiriya
// 2016/09/21

namespace Payroll.Library.Payments
{
    public class TcEmployerData
    {
        public string Bank { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public DateTime ValueDate { get; set; }
        public string Remarks { get; set; }
    }
}
