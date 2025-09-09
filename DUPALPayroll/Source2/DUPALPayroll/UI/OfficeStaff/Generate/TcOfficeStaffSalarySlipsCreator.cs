using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.SalarySlip;
using DUPALPayroll.UI.OfficeStaff.Analyze;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.OfficeStaff.Generate
{
    public class TcOfficeStaffSalarySlipsCreator : TcSalarySlipsCreator<TcOfficeStaffAnalyzedRow>
    {
        public TcOfficeStaffSalarySlipsCreator(TcYearMonth workingYearMonth)
            : base(workingYearMonth, "OFFICE STAFF")
        {
        }

        public override void FillContent(TcOfficeStaffAnalyzedRow data)
        {
            AddRow("Basic Salary", data.BasicSalary);
            AddRow("Budgetary Relief Allowance", data.BRA);
            AddEmptyRow();

            AddRow("Gross Salary", data.GrossSalary);
            AddNegativeRow("EPF 8%", data.EPFDeduction);
            AddEmptyRow();

            AddTotalRow("Net Salary", data.NetSalary);
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
