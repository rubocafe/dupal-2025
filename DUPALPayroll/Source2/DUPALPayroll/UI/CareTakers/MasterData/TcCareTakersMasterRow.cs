using DUPALPayroll.Controls;
using System;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.MasterData
{
    public class TcCareTakersMasterRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
        public string SiteEngineer { get; set; }
        public string Name { get; set; }
        public string NIC { get; set; }
        public string Address { get; set; }
        public string Bank { get; set; }
        public string Branch { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string AccountNumber { get; set; }

        public string[] GetSearchableFields()
        {
            string[] fields = { SiteName, SiteCode, SiteEngineer, NIC, Name, AccountNumber,  Bank, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }
    }
}
