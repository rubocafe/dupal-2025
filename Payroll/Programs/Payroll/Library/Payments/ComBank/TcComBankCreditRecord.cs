using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ruchira Bomiriya
// 2016/09/23

namespace Payroll.Library.Payments.ComBank
{
    public class TcComBankCreditRecord : TcComBankPaymentRecord
    {
        public TcComBankCreditRecord(TcEmployerData employer, TcBankMemberData member)
            : base(employer, member, true)
        {
        }
    }
}
