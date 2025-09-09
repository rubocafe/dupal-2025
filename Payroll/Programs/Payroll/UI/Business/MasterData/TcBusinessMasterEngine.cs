using Payroll.Library;
//using Payroll.UI.Business.Salary;
using Payroll.UI.Business.MasterData;
using System.Collections.Generic;
using Payroll.UI.Controls;
using Payroll.Library.General;
using System.Linq;
using Payroll.UI.Business.Salary;

// Harshan Nishantha
// 2013-08-27

namespace Payroll.UI.Business.MasterData
{
    public class TcBusinessMasterEngine
    {
        private List<TcBusinessMasterRow> allData = new List<TcBusinessMasterRow>();
        private Dictionary<string, TcBusinessMasterRow> nicAll = new Dictionary<string, TcBusinessMasterRow>();
        private Dictionary<string, List<TcBusinessMasterRow>> nicDuplicates = new Dictionary<string, List<TcBusinessMasterRow>>();

        public TcBusinessMasterEngine(List<TcBusinessMasterRow> data)
        {
            data = data
                .Where(d =>
                    !(string.IsNullOrEmpty(d.NameWithInitials) &&
                    string.IsNullOrEmpty(d.NIC) &&
                    string.IsNullOrEmpty(d.EmployeeNumber)))
                    .ToList();

            this.allData = data;
            Load();
        }

        public void Load()
        {
            foreach (TcBusinessMasterRow data in allData)
            {
                if (nicAll.ContainsKey(data.NIC))
                {
                    if (nicDuplicates.ContainsKey(data.NIC))
                    {
                        nicDuplicates[data.NIC].Add(data);
                    }
                    else
                    {
                        nicDuplicates.Add(data.NIC, new List<TcBusinessMasterRow>() { nicAll[data.NIC], data });
                    }
                }
                else
                {
                    nicAll.Add(data.NIC, data);
                }
            }
        }

        public List<TcBusinessMasterRow> FilterAndSearch(string filterText, string searchText, TcBusinessSalaryTable salaryTable)
        {
            TeBusinessMasterFilter filter = TcEnum.GetEnumForText<TeBusinessMasterFilter>(TeBusinessMasterFilter.All, filterText);
            var data = allData;

            switch (filter)
            {
                case TeBusinessMasterFilter.NIC_Duplicates:
                    data = GetNICDuplicates();
                    break;

                case TeBusinessMasterFilter.NIC_Empty:
                    data = GetNICEmptyList();
                    break;

                case TeBusinessMasterFilter.Duplicate_NICs_for_Employees_in_Salary_File:
                    data = GetNICDuplicatesForEmployeesInSalaryFile(salaryTable);
                    break;

                default:
                    break;
            }

            TcListSearchHelper<TcBusinessMasterRow> searchHelper = new TcListSearchHelper<TcBusinessMasterRow>();
            data = searchHelper.Search(data, searchText);
            return data;
        }

        public bool HasNICDuplicates()
        {
            return nicDuplicates.Count > 0 ? true : false;
        }

        public bool HasEmptyNIC()
        {
            return nicDuplicates.ContainsKey("");
        }

        public bool HasDuplicateNICsForEmployeesInSalaryFile(TcBusinessSalaryTable table)
        {
            var list = GetNICDuplicatesForEmployeesInSalaryFile(table);
            if (list.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public TcBusinessMasterRow GetRowWithNIC(string nic)
        {
            TcBusinessMasterRow row = null;

            if (!string.IsNullOrEmpty(nic) &&
                nicAll.ContainsKey(nic))
            {
                row = nicAll[nic];
            }

            return row;
        }

        public List<TcBusinessMasterRow> GetNICDuplicates(string nic)
        {
            List<TcBusinessMasterRow> list = new List<TcBusinessMasterRow>();

            if (!string.IsNullOrEmpty(nic) && nicDuplicates.ContainsKey(nic))
            {
                List<TcBusinessMasterRow> niclist = nicDuplicates[nic];
                foreach (TcBusinessMasterRow row in niclist)
                {
                    if (!list.Contains(row))
                    {
                        list.Add(row);
                    }
                }
            }

            return list;
        }

        public List<TcBusinessMasterRow> GetNICDuplicates()
        {
            List<TcBusinessMasterRow> nicList = new List<TcBusinessMasterRow>();
            foreach (KeyValuePair<string, List<TcBusinessMasterRow>> record in nicDuplicates)
            {
                TcBusinessMasterRow data = GetRowWithNIC(record.Key);
                if (data != null)
                {
                    nicList.Add(data);
                }
            }

            return nicList;
        }

        public List<TcBusinessMasterRow> GetNICEmptyList()
        {
            List<TcBusinessMasterRow> list = new List<TcBusinessMasterRow>();

            if (nicDuplicates.ContainsKey(""))
            {
                list = nicDuplicates[""];
            }

            return list;
        }

        //public TcBindingList<TcBusinessMasterRow> GetVNDuplicateRowsForAgentsInCommissionsFile(TcBusinessSalaryTable commissionsTable)
        //{
        //    TcBindingList<TcBusinessMasterRow> list = new TcBindingList<TcBusinessMasterRow>();

        //    foreach (TcBusinessSalaryRow commissionRow in commissionsTable.All)
        //    {
        //        TcBindingList<TcBusinessMasterRow> duplicates = GetVNDuplicates(commissionRow.VirtualNumber);
        //        if (duplicates.Count > 0)
        //        {
        //            list.Add(duplicates[0]);
        //        }
        //    }

        //    return list;
        //}

        //public TcBindingList<TcBusinessMasterRow> GetNICDuplicateRowsForAgentsInCommissionsFile(TcBusinessSalaryTable commissionsTable)
        //{
        //    TcBindingList<TcBusinessMasterRow> list = new TcBindingList<TcBusinessMasterRow>();

        //    foreach (TcBusinessSalaryRow commissionRow in commissionsTable.All)
        //    {
        //        TcBindingList<TcBusinessMasterRow> duplicates = GetNICDuplicates(commissionRow.NIC);
        //        if (duplicates.Count > 0)
        //        {
        //            list.Add(duplicates[0]);
        //        }
        //    }

        //    return list;
        //}

        public List<TcBusinessMasterRow> GetNICDuplicatesForEmployeesInSalaryFile(TcBusinessSalaryTable table)
        {
            List<TcBusinessMasterRow> list = new List<TcBusinessMasterRow>();

            foreach (TcBusinessSalaryRow row in table.Rows)
            {
                if (!string.IsNullOrEmpty(row.NIC))
                {
                    List<TcBusinessMasterRow> duplicates = GetNICDuplicates(row.NIC);
                    if (duplicates.Count > 0)
                    {
                        list.Add(duplicates[0]);
                    }
                }

            }

            return list;
        }
    }
}
