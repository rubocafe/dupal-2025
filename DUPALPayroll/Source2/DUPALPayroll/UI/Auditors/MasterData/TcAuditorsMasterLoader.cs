using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.MasterBean;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.MasterData
{
    public class TcAuditorsMasterLoader : TcSalaryMasterLoader<TcAuditorsMasterRow>
    {
        public TcAuditorsMasterLoader(TcYearMonth salaryMonth)
            : base(salaryMonth)
        {
            Init();
        }

        public override TcAuditorsMasterRow New()
        {
            return new TcAuditorsMasterRow();
        }
    }
}
