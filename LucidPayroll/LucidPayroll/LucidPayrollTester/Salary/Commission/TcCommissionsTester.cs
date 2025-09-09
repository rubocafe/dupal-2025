using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LucidPayroll.Model.Salary.Commission;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayrollTester.Salary.Commission
{
    [TestClass]
    public class TcCommissionsTester
    {
        private TcCommissions commissions = new TcCommissions();

        string commissionName = "Sales Commission";
        TcCommission commission;

        public TcCommissionsTester()
        {
            commission = new TcCommission(commissionName, 1000);
        }

        [TestMethod]
        public void AddCommission()
        {
            commissions.Clear();
            commissions.Add(commission);

            TcCommission retrievedCommission = commissions.GetCommission(commissionName);

            Assert.AreEqual(1, commissions.Count());
            Assert.AreEqual(true, commissions.Exists());
            Assert.AreEqual(true, commissions.ContainsCommission(commissionName));
            Assert.AreEqual(commission, retrievedCommission);
        }

        [TestMethod]
        public void RemoveCommission()
        {
            commissions.Clear();
            commissions.Add(commission);
            Assert.AreEqual(true, commissions.ContainsCommission(commissionName));
            Assert.AreEqual(1, commissions.Count());

            commissions.Remove(commission.Name);
            Assert.AreEqual(false, commissions.ContainsCommission(commissionName));
            Assert.AreEqual(0, commissions.Count());
        }

        [TestMethod]
        public void SetCommissionValue()
        {
            commissions.Clear();
            commissions.Add(commission);
            commissions.SetCommissionValue(commissionName, 1112);

            TcCommission retrievedCommission = commissions.GetCommission(commissionName);
            Assert.AreEqual(1112, retrievedCommission.Value);
        }

        [TestMethod]
        public void GetCommissionsTotal()
        {
            commissions.Clear();

            TcCommission com1 = new TcCommission("COM1", 48868);
            commissions.Add(com1);

            TcCommission com2 = new TcCommission("COM2", 41132);
            commissions.Add(com2);

            decimal total = commissions.GetTotal();
            Assert.AreEqual(new decimal(90000), total);
        }
    }
}
