using System;
using System.Collections;
using System.Collections.Generic;

// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.Allowance
{
    public class TcAllowances : IEnumerable
    {
        private Dictionary<string, TcAllowance> allowances = new Dictionary<string, TcAllowance>();

        public TcAllowances()
        {
        }

        public IEnumerator GetEnumerator()
        {
            return allowances.GetEnumerator();
        }

        public void Add(TcAllowance allowance)
        {
            if (ContainsAllowance(allowance.Name))
            {
                allowances[allowance.Name] = allowance;
            }
            else
            {
                allowances.Add(allowance.Name, allowance);
            }
        }

        public void Remove(string allowanceName)
        {
            if (ContainsAllowance(allowanceName))
            {
                allowances.Remove(allowanceName);
            }
        }

        public TcAllowance GetAllowance(string name)
        {
            TcAllowance allowance = null;

            if (ContainsAllowance(name))
            {
                allowance = allowances[name];
            }

            return allowance;
        }

        public void SetAllowanceValue(string name, decimal value)
        {
            if (ContainsAllowance(name))
            {
                allowances[name].Value = value;
            }
            else
            {
                string ex = string.Format("Allowance with name [{0}] does not exist", name);
                throw new Exception(ex);
            }
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (TcAllowance allowance in allowances.Values)
            {
                total += allowance.Value;
            }

            return total;
        }

        public bool Exists()
        {
            bool exists = allowances.Count > 0 ? true : false;

            return exists;
        }

        public void Clear()
        {
            allowances.Clear();
        }

        public int Count()
        {
            return allowances.Count;
        }

        public bool ContainsAllowance(string name)
        {
            bool contains = allowances.ContainsKey(name);

            return contains;
        }
    }
}
