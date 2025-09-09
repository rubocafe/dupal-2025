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
    public class TcEmployerMetaData : TaMetaDataBase<TcEmployerMetaDataRow>
    {
        public string CompanyName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Bank { get; set; }            // 2016/10/02
        public string BankCode { get; set; }        // 2016/10/25
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string ZoneCode { get; set; }
        public string EmployerNumber { get; set; }

        public TcEmployerMetaData() : 
            base()
        {
        }

        public void Load(string path)
        {
            Load(path, "Data");
        }

        public override void AddRequiredProperties()
        {
            RequiredProperties.Add(TcPropertyNames.CompanyName);
            RequiredProperties.Add(TcPropertyNames.AddressLine1);
            RequiredProperties.Add(TcPropertyNames.AddressLine2);
            RequiredProperties.Add(TcPropertyNames.City);
            RequiredProperties.Add(TcPropertyNames.Telephone);
            RequiredProperties.Add(TcPropertyNames.Fax);
            RequiredProperties.Add(TcPropertyNames.BranchCode);
            RequiredProperties.Add(TcPropertyNames.AccountNumber);
            RequiredProperties.Add(TcPropertyNames.AccountName);
            RequiredProperties.Add(TcPropertyNames.ZoneCode);
            RequiredProperties.Add(TcPropertyNames.EmployerNumber);
        }

        public override void LoadToVariables()
        {
            object value;

            value = GetVariableValue(TcPropertyNames.CompanyName);
            CompanyName = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.AddressLine1);
            AddressLine1 = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.AddressLine2);
            AddressLine2 = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.City);
            City = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.Telephone);
            Telephone = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.Fax);
            Fax = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.BankCode);
            BankCode = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.BranchCode);
            BranchCode = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.AccountNumber);
            AccountNumber = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.AccountName);
            AccountName = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.ZoneCode);
            ZoneCode = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.EmployerNumber);
            EmployerNumber = TcExcelValueDecorder.GetString(value);

            value = GetVariableValue(TcPropertyNames.Bank);
            Bank = TcExcelValueDecorder.GetString(value);

            Clean();
        }

        private void Clean()
        {
            AddressLine1    = TcFormatter.TrimAndUpper(AddressLine1);
            AddressLine2    = TcFormatter.TrimAndUpper(AddressLine2);
            City            = TcFormatter.TrimAndUpper(City);
            BankCode        = TcFormatter.GetFormattedBankCode(BankCode);
            BranchCode      = TcFormatter.GetFormattedBranchCode(BranchCode);
            AccountNumber   = TcFormatter.GetFormattedBankAccountNumber(AccountNumber);
            EmployerNumber  = TcFormatter.TrimAndUpper(EmployerNumber);
            Bank            = TcFormatter.TrimAndUpper(Bank);

            if (string.IsNullOrEmpty(Bank))
            {
                Bank = "COM";
            }

            // 2016/10/25
            if (string.IsNullOrEmpty(BankCode))
            {
                BankCode = "7056";
            }
        }

        public override List<string> GetColumnNames()
        {
            List<string> columns = new List<string>();

            columns.Add("KEY");
            columns.Add("VALUE");

            return columns;
        }

        public override string GetKey(TcExcelTableRow row)
        {
            var key = row.GetCell("KEY").StringValue();
            if (!string.IsNullOrEmpty(key))
            {
                key = key.ToUpper();
            }

            return key;     
        }

        public override TcEmployerMetaDataRow GetValue(int index, TcExcelTableRow row)
        {
            var key     = row.GetCell("KEY").StringValue();
            var value   = row.GetCell("VALUE").StringValue();

            var newRow = TcEmployerMetaDataRow.New(index, key, value);

            return newRow;
        }

        public override object GetVariableValue(string key)
        {
            object value = null;

            var x = GetData(key);
            if (x != null)
            {
                value = x.Value;
            }

            return value;
        }
    }
}
