using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.MasterBean;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterOutbound.MasterData
{
    public class TcCallCenterOutboundMasterLoader : TcSalaryMasterLoader<TcCallCenterOutboundMasterRow>
    {
        public TcCallCenterOutboundMasterLoader(TcYearMonth workingYearMonth)
            : base(workingYearMonth)
        {
            Init();
        }

        public override TcCallCenterOutboundMasterRow New()
        {
            return new TcCallCenterOutboundMasterRow();
        }
    }
}
