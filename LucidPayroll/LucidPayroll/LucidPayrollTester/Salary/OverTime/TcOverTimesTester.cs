using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LucidPayroll.Model.Salary.OverTime;

// Harshan Nishantha
// 2014-01-24

namespace LucidPayrollTester.Salary.OverTime
{
    [TestClass]
    public class TcOverTimesTester
    {
        private TcOverTimes overtimes = new TcOverTimes();

        string overtimeName = "Sales OverTime";
        TcOverTime overtime;

        public TcOverTimesTester()
        {
            overtime = new TcOverTime(overtimeName, 1000);
        }

        [TestMethod]
        public void AddOverTime()
        {
            overtimes.Clear();
            overtimes.Add(overtime);

            TcOverTime retrievedOverTime = overtimes.GetOverTime(overtimeName);

            Assert.AreEqual(1, overtimes.Count());
            Assert.AreEqual(true, overtimes.Exists());
            Assert.AreEqual(true, overtimes.ContainsOverTime(overtimeName));
            Assert.AreEqual(overtime, retrievedOverTime);
        }

        [TestMethod]
        public void RemoveOverTime()
        {
            overtimes.Clear();
            overtimes.Add(overtime);
            Assert.AreEqual(true, overtimes.ContainsOverTime(overtimeName));
            Assert.AreEqual(1, overtimes.Count());

            overtimes.Remove(overtime.Name);
            Assert.AreEqual(false, overtimes.ContainsOverTime(overtimeName));
            Assert.AreEqual(0, overtimes.Count());
        }

        [TestMethod]
        public void SetOverTimeValue()
        {
            overtimes.Clear();
            overtimes.Add(overtime);
            overtimes.SetOverTimeValue(overtimeName, 152315.45m);

            TcOverTime retrievedOverTime = overtimes.GetOverTime(overtimeName);
            Assert.AreEqual(152315.45m, retrievedOverTime.Value);
        }

        [TestMethod]
        public void GetOverTimesTotal()
        {
            overtimes.Clear();

            TcOverTime ot1 = new TcOverTime("OT Normal", 50.0m);
            ot1.HoursWorked = 10;
            ot1.Calculate();
            overtimes.Add(ot1);

            TcOverTime ot2 = new TcOverTime("OT Double", 110.0m);
            ot2.HoursWorked = 5;
            ot2.Calculate();
            overtimes.Add(ot2);

            decimal total = overtimes.GetTotal();
            Assert.AreEqual((50.0m * 10) + (110.0m * 5), total);
        }
    }
}
