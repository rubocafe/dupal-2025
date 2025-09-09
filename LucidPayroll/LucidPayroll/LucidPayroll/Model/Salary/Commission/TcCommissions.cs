using System;
using System.Collections;
using System.Collections.Generic;

// Harshan Nishantha
// 2014-01-23

namespace LucidPayroll.Model.Salary.Commission
{
    public class TcCommissions : IEnumerable
    {
        private Dictionary<string, TcCommission> commissions = new Dictionary<string, TcCommission>();

        public TcCommissions()
        {
        }

        public IEnumerator GetEnumerator()
        {
            return commissions.GetEnumerator();
        }

        public void Add(TcCommission commission)
        {
            if (ContainsCommission(commission.Name))
            {
                commissions[commission.Name] = commission;
            }
            else
            {
                commissions.Add(commission.Name, commission);
            }
        }

        public void Remove(string commissionName)
        {
            if (ContainsCommission(commissionName))
            {
                commissions.Remove(commissionName);
            }
        }

        public void SetCommissionValue(string name, decimal value)
        {
            if (ContainsCommission(name))
            {
                commissions[name].Value = value;
            }
            else
            {
                string ex = string.Format("Commission with name [{0}] does not exist", name);
                throw new Exception(ex);
            }
        }

        public TcCommission GetCommission(string name)
        {
            TcCommission commission = null;

            if (ContainsCommission(name))
            {
                commission = commissions[name];
            }

            return commission;
        }

        public decimal GetTotal()
        {
            decimal total = 0;

            foreach (TcCommission commission in commissions.Values)
            {
                total += commission.Value;
            }

            return total;
        }

        public int Count()
        {
            return commissions.Count;
        }

        public bool Exists()
        {
            bool exists = commissions.Count > 0 ? true : false;

            return exists;
        }

        public void Clear()
        {
            commissions.Clear();
        }

        public bool ContainsCommission(string name)
        {
            bool contains = commissions.ContainsKey(name);

            return contains;
        }
    }
}
