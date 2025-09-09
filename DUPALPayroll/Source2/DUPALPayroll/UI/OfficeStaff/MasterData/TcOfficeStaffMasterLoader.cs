using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.MasterBean;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.OfficeStaff.MasterData
{
    public class TcOfficeStaffMasterLoader : TcSalaryMasterLoader<TcOfficeStaffMasterRow>
    {
        public TcOfficeStaffMasterLoader(TcYearMonth salaryMonth)
            : base(salaryMonth)
        {
            Init();
        }

        public override TcOfficeStaffMasterRow New()
        {
            return new TcOfficeStaffMasterRow();
        }
    }
}
