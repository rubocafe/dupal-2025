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
    public class TcMasterMetaData : TaMetaDataBase<TcMasterMetaDataRow>
    {
        public override void AddRequiredProperties()
        {
            RequiredProperties.Add(TcPropertyNames.EmployeeNumber);
            RequiredProperties.Add(TcPropertyNames.NIC);
            RequiredProperties.Add(TcPropertyNames.NameWithInitials);
            RequiredProperties.Add(TcPropertyNames.Initials);
            RequiredProperties.Add(TcPropertyNames.LastName);
            RequiredProperties.Add(TcPropertyNames.Designation);
            RequiredProperties.Add(TcPropertyNames.OCGrade);
            RequiredProperties.Add(TcPropertyNames.BasicSalary);
            RequiredProperties.Add(TcPropertyNames.DateOfJoin);
            RequiredProperties.Add(TcPropertyNames.Bank);
            RequiredProperties.Add(TcPropertyNames.BankCode);
            RequiredProperties.Add(TcPropertyNames.Branch);
            RequiredProperties.Add(TcPropertyNames.BranchCode);
            RequiredProperties.Add(TcPropertyNames.AccountNumber);
            RequiredProperties.Add(TcPropertyNames.AddressLine1);
            RequiredProperties.Add(TcPropertyNames.AddressLine2);
            RequiredProperties.Add(TcPropertyNames.City);
        }

        public void Load(string path)
        {
            Load(path, "Master Data");
        }

        public override List<string> GetColumnNames()
        {
            List<string> columns = new List<string>();

            columns.Add("NAME");
            columns.Add("TYPE");

            return columns;
        }

        public override string GetKey(TcExcelTableRow row)
        {
            var key = row.GetCell("NAME").StringValue();
            if (!string.IsNullOrEmpty(key))
            {
                key = key.ToUpper();
            }

            return key;
        }

        public override TcMasterMetaDataRow GetValue(int index, TcExcelTableRow row)
        {
            var name = row.GetCell("NAME").StringValue();
            var type = row.GetCell("TYPE").StringValue();

            var newRow = TcMasterMetaDataRow.New(index, name, type);
            return newRow;
        }

        public override object GetVariableValue(string key)
        {
            return null;
        }

        public override void LoadToVariables()
        {
            // Nothing to do
        }
    }
}
