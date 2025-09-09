using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.MasterBean;
using System;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.CustomerCare.MasterData
{
    public class TcCustomerCareMasterLoader : TcSalaryMasterLoader<TcCustomerCareMasterRow>
    {
        public TcCustomerCareMasterLoader(TcYearMonth workingYearMonth)
            : base(workingYearMonth)
        {
            Init();
        }

        public override TcCustomerCareMasterRow New()
        {
            return new TcCustomerCareMasterRow();
        }
    }
}
