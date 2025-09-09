using DUPALPayroll.UI.SupervisorsAndBackOffice.Analyze;
using DUPALPayroll.UI.Common.SalarySlip;
using System;
using DUPALPayroll.Library.Date;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.SupervisorsAndBackOffice.Generate
{
    public class TcSupervisorsAndBackOfficeSalarySlipsCreator : TcSalarySlipsCreator<TcSupervisorsAndBackOfficeAnalyzedRow>
    {
        public TcSupervisorsAndBackOfficeSalarySlipsCreator(TcYearMonth workingYearMonth)
            : base(workingYearMonth, "SUPERVISORS AND BACK OFFICE")
        {
        }

        public override void FillContent(TcSupervisorsAndBackOfficeAnalyzedRow data)
        {
            AddRow("Basic Salary", data.BasicSalary);
            AddRow("Budgetary Relief Allowance", data.BRA);
            AddRow("OT Normal", data.OTNormal);
            AddRow("OT Double", data.OTDouble);
            AddEmptyRow();

            AddRow("Gross Salary", data.GrossSalary);
            AddNegativeRow("EPF 8%", data.EPFDeduction);
            AddEmptyRow();

            AddTotalRow("Net Salary", data.NetSalary);
            AddEmptyRow();

            AddRow("Performance-Based Incentive", data.PBI);
            AddRow("Work Travel Allownace", data.WorkTravelAllowance);
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
