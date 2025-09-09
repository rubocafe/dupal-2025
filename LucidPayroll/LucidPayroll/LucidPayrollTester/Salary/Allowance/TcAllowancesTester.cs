using LucidPayroll.Model.Salary.Allowance;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayrollTester.Salary.Allowance
{
    [TestClass]
    public class TcAllowancesTester
    {
        private TcAllowances allowances = new TcAllowances();

        string allowanceName = "Sales Allowance";
        TcAllowance allowance;

        public TcAllowancesTester()
        {
            allowance = new TcAllowance(allowanceName, 1000);
        }

        [TestMethod]
        public void AddAllowance()
        {
            allowances.Clear();
            allowances.Add(allowance);

            TcAllowance retrievedAllowance = allowances.GetAllowance(allowanceName);

            Assert.AreEqual(1, allowances.Count());
            Assert.AreEqual(true, allowances.Exists());
            Assert.AreEqual(true, allowances.ContainsAllowance(allowanceName));
            Assert.AreEqual(allowance, retrievedAllowance);
        }

        [TestMethod]
        public void RemoveAllowance()
        {
            allowances.Clear();
            allowances.Add(allowance);
            Assert.AreEqual(true, allowances.ContainsAllowance(allowanceName));
            Assert.AreEqual(1, allowances.Count());

            allowances.Remove(allowance.Name);
            Assert.AreEqual(false, allowances.ContainsAllowance(allowanceName));
            Assert.AreEqual(0, allowances.Count());
        }

        [TestMethod]
        public void SetAllowanceValue()
        {
            allowances.Clear();
            allowances.Add(allowance);
            allowances.SetAllowanceValue(allowanceName, 45879);

            TcAllowance retrievedAllowance = allowances.GetAllowance(allowanceName);
            Assert.AreEqual(45879, retrievedAllowance.Value);
        }

        [TestMethod]
        public void GetAllowancesTotal()
        {
            allowances.Clear();

            TcAllowance allownance1 = new TcAllowance("Allowance 1", 48868);
            allowances.Add(allownance1);

            TcAllowance allownance2 = new TcAllowance("Allowance 2", 41142);
            allowances.Add(allownance2);

            decimal total = allowances.GetTotal();
            Assert.AreEqual(new decimal(90010), total);
        }
    }
}
