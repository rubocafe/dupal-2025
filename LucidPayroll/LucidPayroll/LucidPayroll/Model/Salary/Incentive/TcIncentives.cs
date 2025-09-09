using System;
using System.Collections;
using System.Collections.Generic;

// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.Incentive
{
    public class TcIncentives : IEnumerable
    {
        private Dictionary<string, TcIncentive> incentives = new Dictionary<string, TcIncentive>();

        public TcIncentives()
        {
        }

        public IEnumerator GetEnumerator()
        {
            return incentives.GetEnumerator();
        }

        public void Add(TcIncentive incentive)
        {
            if (ContainsIncentive(incentive.Name))
            {
                incentives[incentive.Name] = incentive;
            }
            else
            {
                incentives.Add(incentive.Name, incentive);
            }
        }

        public void Remove(string incentiveName)
        {
            if (ContainsIncentive(incentiveName))
            {
                incentives.Remove(incentiveName);
            }
        }

        public void SetIncentiveValue(string name, decimal value)
        {
            if (ContainsIncentive(name))
            {
                incentives[name].Value = value;
            }
            else
            {
                string ex = string.Format("Incentive with name [{0}] does not exist", name);
                throw new Exception(ex);
            }
        }

        public TcIncentive GetIncentive(string name)
        {
            TcIncentive incentive = null;

            if (ContainsIncentive(name))
            {
                incentive = incentives[name];
            }

            return incentive;
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (TcIncentive incentive in incentives.Values)
            {
                total += incentive.Value;
            }

            return total;
        }

        public int Count()
        {
            return incentives.Count;
        }

        public bool Exists()
        {
            bool exists = incentives.Count > 0 ? true : false;

            return exists;
        }

        public void Clear()
        {
            incentives.Clear();
        }

        public bool ContainsIncentive(string name)
        {
            bool contains = incentives.ContainsKey(name);

            return contains;
        }
    }
}
