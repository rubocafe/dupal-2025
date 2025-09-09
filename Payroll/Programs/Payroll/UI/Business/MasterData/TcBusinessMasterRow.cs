using Payroll.Library.Excel;
using Payroll.Library.General;
using Payroll.UI.Common;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-26

namespace Payroll.UI.Business.MasterData
{
    public class TcBusinessMasterRow : TcBaseRow, TiSearchable
    {
        public string EmployeeNumber { get; set; }
        public string NIC { get; set; }
        public string NameWithInitials { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string OCGrade { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public decimal BasicSalary { get; set; }
        public Nullable<DateTime> DateOfJoin { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }

        public string[] SearchableFields()
        {
            string[] fields = { EmployeeNumber, NIC, NameWithInitials, Designation, AddressLine1, AddressLine2, City, 
                                  AccountNumber, Bank, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }

        public override void LoadToVariables()
        {
            object value = Get(TcPropertyNames.EmployeeNumber);
            EmployeeNumber = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.NIC);
            NIC = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.NameWithInitials);
            NameWithInitials = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.Initials);
            Initials = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.LastName);
            LastName = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.Designation);
            Designation = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.OCGrade);
            OCGrade = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.AddressLine1);
            AddressLine1 = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.AddressLine2);
            AddressLine2 = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.City);
            City = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.BasicSalary);
            BasicSalary = TcExcelValueDecorder.GetDecimal(value);

            value = Get(TcPropertyNames.DateOfJoin);
            DateOfJoin = TcExcelValueDecorder.GetDate(value);

            value = Get(TcPropertyNames.Bank);
            Bank = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.Branch);
            Branch = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.BankCode);
            BankCode = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.BranchCode);
            BranchCode = TcExcelValueDecorder.GetString(value);

            value = Get(TcPropertyNames.AccountNumber);
            AccountNumber = TcExcelValueDecorder.GetString(value);

            Clean();
        }

        private void Clean()
        {
            EmployeeNumber      = TcFormatter.TrimAndUpper(EmployeeNumber);
            NIC                 = TcFormatter.GetFormattedNIC(NIC);
            NameWithInitials    = TcFormatter.TrimAndUpper(NameWithInitials);
            Initials            = TcFormatter.GetFormattedInitials(Initials);
            LastName            = TcFormatter.GetFormattedLastName(LastName);
            OCGrade             = TcFormatter.TrimAndUpper(OCGrade);

            AddressLine1        = TcFormatter.TrimAndUpper(AddressLine1);
            AddressLine2        = TcFormatter.TrimAndUpper(AddressLine2);
            City                = TcFormatter.TrimAndUpper(City);

            Bank                = TcFormatter.TrimAndUpper(Bank);
            Branch              = TcFormatter.TrimAndUpper(Branch);
            BankCode            = TcFormatter.GetFormattedBankCode(BankCode);
            BranchCode          = TcFormatter.GetFormattedBranchCode(BranchCode);

            AccountNumber       = TcFormatter.GetFormattedBankAccountNumber(AccountNumber);
        }
    }
}
