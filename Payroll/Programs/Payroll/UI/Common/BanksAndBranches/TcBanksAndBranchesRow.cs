using Payroll.Library.Excel;
using Payroll.Library.General;
using Payroll.UI.Controls;

// Harshan Nishantha
// 2013-08-26

namespace Payroll.UI.Common.BanksAndBranches
{
    public class TcBanksAndBranchesRow : TcBaseRow, TiSearchable
    {
        public string   Key { get; set; }
        public int      BankCode { get; set; }
        public int      BranchCode { get; set; }
        public string   Bank { get; set; }
        public string   BankName { get; set; }
        public string   Branch { get; set; }

        public string[] SearchableFields()
        {
            string[] fields = { Bank, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }

        public override void LoadToVariables()
        {
            object value = Get(TcPropertyNames.BankCode);
            BankCode = TcExcelValueDecorder.GetInt(value);

            value = Get(TcPropertyNames.BranchCode);
            BranchCode = TcExcelValueDecorder.GetInt(value);

            value = Get(TcPropertyNames.Bank);
            Bank = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.BankName);
            BankName = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.Branch);
            Branch = TcExcelValueDecorder.GetString(value);

            Clean();
        }

        private void Clean()
        {
            Bank = TcFormatter.UpperAndRemoveSpaces(Bank);
            BankName = TcFormatter.TrimAndUpper(BankName);
            Branch = TcFormatter.TrimAndUpper(Branch);
        }
    }
}
