using DUPALPayroll.Library;
using DUPALPayroll.UI.SupervisorsAndBackOffice.MasterData;
using DUPALPayroll.UI.Common.AnalyzeBean;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.SupervisorsAndBackOffice.Analyze
{
    public class TcSupervisorsAndBackOfficeAnalyzedRow : TcSalaryAnalyzedRow
    {
        
        public TcBindingList<TcSupervisorsAndBackOfficeMasterRow> DuplicateMasterRows { get; set; }

        public decimal OTNormal { get; set; }
        public decimal OTDouble { get; set; }
        public decimal PBI { get; set; }
        public decimal WorkTravelAllowance { get; set; }

        public TcSupervisorsAndBackOfficeAnalyzedRow()
        {
            DuplicateMasterRows = new TcBindingList<TcSupervisorsAndBackOfficeMasterRow>();
        }
    }
}
