using Payroll.General;
using Payroll.Library.Date;
using Payroll.Library.MetaData;
using Payroll.UI.Business.Analyze;
using System;

// Harshan Nishantha
// 2015-11-18

namespace Payroll.UI.Business.Generate
{
    public class TcBusinessSalarySlipsCreator : TcSalarySlipsCreator<TcBusinessAnalyzedRow>
    {
        public TcBusinessSalarySlipsCreator(TcYearMonth workingYearMonth, TcEmployerMetaData data, string business)
            : base(workingYearMonth, data, business)
        {
        }

        public override void FillContent(TcBusinessAnalyzedRow data)
        {
            AddRow("Basic Salary", data.BasicSalary);

            if (data.Allowances.Count > 0)
            {
                foreach (var allowance in data.Allowances)
                {
                    AddRow(allowance.Key, allowance.Value);
                }
                AddEmptyRow();
            }

            if (data.NoPays.Count > 0)
            {
                foreach (var noPay in data.NoPays)
                {
                    AddNegativeRow(noPay.Key, noPay.Value);
                }
                AddEmptyRow();
            }

            AddRow("Gross Salary", data.GrossSalary);
            AddNegativeRow("EPF Deduction", data.EpfDeduction);
            AddEmptyRow();

            AddTotalRow("Net Salary", data.NetSalary);
            AddEmptyRow();

            if (data.Incentives.Count > 0)
            {
                foreach (var incentive in data.Incentives)
                {
                    AddRow(incentive.Key, incentive.Value);
                }
                AddEmptyRow();
            }

            AddTotalRow("Total Remuneration", data.TotalRemuneration);
            AddEmptyRow();

            if (data.Deductions.Count > 0)
            {
                AddBoldHeadingRow("Deductions");
                foreach (var deduction in data.Deductions)
                {
                    AddNegativeRow(deduction.Key, deduction.Value);
                }
                AddEmptyRow();
            }

            AddTotalRow(finalSalaryString, ZeroIfNegative(data.BankTransferAmount));
            AddEmptyRow();

            AddRow("EPF Contribution", data.EpfContribution);
            AddRow("ETF Contribution", data.EtfContribution);
            AddRow("EPF Total", (data.EpfTotal));
        }
    }
}
