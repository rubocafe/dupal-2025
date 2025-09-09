using DUPALPayroll.Controls;
using System;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.CommissionsHeld
{
    public class TcCommissionsHeldRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public string Request { get; set; }
        public string VirtualNumber { get; set; }
        public string Name { get; set; }
        public string Manager { get; set; }
        public decimal NetCommission { get; set; }
        public decimal Hold { get; set; }
        public decimal AmountPayable { get; set; }

        public string[] GetSearchableFields()
        {
            string[] fields = { VirtualNumber, Name, Manager };

            return fields;
        }
    }
}
