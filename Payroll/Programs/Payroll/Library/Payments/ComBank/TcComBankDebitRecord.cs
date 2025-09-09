using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ruchira Bomiriya
// 2016/09/24

namespace Payroll.Library.Payments.ComBank
{
    public class TcComBankDebitRecord : TcComBankPaymentRecord
    {
        public TcComBankDebitRecord(TcEmployerData employer, decimal amount)
        {
            TcComBankEmployerData comBankEmployer = new TcComBankEmployerData(employer);
            TcComBankMemberData employerAsComBankMember = new TcComBankMemberData(employer, amount);

            FillAndFormat(comBankEmployer, employerAsComBankMember, false);
        }
    }
}
