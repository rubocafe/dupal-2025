using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LucidPayroll.Model.Salary.Deduction;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayrollTester.Salary.Deduction
{
    [TestClass]
    public class TcDeductionsTester
    {
        private TcDeductions deductions = new TcDeductions();

        string deductionName = "Sales Deduction";
        TcDeduction deduction;

        public TcDeductionsTester()
        {
            deduction = new TcDeduction(deductionName, 1000);
        }

        [TestMethod]
        public void AddDeduction()
        {
            deductions.Clear();
            deductions.Add(deduction);

            TcDeduction retrievedDeduction = deductions.GetDeduction(deductionName);

            Assert.AreEqual(1, deductions.Count());
            Assert.AreEqual(true, deductions.Exists());
            Assert.AreEqual(true, deductions.ContainsDeduction(deductionName));
            Assert.AreEqual(deduction, retrievedDeduction);
        }

        [TestMethod]
        public void RemoveDeduction()
        {
            deductions.Clear();
            deductions.Add(deduction);
            Assert.AreEqual(true, deductions.ContainsDeduction(deductionName));
            Assert.AreEqual(1, deductions.Count());

            deductions.Remove(deduction.Name);
            Assert.AreEqual(false, deductions.ContainsDeduction(deductionName));
            Assert.AreEqual(0, deductions.Count());
        }

        [TestMethod]
        public void SetDeductionValue()
        {
            deductions.Clear();
            deductions.Add(deduction);
            deductions.SetDeductionValue(deductionName, 3472374.67m);

            TcDeduction retrievedDeduction = deductions.GetDeduction(deductionName);
            Assert.AreEqual(3472374.67m, retrievedDeduction.Value);
        }

        [TestMethod]
        public void GetDeductionsTotal()
        {
            deductions.Clear();

            TcDeduction deduction1 = new TcDeduction("Deduction 1", 1231231.56m);
            deductions.Add(deduction1);

            TcDeduction deduction2 = new TcDeduction("Deduction 2", 34345345.44m);
            deductions.Add(deduction2);

            decimal total = deductions.GetTotal();
            Assert.AreEqual(new decimal(1231231 + 34345346), total);
        }
    }
}
