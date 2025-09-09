using DUPALPayroll.Controls;
using System;

// Harshan Nishantha
// 2013-09-24

namespace DUPALPayroll.UI.Common.MasterBean
{
    public class TcSalaryMasterRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public string EmployeeNumber { get; set; }
        public string NIC { get; set; }
        public string Name { get; set; }
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

        public virtual string[] GetSearchableFields()
        {
            string[] fields = { EmployeeNumber, NIC, Name, Designation, AddressLine1, AddressLine2, City, 
                                  AccountNumber, Bank, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }
    }
}
