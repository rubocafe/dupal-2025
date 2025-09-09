using System;
using System.Collections;
using System.Collections.Generic;

// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.Deduction
{
    public class TcDeductions : IEnumerable
    {
        private Dictionary<string, TcDeduction> deductions = new Dictionary<string, TcDeduction>();

        public TcDeductions()
        {
        }

        public IEnumerator GetEnumerator()
        {
            return deductions.GetEnumerator();
        }

        public void Add(TcDeduction deduction)
        {
            if (ContainsDeduction(deduction.Name))
            {
                deductions[deduction.Name] = deduction;
            }
            else
            {
                deductions.Add(deduction.Name, deduction);
            }
        }

        public void Remove(string deductionName)
        {
            if (ContainsDeduction(deductionName))
            {
                deductions.Remove(deductionName);
            }
        }

        public void SetDeductionValue(string name, decimal value)
        {
            if (ContainsDeduction(name))
            {
                deductions[name].Value = value;
            }
            else
            {
                string ex = string.Format("Deduction with name [{0}] does not exist", name);
                throw new Exception(ex);
            }
        }

        public TcDeduction GetDeduction(string name)
        {
            TcDeduction deduction = null;

            if (ContainsDeduction(name))
            {
                deduction = deductions[name];
            }

            return deduction;
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (TcDeduction deduction in deductions.Values)
            {
                total += deduction.Value;
            }

            return total;
        }

        public int Count()
        {
            return deductions.Count;
        }

        public bool Exists()
        {
            bool exists = deductions.Count > 0 ? true : false;

            return exists;
        }

        public void Clear()
        {
            deductions.Clear();
        }

        public bool ContainsDeduction(string name)
        {
            bool contains = deductions.ContainsKey(name);

            return contains;
        }
    }
}
