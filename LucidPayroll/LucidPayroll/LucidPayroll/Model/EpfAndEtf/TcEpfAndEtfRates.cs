using LucidLibrary.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayroll.Model.EpfAndEtf
{
    public class TcEpfAndEtfRates
    {
        public List<TcEpfAndEtfRate> Rates { get; set; }

        public TcEpfAndEtfRates()
        {
            Rates = new List<TcEpfAndEtfRate>();
        }

        public void Add(TcEpfAndEtfRate rate)
        {
            Rates.Add(rate);
        }

        public TcEpfAndEtfRate GetAffectedRate(TcYearMonth salaryYearMonth)
        {
            TcEpfAndEtfRate epfAndEtfRate = TcEpfAndEtfRate.ZeroRate();
            DateTime salaryStartDate = salaryYearMonth.ToDate();

            foreach (TcEpfAndEtfRate anEpfAndEtfRate in Rates)
            {
                if (anEpfAndEtfRate.FromDate == null || anEpfAndEtfRate.FromDate <= salaryStartDate)
                {
                    if (anEpfAndEtfRate.ToDate == null || anEpfAndEtfRate.ToDate > salaryStartDate)
                    {
                        epfAndEtfRate = anEpfAndEtfRate;
                        break;
                    }
                }
            }

            return epfAndEtfRate;
        }
    }
}
