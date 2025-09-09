using DUPALPayroll.Controls;
using System;

// Harshan Nishantha
// 2013-09-24

namespace DUPALPayroll.UI.Common.SalaryBean
{
    public class TcSalaryRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string EmployeeNumber { get; set; }
        public string Designation { get; set; }
        public Nullable<DateTime> DateOfJoin { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal BRA { get; set; }
        public decimal GrossSalary { get; set; }
        public decimal EPFDeduction { get; set; }
        public decimal NetSalary { get; set; }
        public decimal TotalRemuneration { get; set; }
        public decimal Payment { get; set; }
        public decimal Hold { get; set; }
        public decimal BankTransferAmount { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public decimal EPFContribution { get; set; }
        public decimal ETFContribution { get; set; }
        public decimal Paye { get; set; }
        public string MemberStatus { get; set; }
        public decimal DaysWorked { get; set; }

        public TcSalaryRow()
        {
            MemberStatus    = "E";
            DaysWorked      = 0;
        }

        public virtual string[] GetSearchableFields()
        {
            string[] fields = { Name, NIC, Name, EmployeeNumber, Designation, Bank, Branch, AccountNumber};

            return fields;
        }
    }
}
