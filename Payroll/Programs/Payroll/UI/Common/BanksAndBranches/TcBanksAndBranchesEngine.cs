using Payroll.Library;
using Payroll.Library.General;
using Payroll.UI.Controls;
using System.Collections.Generic;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.UI.Common.BanksAndBranches
{
    public class TcBanksAndBranchesEngine
    {
        private List<TcBanksAndBranchesRow> allList = new List<TcBanksAndBranchesRow>();
        private Dictionary<string, TcBanksAndBranchesRow> allBankAndBranches = new Dictionary<string, TcBanksAndBranchesRow>();
        private Dictionary<string, int> allBanks = new Dictionary<string, int>();

        private Dictionary<string, List<TcBanksAndBranchesRow>> duplicates = new Dictionary<string, List<TcBanksAndBranchesRow>>();

        private TcBanksAndBranchesRow commercialDefault;
        private TcBanksAndBranchesRow sampathDefault;

        public TcBanksAndBranchesEngine(List<TcBanksAndBranchesRow> allList)
        {
            commercialDefault = new TcBanksAndBranchesRow();
            commercialDefault.BankName      = "COMMERCIAL BANK";
            commercialDefault.Bank          = "COM";
            commercialDefault.BankCode      = 7056;
            commercialDefault.Branch        = "DEFAULT";
            commercialDefault.BranchCode    = 1;

            sampathDefault = new TcBanksAndBranchesRow();
            sampathDefault.BankName     = "SAMPATH BANK";
            sampathDefault.Bank         = "SAM";
            sampathDefault.BankCode     = 7278;
            sampathDefault.Branch       = "DEFAULT";
            sampathDefault.BranchCode   = 1;

            this.allList = allList;
            Load(allList);
        }

        public void Load(List<TcBanksAndBranchesRow> banksAndBranchesDataData)
        {
            foreach (TcBanksAndBranchesRow data in banksAndBranchesDataData)
            {
                string key = GetKey(data.BankName, data.Bank, data.Branch);
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
                        duplicates[key] = new List<TcBanksAndBranchesRow>() { allBankAndBranches[key], data };
                    }
                }

                if (!allBanks.ContainsKey(data.Bank))
                {
                    allBanks.Add(data.Bank, data.BankCode);
                }
            }
        }

        public List<TcBanksAndBranchesRow> FilterAndSearch(string filterText, string searchText)
        {
            TeBanksAndBranchesFilter filter = TcEnum.GetEnumForText<TeBanksAndBranchesFilter>(TeBanksAndBranchesFilter.All, filterText);
            var data = allList;

            switch (filter)
            {
                case TeBanksAndBranchesFilter.Duplicates:
                    data = GetDuplicatesList();
                    break;

                default:
                    break;
            }

            TcListSearchHelper<TcBanksAndBranchesRow> searchHelper = new TcListSearchHelper<TcBanksAndBranchesRow>();
            data = searchHelper.Search(data, searchText);
            return data;
        }

        public string GetKey(string bankName, string bank, string branch)
        {
            string key = "";

            if (string.IsNullOrEmpty(bank))
            {
                key = string.Format("{0}_{1}", bankName, branch);
            }
            else
            {
                key = string.Format("{0}_{1}", bank, branch);
            }

            return key;
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

        public List<TcBanksAndBranchesRow> GetDuplicatesList()
        {
            List<TcBanksAndBranchesRow> list = new List<TcBanksAndBranchesRow>();

            foreach (string item in duplicates.Keys)
            {
                TcBanksAndBranchesRow row = GetRow(item);
                list.Add(row);
            }

            return list;
        }

        public List<TcBanksAndBranchesRow> GetDuplicates(string key)
        {
            List<TcBanksAndBranchesRow> list = new List<TcBanksAndBranchesRow>();

            if (!string.IsNullOrEmpty(key) && duplicates.ContainsKey(key))
            {
                list = duplicates[key];
            }

            return list;
        }
    }
}
