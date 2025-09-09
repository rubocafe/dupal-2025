using DUPALPayroll.Controls;
using DUPALPayroll.UI.Common.SalaryBean;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.CustomerCare.Salary
{
    public class TcCustomerCareSalaryRow : TcSalaryRow
    {
        public decimal NoPay { get; set; }
        public decimal TBI { get; set; }
        public decimal PBI { get; set; }

        public decimal SalesCommission { get; set; }
        public decimal UpsellingAndEBillingIncentive { get; set; }
    }
}
