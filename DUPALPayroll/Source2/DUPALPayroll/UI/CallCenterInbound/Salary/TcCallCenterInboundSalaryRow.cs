using DUPALPayroll.UI.Common.SalaryBean;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterInbound.Salary
{
    public class TcCallCenterInboundSalaryRow : TcSalaryRow
    {
        public decimal NoPay { get; set; }
        public decimal AttendanceIncentive { get; set; }
        public decimal UpsellingIncentive { get; set; }
        public decimal EBillingIncentive { get; set; }
    }
}
