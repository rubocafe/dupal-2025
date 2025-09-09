using DUPALPayroll.Library;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.Common.BanksAndBranches
{
    public class TcBanksAndBranchesTable
    {
        private Dictionary<string, TcBanksAndBranchesRow> allBankAndBranches = new Dictionary<string, TcBanksAndBranchesRow>();
        private Dictionary<string, int> allBanks = new Dictionary<string, int>();

        private Dictionary<string, TcBindingList<TcBanksAndBranchesRow>> duplicates = new Dictionary<string, TcBindingList<TcBanksAndBranchesRow>>();

        private TcBanksAndBranchesRow commercialDefault;
        private TcBanksAndBranchesRow sampathDefault;

        public TcBanksAndBranchesTable()
        {
            commercialDefault = new TcBanksAndBranchesRow();
            commercialDefault.BankName          = "COMMERCIAL BANK";
            commercialDefault.Bank   = "COM";
            commercialDefault.BankCode      = 7056;
            commercialDefault.Branch        = "DEFAULT";
            commercialDefault.BranchCode    = 1;

            sampathDefault = new TcBanksAndBranchesRow();
            sampathDefault.BankName         = "SAMPATH BANK";
            sampathDefault.Bank  = "SAM";
            sampathDefault.BankCode     = 7278;
            sampathDefault.Branch       = "DEFAULT";
            sampathDefault.BranchCode   = 1;
        }

        public void Load(TcBindingList<TcBanksAndBranchesRow> banksAndBranchesDataData)
        {
            foreach (TcBanksAndBranchesRow data in banksAndBranchesDataData)
            {
                string key = "";

                if (string.IsNullOrEmpty(data.Bank))
                {
                    key = string.Format("{0}_{1}", data.BankName, data.Branch);
                }
                else
                {
                    key = string.Format("{0}_{1}", data.Bank, data.Branch);
                }

                data.Key = key;

                if (!allBankAndBranches.ContainsKey(key))
                {
                    allBankAndBranches.Add(key, data);
                }
                else
                {
                    if (duplicates.ContainsKey(key))
                    {
                        duplicates[key].Add(data);
                    }
                    else
                    {
                        duplicates[key] = new TcBindingList<TcBanksAndBranchesRow>() { allBankAndBranches[key], data };
                    }
                }

                if (!allBanks.ContainsKey(data.Bank))
                {
                    allBanks.Add(data.Bank, data.BankCode);
                }
            }
        }

        public TcBanksAndBranchesRow GetRow(string bankAcronym, string branch)
        {
            string key = string.Format("{0}_{1}", bankAcronym, branch);

            if (bankAcronym == "COM")
            {
                return commercialDefault;
            }
            else if (bankAcronym == "SAM")
            {
                return sampathDefault;
            }

            if (allBankAndBranches.ContainsKey(key))
            {
                TcBanksAndBranchesRow data = allBankAndBranches[key];

                return data;
            }

            return null;
        }

        public TcBanksAndBranchesRow GetRow(string key)
        {
            TcBanksAndBranchesRow data = null;

            if (allBankAndBranches.ContainsKey(key))
            {
                data = allBankAndBranches[key];
            }

            return data;
        }

        public bool HasDuplicates()
        {
            return duplicates.Count > 0 ? true : false;
        }

        public TcBindingList<TcBanksAndBranchesRow> GetDuplicatesList()
        {
            TcBindingList<TcBanksAndBranchesRow> list = new TcBindingList<TcBanksAndBranchesRow>();

            foreach (string item in duplicates.Keys)
            {
                TcBanksAndBranchesRow row = GetRow(item);
                list.Add(row);
            }

            return list;
        }

        public TcBindingList<TcBanksAndBranchesRow> GetDuplicates(string key)
        {
            TcBindingList<TcBanksAndBranchesRow> list = new TcBindingList<TcBanksAndBranchesRow>();

            if (!string.IsNullOrEmpty(key) && duplicates.ContainsKey(key))
            {
                list = duplicates[key];
            }

            return list;
        }
    }
}
