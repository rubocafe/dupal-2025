using DUPALPayroll.Controls;
using System;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.MasterData
{
    public class TcCommissionAgentsMasterRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public int Index { get; set; }
        public string EmployeeNumber { get; set; }
        public string NameWithInitials { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
        public string OCGrade { get; set; }
        public string NIC { get; set; }
        public string VirtualNumber { get; set; }
        public string AccountNumber { get; set; }
        public Nullable<DateTime> DateOfJoin { get; set; }
        public string Address { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string BankAcronym { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }

        public string[] GetSearchableFields()
        {
            string[] fields = { VirtualNumber, NIC, NameWithInitials, AccountNumber,  BankAcronym, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }
    }
}
