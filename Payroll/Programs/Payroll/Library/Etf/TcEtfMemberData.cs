
// Harshan Nishantha
// 2013-12-31

using Payroll.UI.Business.Analyze;

namespace Payroll.Library.Etf
{
    public class TcEtfMemberData
    {
        public string MemberNumber { get; set; }                   // 6 numeric
        public string Initials { get; set; }                    // 20 text
        public string Surname { get; set; }                     // 30 text
        public string NICNumber { get; set; }                   // 10 text
        public decimal TotalContribution { get; set; }          // 14 numeric, in cents

        public TcEtfMemberData()
        {
            MemberNumber        = "";
            Initials            = "";
            Surname             = "";
            NICNumber           = "";
            TotalContribution   = 0;
        }

        public TcEtfMemberData(TcBusinessAnalyzedRow member)
        {
            MemberNumber = member.EmployeeNumber;
            Initials = member.Initials;
            Surname = member.LastName;
            NICNumber = member.NIC;
            TotalContribution = member.EtfContribution;
        }
    }
}
