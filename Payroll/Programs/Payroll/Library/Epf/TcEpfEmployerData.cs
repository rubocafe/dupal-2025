using Payroll.Library.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-12-31

namespace Payroll.Library.Epf
{
    public class TcEpfEmployerData
    {
        public string ZoneCode { get; set; }                    // 1 Text "A"
        public string EmployerNumber { get; set; }              // 6 numeric
        public TcYearMonth ContributionPeriod { get; set; }     // 6 numeric Contribution YYYYMM
        public int SubmissionId { get; set; }                    // 2 numeric, 01 = all staff in one file / 02 = Staff in separate categories as "Executive", "non-Executive"

        public TcEpfEmployerData()
        {
            ZoneCode        = "A";
            EmployerNumber  = "";
            SubmissionId     = 1;
        }

        public static TcEpfEmployerData Fake(TcYearMonth contributionPeriod)
        {
            TcEpfEmployerData origin = new TcEpfEmployerData();

            origin.ZoneCode             = "";
            origin.EmployerNumber       = "";
            origin.SubmissionId         = 1;
            origin.ContributionPeriod   = contributionPeriod;

            return origin;
        }
    }
}
