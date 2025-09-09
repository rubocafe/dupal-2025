using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

// Harshan Nishatha
// 2014-01-22

namespace LucidPayrollTester
{
    [TestClass]
    public class GenericDefaultValueTester
    {
        public static T Read<T>()
        {
            T value = default(T);
            return value;
        }

        [TestMethod]
        public void TestString()
        {
            string value = Read<string>();
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void TestDateTime()
        {
            DateTime value = Read<DateTime>();
            Assert.AreEqual(DateTime.MinValue, value);
        }

        [TestMethod]
        public void TestNullableDateTime()
        {
            Nullable<DateTime> value = Read<Nullable<DateTime>>();
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void TestBool()
        {
            bool value = Read<bool>();
            Assert.AreEqual(false, value);
        }

        [TestMethod]
        public void TestNullableBool()
        {
            Nullable<bool> value = Read<Nullable<bool>>();
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void TestDecimal()
        {
            decimal value = Read<decimal>();
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void TestNullableDecimal()
        {
            Nullable<decimal> value = Read<Nullable<decimal>>();
            Assert.AreEqual(null, value);
        }
    }
}
