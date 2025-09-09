using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.MasterBean;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterInbound.MasterData
{
    public class TcCallCenterInboundMasterLoader : TcSalaryMasterLoader<TcCallCenterInboundMasterRow>
    {
        public TcCallCenterInboundMasterLoader(TcYearMonth salaryMonth)
            : base(salaryMonth)
        {
            Init();
        }

        public override TcCallCenterInboundMasterRow New()
        {
            return new TcCallCenterInboundMasterRow();
        }
    }
}
