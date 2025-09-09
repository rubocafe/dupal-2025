using Payroll.Library.Excel;
using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-11-03

namespace Payroll.Library.MetaData
{
    public class TcBanksAndBranchesMetaData
    {
        public Dictionary<string, TcPropertyNameRow> Data { get; set; }

        public TcBanksAndBranchesMetaData()
        {
            Data = new Dictionary<string, TcPropertyNameRow>();

            Init();
        }

        private void Init()
        {
            Data.Add(TcPropertyNames.BankName, new TcPropertyNameRow(1, "Bank Name", TcMetaDataType.Text));
            Data.Add(TcPropertyNames.Bank, new TcPropertyNameRow(2, "Bank", TcMetaDataType.Text));
            Data.Add(TcPropertyNames.BankCode, new TcPropertyNameRow(3, "Bank Code", TcMetaDataType.Number));
            Data.Add(TcPropertyNames.Branch, new TcPropertyNameRow(4, "Branch", TcMetaDataType.Text));
            Data.Add(TcPropertyNames.BranchCode, new TcPropertyNameRow(5, "Branch Code", TcMetaDataType.Number));
        }

        internal List<string> GetColumnNames()
        {
            List<string> columns = Data.Values
                .OrderBy(v => v.Index)
                .Select(v => v.Name)
                .ToList();

            return columns;
        }

        internal List<TcPropertyNameRow> GetDataColumns()
        {
            var columns = Data.Values
                .OrderBy(v => v.Index)
                .ToList();

            return columns;
        }
    }
}
