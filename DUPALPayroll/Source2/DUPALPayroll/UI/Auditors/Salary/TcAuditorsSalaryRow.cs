using DUPALPayroll.UI.Common.SalaryBean;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.Salary
{
    public class TcAuditorsSalaryRow : TcSalaryRow
    {
        public decimal TravelAllowance { get; set; }
        public decimal TravelReimbursement { get; set; }
        public decimal TravelIncentive { get; set; }
    }
}
