using DUPALPayroll.Library;
using DUPALPayroll.UI.CareTakers.Payments;
using DUPALPayroll.UI.CareTakers.MasterData;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.MasterData
{
    public class TcCareTakersMasterTable
    {
        private TcBindingList<TcCareTakersMasterRow> all = new TcBindingList<TcCareTakersMasterRow>();

        private Dictionary<string, TcCareTakersMasterRow> nicAll = new Dictionary<string, TcCareTakersMasterRow>();
        private TcBindingList<TcCareTakersMasterRow> nicEmpty = new TcBindingList<TcCareTakersMasterRow>();

        private Dictionary<string, TcBindingList<TcCareTakersMasterRow>> nicDuplicates    = new Dictionary<string, TcBindingList<TcCareTakersMasterRow>>();

        public void Load(TcBindingList<TcCareTakersMasterRow> employeesData)
        {
            foreach (TcCareTakersMasterRow data in employeesData)
            {
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
                            nicDuplicates.Add(data.NIC, new TcBindingList<TcCareTakersMasterRow>() { nicAll[data.NIC], data });
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

        public TcCareTakersMasterRow GetRow(string nic)
        {
            TcCareTakersMasterRow row = GetRowWithNIC(nic);

            return row;
        }

        private TcCareTakersMasterRow GetRowWithNIC(string nic)
        {
            TcCareTakersMasterRow row = null;

            if (!string.IsNullOrEmpty(nic) &&
                nicAll.ContainsKey(nic))
            {
                row = nicAll[nic];
            }

            return row;
        }

        public bool HasNICDuplicates()
        {
            return nicDuplicates.Count > 0 ? true : false;
        }

        public bool HasEmptyNIC()
        {
            return nicEmpty.Count > 0 ? true : false;
        }

        public TcBindingList<TcCareTakersMasterRow> GetDuplicates(string nic)
        {
            TcBindingList<TcCareTakersMasterRow> list = new TcBindingList<TcCareTakersMasterRow>();

            if (!string.IsNullOrEmpty(nic) && nicDuplicates.ContainsKey(nic))
            {
                TcBindingList<TcCareTakersMasterRow> niclist = nicDuplicates[nic];
                foreach (TcCareTakersMasterRow row in niclist)
                {
                    if (!list.Contains(row))
                    {
                        list.Add(row);
                    }
                }
            }

            return list;
        }

        public TcBindingList<TcCareTakersMasterRow> GetCommissionRowDuplicates(string nic)
        {
            TcBindingList<TcCareTakersMasterRow> list = new TcBindingList<TcCareTakersMasterRow>();

            if ((!string.IsNullOrEmpty(nic)))
            {
                list = GetNICDuplicates(nic);
            }

            return list;
        }

        public TcBindingList<TcCareTakersMasterRow> GetNICDuplicates()
        {
            TcBindingList<TcCareTakersMasterRow> nicList = new TcBindingList<TcCareTakersMasterRow>();
            foreach (KeyValuePair<string, TcBindingList<TcCareTakersMasterRow>> record in nicDuplicates)
            {
                TcCareTakersMasterRow data = GetRowWithNIC(record.Key);
                if (data != null)
                {
                    nicList.Add(data);
                }
            }

            return nicList;
        }

        public TcBindingList<TcCareTakersMasterRow> GetNICDuplicates(string nic)
        {
            TcBindingList<TcCareTakersMasterRow> duplicates = new TcBindingList<TcCareTakersMasterRow>();
            if (nicDuplicates.ContainsKey(nic))
            {
                duplicates = nicDuplicates[nic];
            }

            return duplicates;
        }

        public TcBindingList<TcCareTakersMasterRow> GetNICEmpltyList()
        {
            return nicEmpty;
        }

        public TcBindingList<TcCareTakersMasterRow> GetNICDuplicateRowsForAgentsInCommissionsFile(TcCareTakersPaymentsTable commissionsTable)
        {
            TcBindingList<TcCareTakersMasterRow> list = new TcBindingList<TcCareTakersMasterRow>();

            foreach (TcCareTakersPaymentsRow commissionRow in commissionsTable.All)
            {
                TcBindingList<TcCareTakersMasterRow> duplicates = GetNICDuplicates(commissionRow.NIC);
                if (duplicates.Count > 0)
                {
                    if (!list.Contains(duplicates[0]))
                    {
                        list.Add(duplicates[0]);
                    }
                }
            }

            return list;
        }
    }
}
