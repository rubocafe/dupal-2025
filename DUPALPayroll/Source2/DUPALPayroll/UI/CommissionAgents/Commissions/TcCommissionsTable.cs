using DUPALPayroll.Library;
using DUPALPayroll.UI.CommissionAgents.Commissions;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.CommissionAgents.Commissions
{
    public class TcCommissionsTable
    {
        private TcBindingList<TcCommissionsRow> all = new TcBindingList<TcCommissionsRow>();

        private Dictionary<string, TcCommissionsRow> vnAll = new Dictionary<string, TcCommissionsRow>();
        private Dictionary<string, TcCommissionsRow> nicAll = new Dictionary<string, TcCommissionsRow>();

        private Dictionary<string, TcBindingList<TcCommissionsRow>> vnDuplicates = new Dictionary<string, TcBindingList<TcCommissionsRow>>();
        private Dictionary<string, TcBindingList<TcCommissionsRow>> nicDuplicates = new Dictionary<string, TcBindingList<TcCommissionsRow>>();

        private TcBindingList<TcCommissionsRow> emptyVNandNIC = new TcBindingList<TcCommissionsRow>();

        public TcBindingList<TcCommissionsRow> All
        {
            get { return all; }
        }

        public void Load(TcBindingList<TcCommissionsRow> commissionsRows)
        {
            foreach (TcCommissionsRow data in commissionsRows)
            {
                if (vnAll.ContainsKey(data.VirtualNumber))
                {
                    if (vnDuplicates.ContainsKey(data.VirtualNumber))
                    {
                        vnDuplicates[data.VirtualNumber].Add(data);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(data.VirtualNumber) && data.VirtualNumber != "0")
                        {
                            vnDuplicates.Add(data.VirtualNumber, new TcBindingList<TcCommissionsRow>() { vnAll[data.VirtualNumber], data });
                        }
                    }
                }
                else
                {
                    vnAll.Add(data.VirtualNumber, data);
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
                            nicDuplicates.Add(data.NIC, new TcBindingList<TcCommissionsRow>() { nicAll[data.NIC], data });
                        }
                    }
                    else
                    {
                        nicAll.Add(data.NIC, data);
                    }
                }

                if ((string.IsNullOrEmpty(data.VirtualNumber) || data.VirtualNumber == "0") && string.IsNullOrEmpty(data.NIC))
                {
                    if (!string.IsNullOrEmpty(data.Name))
                    {
                        all.Add(data);
                        emptyVNandNIC.Add(data);
                    }
                }
                else
                {
                    all.Add(data);
                }
            }
        }

        public bool HasEmptyVNAndNICRows()
        {
            return emptyVNandNIC.Count > 0 ? true : false;
        }

        public bool HasNICDuplicates()
        {
            return nicDuplicates.Count > 0 ? true : false;
        }

        public bool HasVNDuplicates()
        {
            return vnDuplicates.Count > 0 ? true : false;
        }

        private TcCommissionsRow GetRowWithNIC(string nic)
        {
            TcCommissionsRow data = null;

            if (!string.IsNullOrEmpty(nic) &&
                nicAll.ContainsKey(nic))
            {
                data = nicAll[nic];
            }

            return data;
        }

        public TcCommissionsRow GetRowWithVN(string virtualNumber)
        {
            TcCommissionsRow data = null;

            if (!string.IsNullOrEmpty(virtualNumber) &&
                virtualNumber != "0" &&
                vnAll.ContainsKey(virtualNumber))
            {
                data = vnAll[virtualNumber];
            }

            return data;
        }

        public TcBindingList<TcCommissionsRow> GetDuplicates(string virtualNumber, string nic)
        {
            TcBindingList<TcCommissionsRow> list = new TcBindingList<TcCommissionsRow>();

            if (!string.IsNullOrEmpty(virtualNumber) && vnDuplicates.ContainsKey(virtualNumber))
            {
                TcBindingList<TcCommissionsRow> vnlist = vnDuplicates[virtualNumber];
                list = vnlist;
            }

            if (!string.IsNullOrEmpty(nic) && nicDuplicates.ContainsKey(nic))
            {
                TcBindingList<TcCommissionsRow> niclist = nicDuplicates[nic];
                foreach (TcCommissionsRow row in niclist)
                {
                    if (!list.Contains(row))
                    {
                        list.Add(row);
                    }
                }
            }

            return list;
        }

        public TcBindingList<TcCommissionsRow> GetNICDuplicates()
        {
            TcBindingList<TcCommissionsRow> nicList = new TcBindingList<TcCommissionsRow>();
            foreach (KeyValuePair<string, TcBindingList<TcCommissionsRow>> record in nicDuplicates)
            {
                TcCommissionsRow row = GetRowWithNIC(record.Key);
                if (row != null)
                {
                    nicList.Add(row);
                }
            }

            return nicList;
        }

        public TcBindingList<TcCommissionsRow> GetNICDuplicates(string nic)
        {
            TcBindingList<TcCommissionsRow> duplicates = new TcBindingList<TcCommissionsRow>();
            if (nicDuplicates.ContainsKey(nic))
            {
                duplicates = nicDuplicates[nic];
            }

            return duplicates;
        }

        public TcBindingList<TcCommissionsRow> GetVNDuplicates()
        {
            TcBindingList<TcCommissionsRow> vnList = new TcBindingList<TcCommissionsRow>();
            foreach (KeyValuePair<string, TcBindingList<TcCommissionsRow>> record in vnDuplicates)
            {
                TcCommissionsRow data = GetRowWithVN(record.Key);
                if (data != null)
                {
                    vnList.Add(data);
                }
            }

            return vnList;
        }

        public TcBindingList<TcCommissionsRow> GetVNDuplicates(string virtualNumber)
        {
            TcBindingList<TcCommissionsRow> duplicates = new TcBindingList<TcCommissionsRow>();
            if (vnDuplicates.ContainsKey(virtualNumber))
            {
                duplicates = vnDuplicates[virtualNumber];
            }

            return duplicates;
        }

        public TcBindingList<TcCommissionsRow> GetEmptyVirtualNumberAndNICRows()
        {
            return emptyVNandNIC;
        }
    }
}
