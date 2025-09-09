using Payroll.Library;
using Payroll.Library.General;
using Payroll.UI.Business.Salary;
using Payroll.UI.Controls;
using System.Collections.Generic;
using System.Linq;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.UI.Business.Salary
{
    public class TcBusinessSalaryEngine
    {
        private List<TcBusinessSalaryRow> all = new List<TcBusinessSalaryRow>();

        private Dictionary<string, TcBusinessSalaryRow> nicAll = new Dictionary<string, TcBusinessSalaryRow>();
        private Dictionary<string, List<TcBusinessSalaryRow>> nicDuplicates = new Dictionary<string, List<TcBusinessSalaryRow>>();

        private List<TcBusinessSalaryRow> emptyNIC = new List<TcBusinessSalaryRow>();

        public List<TcBusinessSalaryRow> All
        {
            get { return all; }
        }

        public TcBusinessSalaryEngine(List<TcBusinessSalaryRow> data)
        {
            data = data
                .Where(d =>
                    !(string.IsNullOrEmpty(d.NameWithInitials) &&
                    string.IsNullOrEmpty(d.NIC) &&
                    string.IsNullOrEmpty(d.EmployeeNumber)))
                    .ToList();

            this.all = data;
            Load();
        }

        public void Load()
        {
            foreach (TcBusinessSalaryRow data in all)
            {
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
                            nicDuplicates.Add(data.NIC, new List<TcBusinessSalaryRow>() { nicAll[data.NIC], data });
                        }
                    }
                    else
                    {
                        nicAll.Add(data.NIC, data);
                    }
                }

                if (string.IsNullOrEmpty(data.NIC))
                {
                    emptyNIC.Add(data);
                }
            }
        }

        public List<TcBusinessSalaryRow> FilterAndSearch(string filterText, string searchText)
        {
            TeBusinessSalaryFilter filter = TcEnum.GetEnumForText<TeBusinessSalaryFilter>(TeBusinessSalaryFilter.All, filterText);
            var data = all;

            switch (filter)
            {
                case TeBusinessSalaryFilter.NIC_Duplicates:
                    data = GetNICDuplicates();
                    break;

                case TeBusinessSalaryFilter.Empty_NIC:
                    data = GetEmptyNICRows();
                    break;

                default:
                    break;
            }

            TcListSearchHelper<TcBusinessSalaryRow> searchHelper = new TcListSearchHelper<TcBusinessSalaryRow>();
            data = searchHelper.Search(data, searchText);
            return data;
        }

        public bool HasEmptyNICRows()
        {
            return emptyNIC.Count > 0 ? true : false;
        }

        public bool HasNICDuplicates()
        {
            return nicDuplicates.Count > 0 ? true : false;
        }

        private TcBusinessSalaryRow GetRowWithNIC(string nic)
        {
            TcBusinessSalaryRow data = null;

            if (!string.IsNullOrEmpty(nic) &&
                nicAll.ContainsKey(nic))
            {
                data = nicAll[nic];
            }

            return data;
        }

        public List<TcBusinessSalaryRow> GetNICDuplicates()
        {
            List<TcBusinessSalaryRow> nicList = new List<TcBusinessSalaryRow>();
            foreach (KeyValuePair<string, List<TcBusinessSalaryRow>> record in nicDuplicates)
            {
                TcBusinessSalaryRow row = GetRowWithNIC(record.Key);
                if (row != null)
                {
                    nicList.Add(row);
                }
            }

            return nicList;
        }

        public List<TcBusinessSalaryRow> GetNICDuplicates(string nic)
        {
            List<TcBusinessSalaryRow> duplicates = new List<TcBusinessSalaryRow>();
            if (nicDuplicates.ContainsKey(nic))
            {
                duplicates = nicDuplicates[nic];
            }

            return duplicates;
        }

        public List<TcBusinessSalaryRow> GetEmptyNICRows()
        {
            return emptyNIC;
        }
    }
}
