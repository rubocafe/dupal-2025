using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayroll.Model.EpfAndEtf
{
    public class TcEpfAndEtfRate
    {
        public Nullable<DateTime> FromDate { get; set; }
        public Nullable<DateTime> ToDate { get; set; }
        public decimal EmployerEpfContributionRate { get; set; }
        public decimal EmployeeEpfContributionRate { get; set; }
        public decimal EmployerEtfContributionRate { get; set; }

        public static TcEpfAndEtfRate ZeroRate()
        {
            TcEpfAndEtfRate epfAndEtfRate = new TcEpfAndEtfRate();

            epfAndEtfRate.FromDate  = null;
            epfAndEtfRate.ToDate    = null;
            epfAndEtfRate.EmployerEpfContributionRate = 0.00m;
            epfAndEtfRate.EmployeeEpfContributionRate = 0.00m;
            epfAndEtfRate.EmployerEtfContributionRate = 0.00m;

            return epfAndEtfRate;
        }
    }
}
