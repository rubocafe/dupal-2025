using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LucidPayroll.Model.Salary.Incentive;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayrollTester.Salary.Incentive
{
    [TestClass]
    public class TcIncentivesTester
    {
        private TcIncentives incentives = new TcIncentives();

        string incentiveName = "Sales Incentive";
        TcIncentive incentive;

        public TcIncentivesTester()
        {
            incentive = new TcIncentive(incentiveName, 1000);
        }

        [TestMethod]
        public void AddIncentive()
        {
            incentives.Clear();
            incentives.Add(incentive);

            TcIncentive retrievedIncentive = incentives.GetIncentive(incentiveName);

            Assert.AreEqual(1, incentives.Count());
            Assert.AreEqual(true, incentives.Exists());
            Assert.AreEqual(true, incentives.ContainsIncentive(incentiveName));
            Assert.AreEqual(incentive, retrievedIncentive);
        }

        [TestMethod]
        public void RemoveIncentive()
        {
            incentives.Clear();
            incentives.Add(incentive);
            Assert.AreEqual(true, incentives.ContainsIncentive(incentiveName));
            Assert.AreEqual(1, incentives.Count());

            incentives.Remove(incentive.Name);
            Assert.AreEqual(false, incentives.ContainsIncentive(incentiveName));
            Assert.AreEqual(0, incentives.Count());
        }

        [TestMethod]
        public void SetIncentiveValue()
        {
            incentives.Clear();
            incentives.Add(incentive);
            incentives.SetIncentiveValue(incentiveName, 7212812312.67m);

            TcIncentive retrievedIncentive = incentives.GetIncentive(incentiveName);
            Assert.AreEqual(7212812312.67m, retrievedIncentive.Value);
        }

        [TestMethod]
        public void GetIncentivesTotal()
        {
            incentives.Clear();

            TcIncentive incentive1 = new TcIncentive("Incentive 1", 26371812381.45m);
            incentives.Add(incentive1);

            TcIncentive incentive2 = new TcIncentive("Incentive 2", 8127917238.45m);
            incentives.Add(incentive2);

            decimal total = incentives.GetTotal();
            Assert.AreEqual(26371812381.45m + 8127917238.45m, total);
        }
    }
}
