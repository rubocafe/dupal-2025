using DUPALPayroll.Library;
using DUPALPayroll.UI.CallCenterInbound.MasterData;
using DUPALPayroll.UI.Common.AnalyzeBean;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterInbound.Analyze
{
    public class TcCallCenterInboundAnalyzedRow : TcSalaryAnalyzedRow
    {
        
        public TcBindingList<TcCallCenterInboundMasterRow> DuplicateMasterRows { get; set; }

        public decimal NoPay { get; set; }
        public decimal AttendanceIncentive { get; set; }
        public decimal UpsellingIncentive { get; set; }
        public decimal EBillingIncentive { get; set; }

        public TcCallCenterInboundAnalyzedRow()
        {
            DuplicateMasterRows = new TcBindingList<TcCallCenterInboundMasterRow>();
        }
    }
}
