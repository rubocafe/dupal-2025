using LucidLibrary.Date;
using LucidPayroll.Model.EpfAndEtf;
using LucidPayroll.Model.Salary;
using LucidPayroll.Model.Salary.Allowance;
using LucidPayroll.Model.Salary.Commission;
using LucidPayroll.Model.Salary.Deduction;
using LucidPayroll.Model.Salary.Incentive;
using LucidPayroll.Model.Salary.OverTime;
using LucidPayrollTester.EpfAndEtf;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayrollTester.Salary
{
    [TestClass]
    public class TcMonthlySalaryTester
    {
        private static TcMonthlySalary GetDummyMonthlySalary()
        {
            TcEpfAndEtfRate epfAndEtfRate = EpfAndEtfTester.GetDummyEpfAndEtfRate();
            TcYearMonth salaryYearMonth = TcYearMonth.OfNow();
            TcMonthlySalary salary = new TcMonthlySalary(salaryYearMonth, epfAndEtfRate);

            salary.BasicSalary = 10000m;
            salary.BRA = 1000m;

            salary.Commissions.Add(new TcDummyCommmission(3000));
            salary.OverTimes.Add(new TcDummyOverTime(500m, 10));
            salary.Allowances.Add(new TcDummyAllowance(2000m));
            salary.Incentives.Add(new TcDummyIncentive(2500m));
            salary.Deductions.Add(new TcDummyDeduction(3400m));

            salary.Calculate();

            return salary;
        }

        [TestMethod]
        public void TestCalculations()
        {
            TcMonthlySalary salary = GetDummyMonthlySalary();

            Assert.AreEqual(14000m, salary.GrossSalary);
            Assert.AreEqual(1680m, salary.EmployerEpfContribution);
            Assert.AreEqual(1120m, salary.EmployeeEpfContribution);
            Assert.AreEqual(420m, salary.EmployerEtfContribution);
            Assert.AreEqual(18980m, salary.NetSalary);
        }
    }
}
