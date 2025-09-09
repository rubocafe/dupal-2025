using DUPALPayroll.Library;
using DUPALPayroll.UI.CommissionAgents.Commissions;
using DUPALPayroll.UI.CommissionAgents.MasterData;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.CommissionAgents.MasterData
{
    public class TcCommissionAgentsMasterTable
    {
        private TcBindingList<TcCommissionAgentsMasterRow> all = new TcBindingList<TcCommissionAgentsMasterRow>();

        private Dictionary<string, TcCommissionAgentsMasterRow> vnAll = new Dictionary<string, TcCommissionAgentsMasterRow>();
        private Dictionary<string, TcCommissionAgentsMasterRow> nicAll = new Dictionary<string, TcCommissionAgentsMasterRow>();

        private Dictionary<string, TcBindingList<TcCommissionAgentsMasterRow>> vnDuplicates     = new Dictionary<string, TcBindingList<TcCommissionAgentsMasterRow>>();
        private Dictionary<string, TcBindingList<TcCommissionAgentsMasterRow>> nicDuplicates    = new Dictionary<string, TcBindingList<TcCommissionAgentsMasterRow>>();

        public void Load(TcBindingList<TcCommissionAgentsMasterRow> employeesData)
        {
            foreach (TcCommissionAgentsMasterRow data in employeesData)
            {
                if (vnAll.ContainsKey(data.VirtualNumber))
                {
                    if (vnDuplicates.ContainsKey(data.VirtualNumber))
                    {
                        vnDuplicates[data.VirtualNumber].Add(data);
                    }
                    else
                    {
                        vnDuplicates.Add(data.VirtualNumber, new TcBindingList<TcCommissionAgentsMasterRow>() { vnAll[data.VirtualNumber], data });
                    }
                }
                else
                {
                    vnAll.Add(data.VirtualNumber, data);
                }

                if (nicAll.ContainsKey(data.NIC))
                {
                    if (nicDuplicates.ContainsKey(data.NIC))
                    {
                        nicDuplicates[data.NIC].Add(data);
                    }
                    else
                    {
                        nicDuplicates.Add(data.NIC, new TcBindingList<TcCommissionAgentsMasterRow>() { nicAll[data.NIC], data });
                    }
                }
                else
                {
                    nicAll.Add(data.NIC, data);
                }

                all.Add(data);
            }
        }

        public TcCommissionAgentsMasterRow GetRow(string virtualNumber, string nic)
        {
            TcCommissionAgentsMasterRow row = GetRowWithVN(virtualNumber);

            if (row == null)
            {
                row = GetRowWithNIC(nic);
            }

            return row;
        }

        private TcCommissionAgentsMasterRow GetRowWithNIC(string nic)
        {
            TcCommissionAgentsMasterRow row = null;

            if (!string.IsNullOrEmpty(nic) &&
                nicAll.ContainsKey(nic))
            {
                row = nicAll[nic];
            }

            return row;
        }

        private TcCommissionAgentsMasterRow GetRowWithVN(string virtualNumber)
        {
            TcCommissionAgentsMasterRow data = null;

            if (!string.IsNullOrEmpty(virtualNumber) &&
                virtualNumber != "0" &&
                vnAll.ContainsKey(virtualNumber))
            {
                data = vnAll[virtualNumber];
            }

            return data;
        }

        public bool HasNICDuplicates()
        {
            return nicDuplicates.Count > 0 ? true : false;
        }

        public bool HasVNDuplicates()
        {
            return vnDuplicates.Count > 0 ? true : false;
        }

        public bool HasEmptyVN()
        {
            return vnDuplicates.ContainsKey("");
        }

        public bool HasEmptyNIC()
        {
            return nicDuplicates.ContainsKey("");
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetDuplicates(string virtualNumber, string nic)
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = new TcBindingList<TcCommissionAgentsMasterRow>();

            if (!string.IsNullOrEmpty(virtualNumber) && vnDuplicates.ContainsKey(virtualNumber))
            {
                TcBindingList<TcCommissionAgentsMasterRow> vnlist = vnDuplicates[virtualNumber];
                list = vnlist;
            }

            if (!string.IsNullOrEmpty(nic) && nicDuplicates.ContainsKey(nic))
            {
                TcBindingList<TcCommissionAgentsMasterRow> niclist = nicDuplicates[nic];
                foreach (TcCommissionAgentsMasterRow row in niclist)
                {
                    if (!list.Contains(row))
                    {
                        list.Add(row);
                    }
                }
            }

            return list;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetCommissionRowDuplicates(string virtualNumber, string nic)
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = new TcBindingList<TcCommissionAgentsMasterRow>();

            if ((!string.IsNullOrEmpty(virtualNumber) && virtualNumber != "0"))
            {
                list = GetVNDuplicates(virtualNumber);
            }

            return list;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetVNDuplicates()
        {
            TcBindingList<TcCommissionAgentsMasterRow> vnList = new TcBindingList<TcCommissionAgentsMasterRow>();
            foreach (KeyValuePair<string, TcBindingList<TcCommissionAgentsMasterRow>> record in vnDuplicates)
            {
                TcCommissionAgentsMasterRow data = GetRowWithVN(record.Key);
                if (data != null)
                {
                    vnList.Add(data);
                }
            }

            return vnList;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetVNDuplicates(string virtualNumber)
        {
            TcBindingList<TcCommissionAgentsMasterRow> duplicates = new TcBindingList<TcCommissionAgentsMasterRow>();
            if (vnDuplicates.ContainsKey(virtualNumber))
            {
                duplicates = vnDuplicates[virtualNumber];
            }

            return duplicates;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetNICDuplicates()
        {
            TcBindingList<TcCommissionAgentsMasterRow> nicList = new TcBindingList<TcCommissionAgentsMasterRow>();
            foreach (KeyValuePair<string, TcBindingList<TcCommissionAgentsMasterRow>> record in nicDuplicates)
            {
                TcCommissionAgentsMasterRow data = GetRowWithNIC(record.Key);
                if (data != null)
                {
                    nicList.Add(data);
                }
            }

            return nicList;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetNICDuplicates(string nic)
        {
            TcBindingList<TcCommissionAgentsMasterRow> duplicates = new TcBindingList<TcCommissionAgentsMasterRow>();
            if (nicDuplicates.ContainsKey(nic))
            {
                duplicates = nicDuplicates[nic];
            }

            return duplicates;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetNICEmpltyList()
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = new TcBindingList<TcCommissionAgentsMasterRow>();

            if (nicDuplicates.ContainsKey(""))
            {
                list = nicDuplicates[""];
            }

            return list;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetVNEmpltyList()
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = new TcBindingList<TcCommissionAgentsMasterRow>();

            if (vnDuplicates.ContainsKey(""))
            {
                list = vnDuplicates[""];
            }

            return list;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetVNDuplicateRowsForAgentsInCommissionsFile(TcCommissionsTable commissionsTable)
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = new TcBindingList<TcCommissionAgentsMasterRow>();

            foreach (TcCommissionsRow commissionRow in commissionsTable.All)
            {
                TcBindingList<TcCommissionAgentsMasterRow> duplicates = GetVNDuplicates(commissionRow.VirtualNumber);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetNICDuplicateRowsForAgentsInCommissionsFile(TcCommissionsTable commissionsTable)
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = new TcBindingList<TcCommissionAgentsMasterRow>();

            foreach (TcCommissionsRow commissionRow in commissionsTable.All)
            {
                TcBindingList<TcCommissionAgentsMasterRow> duplicates = GetNICDuplicates(commissionRow.NIC);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> GetNICDuplicateRowsForAgentsWithoutVNInCommissionsFile(TcCommissionsTable commissionsTable)
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = new TcBindingList<TcCommissionAgentsMasterRow>();

            foreach (TcCommissionsRow commissionRow in commissionsTable.All)
            {
                if (string.IsNullOrEmpty(commissionRow.VirtualNumber) || commissionRow.VirtualNumber == "0")
                {
                    if (!string.IsNullOrEmpty(commissionRow.NIC))
                    {
                        TcBindingList<TcCommissionAgentsMasterRow> duplicates = GetNICDuplicates(commissionRow.NIC);
                        if (duplicates.Count > 0)
                        {
                            list.Add(duplicates[0]);
                        }
                    }
                }
                
            }

            return list;
        }
    }
}
