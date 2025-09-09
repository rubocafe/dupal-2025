using DUPALPayroll.Library;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.Common.MasterBean
{
    public class TcSalaryMasterTable<T> where T : TcSalaryMasterRow
    {
        private TcBindingList<T> all = new TcBindingList<T>();

        private Dictionary<string, T> empNoAll = new Dictionary<string, T>();
        private Dictionary<string, T> nicAll = new Dictionary<string, T>();

        private TcBindingList<T> empNoEmpty = new TcBindingList<T>();
        private TcBindingList<T> nicEmpty   = new TcBindingList<T>();

        private Dictionary<string, TcBindingList<T>> empNoDuplicates     = new Dictionary<string, TcBindingList<T>>();
        private Dictionary<string, TcBindingList<T>> nicDuplicates    = new Dictionary<string, TcBindingList<T>>();

        public void Load(TcBindingList<T> employeesData)
        {
            foreach (T data in employeesData)
            {

                if (string.IsNullOrEmpty(data.EmployeeNumber))
                {
                    empNoEmpty.Add(data);
                }
                else
                {
                    if (empNoAll.ContainsKey(data.EmployeeNumber))
                    {
                        if (empNoDuplicates.ContainsKey(data.EmployeeNumber))
                        {
                            empNoDuplicates[data.EmployeeNumber].Add(data);
                        }
                        else
                        {
                            empNoDuplicates.Add(data.EmployeeNumber, new TcBindingList<T>() { empNoAll[data.EmployeeNumber], data });
                        }
                    }
                    else
                    {
                        empNoAll.Add(data.EmployeeNumber, data);
                    }
                }

                if (string.IsNullOrEmpty(data.NIC))
                {
                    nicEmpty.Add(data);
                }
                else
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

        public T GetRow(string employeeNumber, string nic)
        {
            T row = GetRowWithEmployeeNumber(employeeNumber);

            if (row == null)
            {
                row = GetRowWithNIC(nic);
            }

            return row;
        }

        public T GetRow(string nic)
        {
            T row = GetRowWithNIC(nic);

            return row;
        }

        private T GetRowWithNIC(string nic)
        {
            T row = null;

            if (nicAll.ContainsKey(nic))
            {
                row = nicAll[nic];
            }

            return row;
        }

        private T GetRowWithEmployeeNumber(string employeeNumber)
        {
            T row = null;

            if (empNoAll.ContainsKey(employeeNumber))
            {
                row = empNoAll[employeeNumber];
            }

            return row;
        }

        public bool HasNICDuplicates()
        {
            return nicDuplicates.Count > 0 ? true : false;
        }

        public bool HasEmployeeNumberDuplicates()
        {
            return empNoDuplicates.Count > 0 ? true : false;
        }

        public bool HasEmptyEmployeeNumbers()
        {
            return empNoEmpty.Count > 0 ? true : false; 
        }

        public bool HasEmptyNIC()
        {
            return nicEmpty.Count > 0 ? true : false;
        }

        public TcBindingList<T> GetDuplicates(string employeeNumber, string nic)
        {
            TcBindingList<T> list = new TcBindingList<T>();

            if (empNoDuplicates.ContainsKey(employeeNumber))
            {
                TcBindingList<T> empNolist = empNoDuplicates[employeeNumber];
                list = empNolist;
            }

            if (nicDuplicates.ContainsKey(nic))
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

        public TcBindingList<T> GetSalaryRowDuplicates(string employeeNumber, string nic)
        {
            TcBindingList<T> list = new TcBindingList<T>();

            list = GetEmployeeNumberDuplicates(employeeNumber);

            return list;
        }

        public TcBindingList<T> GetEmployeeNumberDuplicates()
        {
            TcBindingList<T> enList = new TcBindingList<T>();
            foreach (KeyValuePair<string, TcBindingList<T>> record in empNoDuplicates)
            {
                T data = GetRowWithEmployeeNumber(record.Key);
                if (data != null)
                {
                    enList.Add(data);
                }
            }

            return enList;
        }

        public TcBindingList<T> GetEmployeeNumberDuplicates(string employeeNumber)
        {
            TcBindingList<T> duplicates = new TcBindingList<T>();
            if (empNoDuplicates.ContainsKey(employeeNumber))
            {
                duplicates = empNoDuplicates[employeeNumber];
            }

            return duplicates;
        }

        public TcBindingList<T> GetNICDuplicates()
        {
            TcBindingList<T> nicList = new TcBindingList<T>();
            foreach (KeyValuePair<string, TcBindingList<T>> record in nicDuplicates)
            {
                T data = GetRowWithNIC(record.Key);
                if (data != null)
                {
                    nicList.Add(data);
                }
            }

            return nicList;
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

        public TcBindingList<T> GetNICEmpltyList()
        {
            return nicEmpty;
        }

        public TcBindingList<T> GetEmployeeNumberEmpltyList()
        {
            return empNoEmpty;
        }
    }
}
