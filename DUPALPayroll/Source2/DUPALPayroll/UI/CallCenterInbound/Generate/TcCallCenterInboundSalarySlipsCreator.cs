using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.CallCenterInbound.Analyze;
using DUPALPayroll.UI.Common.SalarySlip;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterInbound.Generate
{
    public class TcCallCenterInboundSalarySlipsCreator : TcSalarySlipsCreator<TcCallCenterInboundAnalyzedRow>
    {
        public TcCallCenterInboundSalarySlipsCreator(TcYearMonth workingYearMonth)
            : base(workingYearMonth, "CALL CENTER INBOUND")
        {
        }

        public override void FillContent(TcCallCenterInboundAnalyzedRow data)
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

            AddRow("Attendance Incentive", data.AttendanceIncentive);
            AddRow("Upselling Incentive", data.UpsellingIncentive);
            AddRow("E-Billling Incentive", data.EBillingIncentive);
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
