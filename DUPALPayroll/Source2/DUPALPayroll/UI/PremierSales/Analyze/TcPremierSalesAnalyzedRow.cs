using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.AnalyzeBean;
using DUPALPayroll.UI.PremierSales.MasterData;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.PremierSales.Analyze
{
    public class TcPremierSalesAnalyzedRow : TcSalaryAnalyzedRow
    {
        
        public TcBindingList<TcPremierSalesMasterRow> DuplicateMasterRows { get; set; }

        public decimal SalesCommissions { get; set; }
        public decimal CommissionAdvance { get; set; }

        public TcPremierSalesAnalyzedRow()
        {
            DuplicateMasterRows = new TcBindingList<TcPremierSalesMasterRow>();
        }
    }
}
