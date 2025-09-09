using DUPALPayroll.Library.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-12-31

namespace DUPALPayroll.UI.Common.Epf
{
    public class TcEpfOriginData
    {
        public string ZoneCode { get; set; }                    // 1 Text "A"
        public string EmployerNumber { get; set; }              // 6 numeric
        public TcYearMonth ContributionPeriod { get; set; }     // 6 numeric Contribution YYYYMM
        public int SubmitionId { get; set; }                    // 2 numeric, 01 = all staff in one file / 02 = Staff in separate categories as "Executive", "non-Executive"

        public TcEpfOriginData()
        {
            ZoneCode        = "A";
            EmployerNumber  = "";
            SubmitionId     = 1;
        }

        public static TcEpfOriginData Fake(TcYearMonth contributionPeriod)
        {
            TcEpfOriginData origin = new TcEpfOriginData();

            origin.ZoneCode             = "";
            origin.EmployerNumber       = "";
            origin.SubmitionId          = 1;
            origin.ContributionPeriod   = contributionPeriod;

            return origin;
        }
    }
}
