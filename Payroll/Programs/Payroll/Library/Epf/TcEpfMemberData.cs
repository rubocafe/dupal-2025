using Payroll.Library;
using Payroll.UI.Business.Analyze;
using Payroll.Validators;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-12-23

namespace Payroll.Library.Epf
{
    public class TcEpfMemberData
    {
        public int LineNumber { get; set; } // Needed in read from file senario

        public string NICNumber { get; set; }                       // 20 Text
        public string LastName { get; set; }                        // 40 Text
        public string Initials { get; set; }                        // 20 Text
        public decimal TotalContribution { get; set; }              // 9.2
        public decimal EmployersContribution { get; set; }          // 9.2 = There should be maximum of 10 digits including 7 integers, decimal point & 2 decimals. E.g. 0001535.73
        public decimal MembersContribution { get; set; }            // 9.2
        public decimal TotalEarnings { get; set; }                  // 11.2 = There should be maximum of  12 digits including 9 integers, decimal point  & 2 decimals.  E.g. 000014758.55
        public string  MemberStatus { get; set; }                   // 1 Text (E=Extg, N=New, V=Vacated)
        public string  MemberNumber { get; set; }                   // 6 numeric
        public decimal DaysOfWork { get; set; }                     // 4.2 Numeric
        public string OCGrade { get; set; }   // 3 Numeric

        public TcEpfMemberData()
        {
            NICNumber               = "";
            LastName                = "";
            Initials                = "";
            EmployersContribution   = 0;
            MembersContribution     = 0;
            TotalEarnings           = 0;
            MemberStatus            = "E";
            MemberNumber            = "";
            DaysOfWork              = 0;
            OCGrade                 = "";
        }

        public TcEpfMemberData(TcBusinessAnalyzedRow row)
        {
            NICNumber               = row.NIC;
            Initials                = row.Initials;
            LastName                = row.LastName;
            EmployersContribution   = row.EpfContribution;
            MembersContribution     = row.EpfDeduction;
            TotalEarnings           = row.GrossSalary;
            MemberStatus            = row.MemberStatus;
            MemberNumber            = row.EmployeeNumber;
            DaysOfWork              = decimal.Parse(row.DaysWorked.ToString());
            OCGrade                 = row.OCGrade;

            TotalContribution       = EmployersContribution + MembersContribution;
        }
    }
}
