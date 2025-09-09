using Payroll.Library.Excel;
using Payroll.Library.General;
using Payroll.Library.MetaData;
using Payroll.UI.Common;
using Payroll.UI.Controls;
using System;
using System.Linq;
using System.Collections.Generic;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.UI.Business.Salary
{
    public class TcBusinessSalaryRow : TcBaseRow, TiSearchable
    {
        public string EmployeeNumber { get; set; }
        public string NIC { get; set; }
        public string NameWithInitials { get; set; }
        public string Designation { get; set; }
        public Nullable<DateTime> DateOfJoin { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public string MemberStatus { get; set; }
        public double DaysWorked { get; set; }

        public decimal BasicSalary { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal EpfDeduction { get; set; }
        public decimal NetSalary { get; set; }
        public decimal TotalRemuneration { get; set; }
        public decimal BankTransferAmount { get; set; }
        
        public decimal EpfContribution { get; set; }
        public decimal EtfContribution { get; set; }

        public Dictionary<string, decimal> Allowances { get; set; }
        public Dictionary<string, decimal> NoPays { get; set; }
        public Dictionary<string, decimal> Incentives { get; set; }
        public Dictionary<string, decimal> Deductions { get; set; }

        public TcBusinessSalaryRow()
        {
            MemberStatus    = "E";
            DaysWorked      = 0;

            Allowances  = new Dictionary<string, decimal>();
            NoPays      = new Dictionary<string, decimal>();
            Incentives  = new Dictionary<string, decimal>();
            Deductions  = new Dictionary<string, decimal>();
        }

        public virtual string[] SearchableFields()
        {
            string[] fields = { EmployeeNumber, NIC, NameWithInitials, Designation, Bank, Branch, AccountNumber };

            return fields;
        }

        public void LoadFrom(int index, TcExcelTableRow row, TcSalaryMetaData metaData)
        {
            LoadFrom(index, row);
            LoadOtherData(metaData);
        }

        private void LoadOtherData(TcSalaryMetaData metaData)
        {
            var allownacesHeaders = metaData.Data
                .Values
                .Where(m => m.Category == TcMetaDataCategory.Allowance)
                .ToList();

            var columns = metaData.GetDataColumns();
            foreach (var column in columns)
            {
                object value = null;
                decimal price = 0;
                if (column.Category == TcMetaDataCategory.Allowance)
                {
                    value = Get(column.UpperName);
                    price = TcExcelValueDecorder.GetDecimal(value);
                    Allowances.Add(column.Name, price);
                }
                else if (column.Category == TcMetaDataCategory.NoPay)
                {
                    value = Get(column.UpperName);
                    price = TcExcelValueDecorder.GetDecimal(value);
                    NoPays.Add(column.Name, price);
                }
                else if (column.Category == TcMetaDataCategory.Deduction)
                {
                    value = Get(column.UpperName);
                    price = TcExcelValueDecorder.GetDecimal(value);
                    Deductions.Add(column.Name, price);
                }
                else if (column.Category == TcMetaDataCategory.Incentive)
                {
                    value = Get(column.UpperName);
                    price = TcExcelValueDecorder.GetDecimal(value);
                    Incentives.Add(column.Name, price);
                }
            }
        }

        public override void LoadToVariables()
        {
            object value = Get(TcPropertyNames.EmployeeNumber);
            EmployeeNumber = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.NIC);
            NIC = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.NameWithInitials);
            NameWithInitials = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.Designation);
            Designation = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.DateOfJoin);
            DateOfJoin = TcExcelValueDecorder.GetDate(value);

            value = Get(TcPropertyNames.AddressLine1);
            AddressLine1 = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.AddressLine2);
            AddressLine2 = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.City);
            City = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.Bank);
            Bank = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.Branch);
            Branch = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.AccountNumber);
            AccountNumber = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.MemberStatus);
            MemberStatus = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.DaysWorked);
            DaysWorked = TcExcelValueDecorder.GetDouble(value);

            value = Get(TcPropertyNames.BasicSalary);
            BasicSalary = TcExcelValueDecorder.GetDecimal(value);

            value = Get(TcPropertyNames.GrossSalary);
            GrossSalary = TcExcelValueDecorder.GetDecimal(value);

            value = Get(TcPropertyNames.EPFDeduction);
            EpfDeduction = TcExcelValueDecorder.GetDecimal(value);

            value = Get(TcPropertyNames.NetSalary);
            NetSalary = TcExcelValueDecorder.GetDecimal(value);

            value = Get(TcPropertyNames.TotalRemuneration);
            TotalRemuneration = TcExcelValueDecorder.GetDecimal(value);

            value = Get(TcPropertyNames.BankTransferAmount);
            BankTransferAmount = TcExcelValueDecorder.GetDecimal(value);

            value = Get(TcPropertyNames.EPFContribution);
            EpfContribution = TcExcelValueDecorder.GetDecimal(value);

            value = Get(TcPropertyNames.ETFContribution);
            EtfContribution = TcExcelValueDecorder.GetDecimal(value);

            Clean();
        }

        private void Clean()
        {
            EmployeeNumber      = TcFormatter.TrimAndUpper(EmployeeNumber);
            NIC                 = TcFormatter.GetFormattedNIC(NIC);
            NameWithInitials    = TcFormatter.TrimAndUpper(NameWithInitials);

            AddressLine1        = TcFormatter.TrimAndUpper(AddressLine1);
            AddressLine2        = TcFormatter.TrimAndUpper(AddressLine2);
            City                = TcFormatter.TrimAndUpper(City);

            Bank                = TcFormatter.TrimAndUpper(Bank);
            Branch              = TcFormatter.TrimAndUpper(Branch);

            AccountNumber       = TcFormatter.GetFormattedBankAccountNumber(AccountNumber);
        }
    }
}
