using LucidLibrary.Date;
using LucidPayroll.Model.EpfAndEtf;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// Harshan Nishantha
// 2014-01-22

namespace LucidPayrollTester.EpfAndEtf
{
    [TestClass]
    public class EpfAndEtfTester
    {
        private decimal employerEpfRate = new decimal(0.12);
        private decimal employeeEpfRate = new decimal(0.08);
        private decimal employerEtfRate = new decimal(0.03);

        public static TcEpfAndEtfRate GetDummyEpfAndEtfRate()
        {
            TcEpfAndEtfRate rate = new TcEpfAndEtfRate();

            rate.FromDate   = null;
            rate.ToDate     = null;
            rate.EmployerEpfContributionRate = 0.12m;
            rate.EmployeeEpfContributionRate = 0.08m;
            rate.EmployerEtfContributionRate = 0.03m;

            return rate;
        }

        public static TcEpfAndEtfRates GetDummyEpfAndEtfRates()
        {
            TcEpfAndEtfRates rates = new TcEpfAndEtfRates();
            TcEpfAndEtfRate rate = GetDummyEpfAndEtfRate();
            rates.Add(rate);

            return rates;
        }

        public EpfAndEtfTester()
        {
        }

        [TestMethod]
        public void TestRates()
        {
            TcYearMonth salaryMonth = TcYearMonth.OfNow();
            TcEpfAndEtfRates rates = GetDummyEpfAndEtfRates();
            TcEpfAndEtfRate rate = rates.GetAffectedRate(salaryMonth);
            Assert.AreEqual(employerEpfRate, rate.EmployerEpfContributionRate);
            Assert.AreEqual(employeeEpfRate, rate.EmployeeEpfContributionRate);
            Assert.AreEqual(employerEtfRate, rate.EmployerEtfContributionRate);
        }

        [TestMethod]
        public void TestRatesOnDateRange()
        {
            TcYearMonth salaryMonth = new TcYearMonth(2014, 01);
            TcEpfAndEtfRates rates = new TcEpfAndEtfRates();

            TcEpfAndEtfRate rate = GetDummyEpfAndEtfRate();
            rate.FromDate = new DateTime(1970, 01, 01);
            rate.ToDate = new DateTime(2000, 12, 31);
            rates.Add(rate);

            rate = GetDummyEpfAndEtfRate();
            rate.FromDate = new DateTime(2000, 12, 31);
            rate.ToDate = null;
            rate.EmployerEpfContributionRate = 1;
            rate.EmployeeEpfContributionRate = 2;
            rate.EmployerEtfContributionRate = 3;
            rates.Add(rate);

            TcEpfAndEtfRate currentRate = rates.GetAffectedRate(salaryMonth);
            Assert.AreEqual(1, currentRate.EmployerEpfContributionRate);
            Assert.AreEqual(2, currentRate.EmployeeEpfContributionRate);
            Assert.AreEqual(3, currentRate.EmployerEtfContributionRate);
        }
    }
}
