using DUPALPayroll.Library;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.Common.SalaryBean
{
    public class TcSalaryTable<T> where T : TcSalaryRow
    {
        private TcBindingList<T> all = new TcBindingList<T>();

        private Dictionary<string, T> nicAll = new Dictionary<string, T>();
        private Dictionary<string, T> employeeNumberAll = new Dictionary<string, T>();

        private Dictionary<string, TcBindingList<T>> nicDuplicates = new Dictionary<string, TcBindingList<T>>();
        private Dictionary<string, TcBindingList<T>> employeeNumberDuplicates = new Dictionary<string, TcBindingList<T>>();

        public TcBindingList<T> All
        {
            get { return all; }
        }

        public void Load(TcBindingList<T> commissionsRows)
        {
            foreach (T data in commissionsRows)
            {
                if (!string.IsNullOrEmpty(data.EmployeeNumber))
                {
                    if (employeeNumberAll.ContainsKey(data.EmployeeNumber))
                    {
                        if (employeeNumberDuplicates.ContainsKey(data.EmployeeNumber))
                        {
                            employeeNumberDuplicates[data.EmployeeNumber].Add(data);
                        }
                        else
                        {
                            employeeNumberDuplicates.Add(data.EmployeeNumber, new TcBindingList<T>() { employeeNumberAll[data.EmployeeNumber], data });
                        }
                    }
                    else
                    {
                        employeeNumberAll.Add(data.EmployeeNumber, data);
                    }
                }

                if (!string.IsNullOrEmpty(data.NIC))
                {
                    if (nicAll.ContainsKey(data.NIC))
                    {
                        if (nicDuplicates.ContainsKey(data.NIC))
                        {
                            nicDuplicates[data.NIC].Add(data);
                        }
                        else
                        {
                            nicDuplicates.Add(data.NIC, new TcBindingList<T>() { nicAll[data.NIC], data });
                        }
                    }
                    else
                    {
                        nicAll.Add(data.NIC, data);
                    }
                }

                all.Add(data);
            }
        }

        public bool HasEmployeeNumberDuplicates()
        {
            return employeeNumberDuplicates.Count > 0 ? true : false;
        }

        public bool HasNICDuplicates()
        {
            return nicDuplicates.Count > 0 ? true : false;
        }

        private T GetRowWithNIC(string nic)
        {
            T data = null;

            if (!string.IsNullOrEmpty(nic) &&
                nicAll.ContainsKey(nic))
            {
                data = nicAll[nic];
            }

            return data;
        }

        private T GetRowWithEmployeeNumber(string employeeNumber)
        {
            T data = null;

            if (!string.IsNullOrEmpty(employeeNumber) &&
                employeeNumberAll.ContainsKey(employeeNumber))
            {
                data = employeeNumberAll[employeeNumber];
            }

            return data;
        }

        public TcBindingList<T> GetNICDuplicates()
        {
            TcBindingList<T> nicList = new TcBindingList<T>();
            foreach (KeyValuePair<string, TcBindingList<T>> record in nicDuplicates)
            {
                T row = GetRowWithNIC(record.Key);
                if (row != null)
                {
                    nicList.Add(row);
                }
            }

            return nicList;
        }

        public TcBindingList<T> GetDuplicates(string employeeNumber, string nic)
        {
            TcBindingList<T> list = new TcBindingList<T>();

            if (!string.IsNullOrEmpty(employeeNumber) && employeeNumberDuplicates.ContainsKey(employeeNumber))
            {
                TcBindingList<T> vnlist = employeeNumberDuplicates[employeeNumber];
                list = vnlist;
            }

            if (!string.IsNullOrEmpty(nic) && nicDuplicates.ContainsKey(nic))
            {
                TcBindingList<T> niclist = nicDuplicates[nic];
                foreach (T row in niclist)
                {
                    if (!list.Contains(row))
                    {
                        list.Add(row);
                    }
                }
            }

            return list;
        }

        public TcBindingList<T> GetEmployeeNumberDuplicates()
        {
            TcBindingList<T> employeeNumberList = new TcBindingList<T>();
            foreach (KeyValuePair<string, TcBindingList<T>> record in employeeNumberDuplicates)
            {
                T row = GetRowWithEmployeeNumber(record.Key);
                if (row != null)
                {
                    employeeNumberList.Add(row);
                }
            }

            return employeeNumberList;
        }

        public TcBindingList<T> GetNICDuplicates(string nic)
        {
            TcBindingList<T> duplicates = new TcBindingList<T>();
            if (nicDuplicates.ContainsKey(nic))
            {
                duplicates = nicDuplicates[nic];
            }

            return duplicates;
        }

        public TcBindingList<T> GetEmployeeNumberDuplicates(string employeeNumber)
        {
            TcBindingList<T> duplicates = new TcBindingList<T>();
            if (employeeNumberDuplicates.ContainsKey(employeeNumber))
            {
                duplicates = employeeNumberDuplicates[employeeNumber];
            }

            return duplicates;
        }
    }
}
