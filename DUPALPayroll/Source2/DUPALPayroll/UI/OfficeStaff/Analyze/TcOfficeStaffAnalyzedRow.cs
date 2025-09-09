using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.AnalyzeBean;
using DUPALPayroll.UI.OfficeStaff.MasterData;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.OfficeStaff.Analyze
{
    public class TcOfficeStaffAnalyzedRow : TcSalaryAnalyzedRow
    {
        
        public TcBindingList<TcOfficeStaffMasterRow> DuplicateMasterRows { get; set; }

        public TcOfficeStaffAnalyzedRow()
        {
            DuplicateMasterRows = new TcBindingList<TcOfficeStaffMasterRow>();
        }
    }
}
