using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Auditors.Analyze;
using DUPALPayroll.UI.Common.SalarySlip;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.Generate
{
    public class TcAuditorsSalarySlipsCreator : TcSalarySlipsCreator<TcAuditorsAnalyzedRow>
    {
        public TcAuditorsSalarySlipsCreator(TcYearMonth workingYearMonth)
            : base(workingYearMonth, "AUDITORS")
        {
        }

        public override void FillContent(TcAuditorsAnalyzedRow data)
        {
            AddRow("Basic Salary", data.BasicSalary);
            AddRow("Budgetary Relief Allowance", data.BRA);
            AddRow("Travel Allowance", data.TravelAllowance);
            AddEmptyRow();

            AddRow("Gross Salary", data.GrossSalary);
            AddNegativeRow("EPF 8%", data.EPFDeduction);
            AddEmptyRow();

            AddTotalRow("Net Salary", data.NetSalary);
            AddEmptyRow();

            AddRow("Travel Reimbursement", data.TravelReimbursement);
            AddRow("Travel Incentive", data.TravelIncentive);
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
