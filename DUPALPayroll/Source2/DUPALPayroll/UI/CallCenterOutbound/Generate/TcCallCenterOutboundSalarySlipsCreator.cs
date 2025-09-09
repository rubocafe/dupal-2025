using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.CallCenterOutbound.Analyze;
using DUPALPayroll.UI.Common.SalarySlip;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterOutbound.Generate
{
    public class TcCallCenterOutboundSalarySlipsCreator : TcSalarySlipsCreator<TcCallCenterOutboundAnalyzedRow>
    {
        public TcCallCenterOutboundSalarySlipsCreator(TcYearMonth workingYearMonth)
            : base(workingYearMonth, "CALL CENTER OUTBOUND")
        {
        }

        public override void FillContent(TcCallCenterOutboundAnalyzedRow data)
        {
            AddRow("Basic Salary", data.BasicSalary);
            AddRow("Budgetary Relief Allowance", data.BRA);
            AddRow("OT Normal", data.OTNormal);
            AddRow("OT Double", data.OTDouble);
            AddEmptyRow();

            AddNegativeRow("No Pay", data.NoPay);
            AddEmptyRow();

            AddRow("Gross Salary", data.GrossSalary);
            AddNegativeRow("EPF 8%", data.EPFDeduction);
            AddEmptyRow();

            AddTotalRow("Net Salary", data.NetSalary);
            AddEmptyRow();

            AddRow("Attendance Incentive", data.AttendanceIncentive);
            AddRow("Performance-Based Incentive", data.PBI);
            AddRow("Upselling and E-Billing Incentive", data.UpsellingAndEBillingIncentive);
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
