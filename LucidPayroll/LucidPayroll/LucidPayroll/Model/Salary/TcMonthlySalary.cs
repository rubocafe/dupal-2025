using LucidLibrary.Date;
using LucidPayroll.Model.EpfAndEtf;
using LucidPayroll.Model.Salary.Allowance;
using LucidPayroll.Model.Salary.Commission;
using LucidPayroll.Model.Salary.Deduction;
using LucidPayroll.Model.Salary.Incentive;
using LucidPayroll.Model.Salary.OverTime;

// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary
{
    public class TcMonthlySalary
    {
        public decimal BasicSalary { get; set; }
        public decimal BRA { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal EmployeeEpfContribution { get; set; }
        public decimal NetSalary { get; set; }
        public decimal EmployerEpfContribution { get; set; }
        public decimal EmployerEtfContribution { get; set; }

        public TcCommissions Commissions { get; set; }
        public TcOverTimes OverTimes { get; set; }
        public TcAllowances Allowances { get; set; }
        public TcIncentives Incentives { get; set; }
        public TcDeductions Deductions { get; set; }

        public TcYearMonth SalaryMonth { get; set; }
        public TcEpfAndEtfRate EpfAndEtfRate { get; set; }

        public TcMonthlySalary(TcYearMonth SalaryMonth, TcEpfAndEtfRate EpfAndEtfRate)
        {
            this.SalaryMonth    = SalaryMonth;
            this.EpfAndEtfRate  = EpfAndEtfRate;

            Commissions = new TcCommissions();
            OverTimes   = new TcOverTimes();
            Allowances  = new TcAllowances();
            Incentives  = new TcIncentives();
            Deductions  = new TcDeductions();
        }

        public virtual void CalculateGrossSalary()
        {
            GrossSalary = BasicSalary + BRA + Commissions.GetTotal();
        }

        public virtual void CalculateEmployerEpfContribution()
        {
            EmployerEpfContribution = GrossSalary * EpfAndEtfRate.EmployerEpfContributionRate;
        }

        public virtual void CalculateEmployeeEpfContribution()
        {
            EmployeeEpfContribution = GrossSalary * EpfAndEtfRate.EmployeeEpfContributionRate;
        }

        public virtual void CalculateEmployerEtfContribution()
        {
            EmployerEtfContribution = GrossSalary * EpfAndEtfRate.EmployerEtfContributionRate;
        }

        public virtual void CalculateNetSalary()
        {
            NetSalary = GrossSalary - EmployeeEpfContribution + OverTimes.GetTotal() + Allowances.GetTotal() + Incentives.GetTotal() - Deductions.GetTotal();
        }

        public virtual void Calculate()
        {
            CalculateGrossSalary();
            CalculateEmployerEpfContribution();
            CalculateEmployeeEpfContribution();
            CalculateEmployerEtfContribution();
            CalculateNetSalary();
        }
    }
}
