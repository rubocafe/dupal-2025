using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.MasterBean;
using System;

// Harshan Nishantha
// 2013-10-01

namespace DUPALPayroll.UI.SupervisorsAndBackOffice.MasterData
{
    public class TcSupervisorsAndBackOfficeMasterLoader : TcSalaryMasterLoader<TcSupervisorsAndBackOfficeMasterRow>
    {
        public TcSupervisorsAndBackOfficeMasterLoader(TcYearMonth workingYearMonth)
            : base(workingYearMonth)
        {
            Init();
        }

        public override TcSupervisorsAndBackOfficeMasterRow New()
        {
            return new TcSupervisorsAndBackOfficeMasterRow();
        }
    }
}
