
// Harshan Nishantha
// 2013-12-31

namespace DUPALPayroll.UI.Common.Etf
{
    public class TcEtfDetailDestinationData
    {
        public string MemberNumber { get; set; }                   // 6 numeric
        public string Initials { get; set; }                    // 20 text
        public string Surname { get; set; }                     // 30 text
        public string NICNumber { get; set; }                   // 10 text
        public decimal TotalContribution { get; set; }          // 14 numeric, in cents

        public TcEtfDetailDestinationData()
        {
            MemberNumber    = "";
            Initials        = "";
            Surname         = "";
            NICNumber       = "";
        }
    }
}
