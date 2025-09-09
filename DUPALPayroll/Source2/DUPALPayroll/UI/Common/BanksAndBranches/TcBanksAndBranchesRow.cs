using DUPALPayroll.Controls;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.Common.BanksAndBranches
{
    public class TcBanksAndBranchesRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public string Key { get; set; }
        public int BankCode { get; set; }
        public int BranchCode { get; set; }
        public string Bank { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }

        public string[] GetSearchableFields()
        {
            string[] fields = { Bank, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }
    }
}
