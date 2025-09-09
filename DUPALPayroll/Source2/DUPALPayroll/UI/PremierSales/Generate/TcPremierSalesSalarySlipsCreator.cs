using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.SalarySlip;
using DUPALPayroll.UI.PremierSales.Analyze;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.PremierSales.Generate
{
    public class TcPremierSalesSalarySlipsCreator : TcSalarySlipsCreator<TcPremierSalesAnalyzedRow>
    {
        public TcPremierSalesSalarySlipsCreator(TcYearMonth workingYearMonth)
            : base(workingYearMonth, "PREMIER SALES")
        {
        }

        public override void FillContent(TcPremierSalesAnalyzedRow data)
        {
            AddRow("Basic Remuneration", data.BasicSalary);
            AddRow("Budgetary Relief Allowance", data.BRA);
            AddRow("Sales Commissions", data.SalesCommissions);
            AddEmptyRow();

            AddRow("Gross Remuneration", data.GrossSalary);
            AddNegativeRow("EPF 8%", data.EPFDeduction);
            AddEmptyRow();

            AddTotalRow("Net Remuneration", data.NetSalary);
            AddEmptyRow();

            AddTotalRow("Total Remuneration", data.TotalRemuneration);
            AddEmptyRow();

            AddBoldHeadingRow("Deductions");
            AddNegativeRow("Commission Advance Paid", data.CommissionAdvance);
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
