using DUPALPayroll.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Payments
{
    public class TcCareTakersPaymentsRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
        public string SiteEngineer { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public decimal Payment { get; set; }
        public decimal Hold { get; set; }

        public string[] GetSearchableFields()
        {
            string[] fields = { SiteName, SiteCode, SiteEngineer, NIC, Name, AccountNumber, Bank, Branch};

            return fields;
        }
    }
}
