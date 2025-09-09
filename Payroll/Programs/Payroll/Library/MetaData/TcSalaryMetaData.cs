using Payroll.Library.Excel;
using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-13

namespace Payroll.Library.MetaData
{
    public class TcSalaryMetaData : TaMetaDataBase<TcSalaryMetaDataRow>
    {
        public override void AddRequiredProperties()
        {
            RequiredProperties.Add(TcPropertyNames.EmployeeNumber);
            RequiredProperties.Add(TcPropertyNames.NIC);
            RequiredProperties.Add(TcPropertyNames.NameWithInitials);
            RequiredProperties.Add(TcPropertyNames.Designation);
            RequiredProperties.Add(TcPropertyNames.DateOfJoin);
            RequiredProperties.Add(TcPropertyNames.AddressLine1);
            RequiredProperties.Add(TcPropertyNames.AddressLine2);
            RequiredProperties.Add(TcPropertyNames.City);
            RequiredProperties.Add(TcPropertyNames.Bank);
            RequiredProperties.Add(TcPropertyNames.Branch);
            RequiredProperties.Add(TcPropertyNames.AccountNumber);
            RequiredProperties.Add(TcPropertyNames.MemberStatus);
            RequiredProperties.Add(TcPropertyNames.DaysWorked);
            RequiredProperties.Add(TcPropertyNames.BasicSalary);
            RequiredProperties.Add(TcPropertyNames.GrossSalary);
            RequiredProperties.Add(TcPropertyNames.EPFDeduction);
            RequiredProperties.Add(TcPropertyNames.NetSalary);
            RequiredProperties.Add(TcPropertyNames.TotalRemuneration);
            RequiredProperties.Add(TcPropertyNames.BankTransferAmount);
            RequiredProperties.Add(TcPropertyNames.EPFContribution);
            RequiredProperties.Add(TcPropertyNames.ETFContribution);
        }

        public void Load(string path)
        {
            Load(path, "Salary");
        }

        public override List<string> GetColumnNames()
        {
            List<string> columns = new List<string>();

            columns.Add("NAME");
            columns.Add("CATEGORY");
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

        public override TcSalaryMetaDataRow GetValue(int index, TcExcelTableRow row)
        {
            var name        = row.GetCell("NAME").StringValue();
            var category    = row.GetCell("CATEGORY").StringValue();
            var type        = row.GetCell("TYPE").StringValue();

            var newRow = TcSalaryMetaDataRow.New(index, name, category, type);
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
