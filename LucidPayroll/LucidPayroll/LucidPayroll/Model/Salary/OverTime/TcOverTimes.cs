using System;
using System.Collections;
using System.Collections.Generic;

// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.OverTime
{
    public class TcOverTimes : IEnumerable
    {
        private Dictionary<string, TcOverTime> overtimes = new Dictionary<string, TcOverTime>();

        public TcOverTimes()
        {
        }

        public IEnumerator GetEnumerator()
        {
            return overtimes.GetEnumerator();
        }

        public void Add(TcOverTime overtime)
        {
            if (ContainsOverTime(overtime.Name))
            {
                overtimes[overtime.Name] = overtime;
            }
            else
            {
                overtimes.Add(overtime.Name, overtime);
            }
        }

        public void Remove(string overtimeName)
        {
            if (ContainsOverTime(overtimeName))
            {
                overtimes.Remove(overtimeName);
            }
        }

        public void SetOverTimeValue(string name, decimal value)
        {
            if (ContainsOverTime(name))
            {
                overtimes[name].Value = value;
            }
            else
            {
                string ex = string.Format("OverTime with name [{0}] does not exist", name);
                throw new Exception(ex);
            }
        }

        public TcOverTime GetOverTime(string name)
        {
            TcOverTime overtime = null;

            if (ContainsOverTime(name))
            {
                overtime = overtimes[name];
            }

            return overtime;
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (TcOverTime overtime in overtimes.Values)
            {
                total += overtime.Value;
            }

            return total;
        }

        public int Count()
        {
            return overtimes.Count;
        }

        public bool Exists()
        {
            bool exists = overtimes.Count > 0 ? true : false;

            return exists;
        }

        public void Clear()
        {
            overtimes.Clear();
        }

        public bool ContainsOverTime(string name)
        {
            bool contains = overtimes.ContainsKey(name);

            return contains;
        }
    }
}
