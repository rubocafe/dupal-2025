using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.MasterBean;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.PremierSales.MasterData
{
    public class TcPremierSalesMasterLoader : TcSalaryMasterLoader<TcPremierSalesMasterRow>
    {
        public TcPremierSalesMasterLoader(TcYearMonth workingYearMonth)
            : base(workingYearMonth)
        {
            Init();
        }

        public override TcPremierSalesMasterRow New()
        {
            return new TcPremierSalesMasterRow();
        }
    }
}
