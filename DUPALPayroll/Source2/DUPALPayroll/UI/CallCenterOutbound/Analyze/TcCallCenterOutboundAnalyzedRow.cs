using DUPALPayroll.Library;
using DUPALPayroll.UI.CallCenterOutbound.MasterData;
using DUPALPayroll.UI.Common.AnalyzeBean;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterOutbound.Analyze
{
    public class TcCallCenterOutboundAnalyzedRow : TcSalaryAnalyzedRow
    {
        
        public TcBindingList<TcCallCenterOutboundMasterRow> DuplicateMasterRows { get; set; }

        public decimal OTNormal { get; set; }
        public decimal OTDouble { get; set; }
        public decimal NoPay { get; set; }

        public decimal AttendanceIncentive { get; set; }
        public decimal PBI { get; set; }
        public decimal UpsellingAndEBillingIncentive { get; set; }

        public TcCallCenterOutboundAnalyzedRow()
        {
            DuplicateMasterRows = new TcBindingList<TcCallCenterOutboundMasterRow>();
        }
    }
}
