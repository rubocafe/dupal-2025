using DUPALPayroll.Library;
using DUPALPayroll.UI.CommissionAgents.Commissions;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.CommissionAgents.CommissionsHeld
{
    public class TcCommissionsHeldTable
    {
        private TcBindingList<TcCommissionsHeldRow> all = new TcBindingList<TcCommissionsHeldRow>();
        private TcBindingList<TcCommissionsHeldRow> emptyVNList = new TcBindingList<TcCommissionsHeldRow>();

        private Dictionary<string, TcCommissionsHeldRow> vnDictionary = new Dictionary<string, TcCommissionsHeldRow>();
        private Dictionary<string, TcBindingList<TcCommissionsHeldRow>> duplicates = new Dictionary<string, TcBindingList<TcCommissionsHeldRow>>();

        public void Load(TcBindingList<TcCommissionsHeldRow> salariesHoldData)
        {
            foreach (TcCommissionsHeldRow data in salariesHoldData)
            {
                if (vnDictionary.ContainsKey(data.VirtualNumber))
                {
                    if (!string.IsNullOrEmpty(data.VirtualNumber)) // Don't insert empty VN as Duplicate
                    {
                        if (duplicates.ContainsKey(data.VirtualNumber))
                        {
                            duplicates[data.VirtualNumber].Add(data);
                        }
                        else
                        {
                            duplicates[data.VirtualNumber] = new TcBindingList<TcCommissionsHeldRow>() { vnDictionary[data.VirtualNumber], data };
                        }
                    }
                }
                else
                {
                    vnDictionary.Add(data.VirtualNumber, data);
                }

                if (string.IsNullOrEmpty(data.VirtualNumber))
                {
                    emptyVNList.Add(data);
                }

                all.Add(data);
            }
        }

        public bool HasDuplicates()
        {
            return duplicates.Count > 0 ? true : false;
        }

        public bool HasDuplicates(string virtualNumber)
        {
            return duplicates.ContainsKey(virtualNumber);
        }

        public bool HasEmpty()
        {
            return emptyVNList.Count > 0 ? true : false;
        }

        public TcCommissionsHeldRow GetRow(string virtualNumber)
        {
            TcCommissionsHeldRow row = null;

            if (vnDictionary.ContainsKey(virtualNumber))
            {
                row = vnDictionary[virtualNumber];
            }

            return row;
        }

        public TcBindingList<TcCommissionsHeldRow> GetDuplicates()
        {
            TcBindingList<TcCommissionsHeldRow> list = new TcBindingList<TcCommissionsHeldRow>();

            foreach (string virtualNumber in duplicates.Keys)
            {
                TcCommissionsHeldRow data = GetRow(virtualNumber);
                list.Add(data);
            }

            return list;
        }

        public TcBindingList<TcCommissionsHeldRow> GetCommissionsHeldRowsNotMappedWithCommissionsAgents(TcCommissionsTable commissions)
        {
            TcBindingList<TcCommissionsHeldRow> list = new TcBindingList<TcCommissionsHeldRow>();

            if (commissions != null)
            {
                foreach (TcCommissionsHeldRow row in all)
                {
                    if (!string.IsNullOrEmpty(row.VirtualNumber))
                    {
                        TcCommissionsRow commissionsRow = commissions.GetRowWithVN(row.VirtualNumber);
                        if (commissionsRow == null)
                        {
                            list.Add(row);
                        }
                    }
                }
            }

            return list;
        }

        public TcBindingList<TcCommissionsHeldRow> GetVNEmptyList()
        {
            return emptyVNList;
        }

        public TcBindingList<TcCommissionsHeldRow> GetDuplicates(string virtualNumber)
        {
            TcBindingList<TcCommissionsHeldRow> list = new TcBindingList<TcCommissionsHeldRow>();

            if (duplicates.ContainsKey(virtualNumber))
            {
                list = duplicates[virtualNumber];
            }

            return list;
        }
    }
}
