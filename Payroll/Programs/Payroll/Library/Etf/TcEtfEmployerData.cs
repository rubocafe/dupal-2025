using Payroll.Library.Date;
using System;

// Harshan Nishantha
// 2013-12-23

namespace Payroll.Library.Etf
{
    public class TcEtfEmployerData
    {
        public string EmployerNumber { get; set; }  // 8 text AANNNNNN
        public TcYearMonth From { get; set; }       // 6 YYYYMM, 2008 July Month 200807 
        public TcYearMonth To { get; set; }         // 6 YYYYMM, 2008 July Month 200807 

        public TcEtfEmployerData(TcYearMonth workingYearMonth)
        {
            EmployerNumber  = "";
            From    = workingYearMonth;
            To      = workingYearMonth;
        }
    }
}
