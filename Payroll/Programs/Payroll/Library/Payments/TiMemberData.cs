using Payroll.Library.Epf;
using Payroll.Library.Etf;
using Payroll.Library.Payments.ComBank;

// Harshan Nishantha
// 2013-09-17

namespace Payroll.Library.Payments
{
    public interface TiMemberData
    {
        TcBankMemberData BankMemberData();
        TcEpfMemberData EpfMemberData();
        TcEtfMemberData EtfMemberData();
    }
}
