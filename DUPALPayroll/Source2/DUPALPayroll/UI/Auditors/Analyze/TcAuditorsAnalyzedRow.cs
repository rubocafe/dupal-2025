using DUPALPayroll.Library;
using DUPALPayroll.UI.Auditors.MasterData;
using DUPALPayroll.UI.Common.AnalyzeBean;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.Analyze
{
    public class TcAuditorsAnalyzedRow : TcSalaryAnalyzedRow
    {
        public TcBindingList<TcAuditorsMasterRow> DuplicateMasterRows { get; set; }

        public decimal TravelAllowance { get; set; }
        public decimal TravelReimbursement { get; set; }
        public decimal TravelIncentive { get; set; }

        public TcAuditorsAnalyzedRow()
        {
            DuplicateMasterRows = new TcBindingList<TcAuditorsMasterRow>();
        }
    }
}
