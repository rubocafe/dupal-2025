using DUPALPayroll.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.Commissions
{
    public class TcCommissionsRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public string VirtualNumber { get; set; }
        public Nullable<DateTime> DateOfJoin { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string NIC { get; set; }
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public string Branch { get; set; }
        public string SalesManager { get; set; }
        public decimal GrossCommission { get; set; }
        public decimal EPFDeduction { get; set; }
        public decimal NetCommission { get; set; }
        public string TLorBPO { get; set; }

        public string EmployeeNumber { get; set; }
        public decimal EPFContribution { get; set; }
        public decimal ETFContribution { get; set; }
        public decimal Paye { get; set; }
        public string MemberStatus { get; set; }
        public decimal DaysWorked { get; set; }

        public string[] GetSearchableFields()
        {
            string[] fields = { VirtualNumber, NIC, TLorBPO, Name, Bank, Branch, AccountNumber, SalesManager };

            return fields;
        }
    }
}
