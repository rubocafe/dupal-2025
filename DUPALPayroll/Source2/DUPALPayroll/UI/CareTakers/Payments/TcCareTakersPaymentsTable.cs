using DUPALPayroll.Library;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Payments
{
    public class TcCareTakersPaymentsTable
    {
        private TcBindingList<TcCareTakersPaymentsRow> all = new TcBindingList<TcCareTakersPaymentsRow>();

        private Dictionary<string, TcCareTakersPaymentsRow> nicAll = new Dictionary<string, TcCareTakersPaymentsRow>();

        private Dictionary<string, TcBindingList<TcCareTakersPaymentsRow>> nicDuplicates = new Dictionary<string, TcBindingList<TcCareTakersPaymentsRow>>();

        private TcBindingList<TcCareTakersPaymentsRow> emptyNIC = new TcBindingList<TcCareTakersPaymentsRow>();

        public TcBindingList<TcCareTakersPaymentsRow> All
        {
            get { return all; }
        }

        public void Load(TcBindingList<TcCareTakersPaymentsRow> commissionsRows)
        {
            foreach (TcCareTakersPaymentsRow data in commissionsRows)
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
                            nicDuplicates.Add(data.NIC, new TcBindingList<TcCareTakersPaymentsRow>() { nicAll[data.NIC], data });
                        }
                    }
                    else
                    {
                        nicAll.Add(data.NIC, data);
                    }
                }

                if (string.IsNullOrEmpty(data.NIC))
                {
                    if (!string.IsNullOrEmpty(data.Name))
                    {
                        all.Add(data);
                        emptyNIC.Add(data);
                    }
                }
                else
                {
                    all.Add(data);
                }
            }
        }

        public bool HasEmptyNICRows()
        {
            return emptyNIC.Count > 0 ? true : false;
        }

        public bool HasNICDuplicates()
        {
            return nicDuplicates.Count > 0 ? true : false;
        }

        private TcCareTakersPaymentsRow GetRowWithNIC(string nic)
        {
            TcCareTakersPaymentsRow data = null;

            if (!string.IsNullOrEmpty(nic) &&
                nicAll.ContainsKey(nic))
            {
                data = nicAll[nic];
            }

            return data;
        }

        public TcBindingList<TcCareTakersPaymentsRow> GetDuplicates(string nic)
        {
            TcBindingList<TcCareTakersPaymentsRow> list = new TcBindingList<TcCareTakersPaymentsRow>();

            if (!string.IsNullOrEmpty(nic) && nicDuplicates.ContainsKey(nic))
            {
                TcBindingList<TcCareTakersPaymentsRow> niclist = nicDuplicates[nic];
                foreach (TcCareTakersPaymentsRow row in niclist)
                {
                    if (!list.Contains(row))
                    {
                        list.Add(row);
                    }
                }
            }

            return list;
        }

        public TcBindingList<TcCareTakersPaymentsRow> GetNICDuplicates()
        {
            TcBindingList<TcCareTakersPaymentsRow> nicList = new TcBindingList<TcCareTakersPaymentsRow>();
            foreach (KeyValuePair<string, TcBindingList<TcCareTakersPaymentsRow>> record in nicDuplicates)
            {
                TcCareTakersPaymentsRow row = GetRowWithNIC(record.Key);
                if (row != null)
                {
                    nicList.Add(row);
                }
            }

            return nicList;
        }

        public TcBindingList<TcCareTakersPaymentsRow> GetNICDuplicates(string nic)
        {
            TcBindingList<TcCareTakersPaymentsRow> duplicates = new TcBindingList<TcCareTakersPaymentsRow>();
            if (nicDuplicates.ContainsKey(nic))
            {
                duplicates = nicDuplicates[nic];
            }

            return duplicates;
        }

        public TcBindingList<TcCareTakersPaymentsRow> GetEmptyNICRows()
        {
            return emptyNIC;
        }
    }
}
