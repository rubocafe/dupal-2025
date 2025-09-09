using DUPALPayroll.General;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.SalarySlip;
using DUPALPayroll.UI.CustomerCare.Analyze;
using System;

// Harshan Nishantha
// 2013-09-23

namespace DUPALPayroll.UI.CustomerCare.Generate
{
    public class TcCustomerCareSalarySlipsCreator : TcSalarySlipsCreator<TcCustomerCareAnalyzedRow>
    {
        public TcCustomerCareSalarySlipsCreator(TcYearMonth workingYearMonth)
            : base(workingYearMonth, "CUSTOMER CARE")
        {
        }

        public override void FillContent(TcCustomerCareAnalyzedRow data)
        {
            AddRow("Basic Salary", data.BasicSalary);
            AddRow("Budgetary Relief Allowance", data.BRA);
            AddEmptyRow();

            AddNegativeRow("No Pay", data.NoPay);
            AddEmptyRow();

            AddRow("Gross Salary", data.GrossSalary);
            AddNegativeRow("EPF 8%", data.EPFDeduction);
            AddEmptyRow();

            AddTotalRow("Net Salary", data.NetSalary);
            AddEmptyRow();

            AddRow("Transaction-Based Incentive", data.TBI);
            AddRow("Performance-Based Incentive", data.PBI);
            if (TcVersions.IsCustomerCareFR001Supported(WorkingYearMonth))
            {
                AddRow("Sales Commission", data.SalesCommission);
                AddRow("Upselling & E-Billing incentive", data.UpsellingAndEBillingIncentive);
            }
            AddEmptyRow();

            AddTotalRow("Total Remuneration", data.TotalRemuneration);
            AddEmptyRow();

            AddBoldHeadingRow("Deductions");
            AddNegativePayeRow("PAYE", data.Paye);
            AddNegativeRow("Held Amount", data.Hold);
            AddEmptyRow();

            AddTotalRow(finalSalaryString, ZeroIfNegative(data.BankTransferAmount));
            AddEmptyRow();

            AddRow("EPF 12%", data.EPFContribution);
            AddRow("ETF 3%", data.ETFContribution);
        }
    }
}
