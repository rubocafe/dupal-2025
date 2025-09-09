using DUPALPayroll.UI.Common.SalaryBean;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterOutbound.Salary
{
    public class TcCallCenterOutboundSalaryRow : TcSalaryRow
    {
        public decimal OTNormal { get; set; }
        public decimal OTDouble { get; set; }
        public decimal NoPay { get; set; }

        public decimal AttendanceIncentive { get; set; }
        public decimal PBI { get; set; }
        public decimal UpsellingAndEBillingIncentive { get; set; }
    }
}
