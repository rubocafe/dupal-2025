using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.AnalyzeBean;
using DUPALPayroll.UI.CustomerCare.MasterData;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.CustomerCare.Analyze
{
    public class TcCustomerCareAnalyzedRow : TcSalaryAnalyzedRow
    {
        
        public TcBindingList<TcCustomerCareMasterRow> DuplicateMasterRows { get; set; }

        public decimal NoPay { get; set; }
        public decimal TBI { get; set; }
        public decimal PBI { get; set; }

        public decimal SalesCommission { get; set; }
        public decimal UpsellingAndEBillingIncentive { get; set; }

        public TcCustomerCareAnalyzedRow()
        {
            DuplicateMasterRows = new TcBindingList<TcCustomerCareMasterRow>();
        }
    }
}
