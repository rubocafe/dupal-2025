using Payroll.General;
using Payroll.Library;
using Payroll.Library.Csv;
using Payroll.Library.Date;
using Payroll.Library.General;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-12-23

namespace Payroll.Library.Epf
{
    public class TcEpfRow
    {
        public int LineNumber { get; set; } // Needed in read from file senario

        /// <summary>20 Text</summary>
        public string NICNumber { get; set; }

        /// <summary>40 Text</summary>
        public string LastName { get; set; }

        /// <summary>20 Text</summary>
        public string Initials { get; set; }

        /// <summary>6 numeric</summary>
        public string MemberNumber { get; set; }
          
        /// <summary>9.2</summary>
        public decimal TotalContribution { get; set; }
 
        /// <summary>
        /// 9.2
        /// <para>There should be maximum of 10 digits including 7 integers, decimal point &amp; 2 decimals. E.g. 0001535.73</para>
        /// </summary>
        public decimal EmployersContribution { get; set; }

        /// <summary>9.2</summary>
        public decimal MembersContribution { get; set; }

        /// <summary>
        /// 11.2
        /// <para>There should be maximum of  12 digits including 9 integers, decimal point &amp; 2 decimals.</para>
        /// E.g. 000014758.55
        /// </summary>
        public decimal TotalEarnings { get; set; }

        /// <summary>1 Text (E=Extg, N=New, V=Vacated)</summary>
        public string MemberStatus { get; set; }

        /// <summary>1 Text "A"</summary>
        public string ZoneCode { get; set; }

        /// <summary>6 numeric</summary>
        public string EmployerNumber { get; set; }

        /// <summary>6 numeric Contribution YYYYMM</summary>
        public TcYearMonth ContributionPeriod { get; set; }

        /// <summary>
        /// 2 numeric
        /// 01 = all staff in one file / 02 = Staff in separate categories as "Executive", "non-Executive"
        /// </summary>
        public int SubmissionId { get; set; }

        /// <summary>4.2 Numeric</summary>
        public decimal DaysOfWork { get; set; }

        /// <summary>3 Numeric</summary>
        public string OCGrade { get; set; }

        public Dictionary<TeEpfError, string> Errors { get; set; }

        public TcEpfRow()
        {
            NICNumber               = "";
            LastName                = "";
            Initials                = "";
            MemberNumber            = "";
            EmployersContribution   = MembersContribution = 0;
            TotalEarnings           = 0;
            MemberStatus            = "E";
            ZoneCode                = "A";
            EmployerNumber          = "";
            SubmissionId            = 1;
            DaysOfWork              = 0;
            OCGrade                 = "";

            Errors = new Dictionary<TeEpfError, string>();
        }

        public TcEpfRow(TcEpfEmployerData employer, TcEpfMemberData member)
        {
            NICNumber                       = member.NICNumber;
            LastName                        = member.LastName;
            Initials                        = member.Initials;
            MemberNumber                    = member.MemberNumber;
            TotalContribution               = member.TotalContribution;
            EmployersContribution           = member.EmployersContribution;
            MembersContribution             = member.MembersContribution;
            TotalEarnings                   = member.TotalEarnings;
            MemberStatus                    = member.MemberStatus;
            ZoneCode                        = employer.ZoneCode;
            EmployerNumber                  = employer.EmployerNumber;
            ContributionPeriod              = employer.ContributionPeriod;
            SubmissionId                    = employer.SubmissionId;
            DaysOfWork                      = member.DaysOfWork;
            OCGrade                         = member.OCGrade;
        }

        public bool IsValid()
        {
            bool valid = true;
            Errors.Clear();

            if (string.IsNullOrEmpty(NICNumber))
            {
                valid = false;
                Errors.Add(TeEpfError.Empty_NIC_Number, "Empty NIC Number");
            }
            else
            {
                if (!TcValidator.IsValidNIC(NICNumber))
                {
                    valid = false;
                    Errors.Add(TeEpfError.Invalid_NIC_Number, string.Format("Invalid NIC Number [{0}]", NICNumber));
                }
            }

            if (!TcValidator.IsValidEmployeeOrEmployerNumberAfterClean(MemberNumber))
            {
                valid = false;
                Errors.Add(TeEpfError.Invalid_Member_Number, string.Format("Member Number[{0}] is invalid", MemberNumber));
            }

            if (!TcValidator.IsValidEmployeeOrEmployerNumberAfterClean(EmployerNumber))
            {
                valid = false;
                Errors.Add(TeEpfError.Invalid_Employer_Number, string.Format("Employer number[{0}] is invalid", EmployerNumber));
            }

            if (string.IsNullOrEmpty(LastName))
            {
                valid = false;
                Errors.Add(TeEpfError.Empty_Member_Name, string.Format("Member name is not set"));
            }

            if (!TcValidator.IsValidEpfMemberStatus(MemberStatus))
            {
                valid = false;
                Errors.Add(TeEpfError.Invalid_Member_Status, string.Format("Member status [{0}] is not valid", MemberStatus));
            }

            if (!IsValueInRange(Convert.ToDecimal(DaysOfWork), 0, 365, "Days of Work", TeEpfError.Invalid_Days_of_Work_Value))
            {
                valid = false;
            }

            decimal minimum = 0m;
            decimal maximum = new decimal(9999999.99);
            if (!IsValueInRange(TotalContribution, minimum, maximum, "Total Contribution", TeEpfError.Invalid_Total_Contribution_Value))
            {
                valid = false;
            }

            if (!IsValueInRange(EmployersContribution, minimum, maximum, "Employer Contribution", TeEpfError.Invalid_Employer_Contribution_Value))
            {
                valid = false;
            }

            if (!IsValueInRange(MembersContribution, minimum, maximum, "Member Contribution", TeEpfError.Invalid_Member_Contribution_Value))
            {
                valid = false;
            }

            maximum = new decimal(999999999.99);
            if (!IsValueInRange(TotalEarnings, minimum, maximum, "Total Earnings", TeEpfError.Invalid_Total_Earnings_Value))
            {
                valid = false;
            }

            if (!TcValidator.IsValidOCGrade(OCGrade))
            {
                valid = false;
                Errors.Add(TeEpfError.Invalid_OCGrade, string.Format("OC Grade [{0}] is not valid", OCGrade));
            }

            return valid;
        }

        private bool IsValueInRange(decimal value, decimal minimum, decimal maximum, string valueName, TeEpfError error)
        {
            bool valid = true;

            if (!TcValidator.IsValueInRange<decimal>(value, minimum, maximum))
            {
                valid = false;
                Errors.Add(error,
                    string.Format("{0} value [{1}] is invalid. Value must be in between [{2}] and [{3}]",
                    valueName, value.ToString("N2"), minimum.ToString("N2"), maximum.ToString("N2")));
            }

            return valid;
        }

        public string GetPayMasterRow()
        {
            int memberNumber = int.Parse(MemberNumber);
            int employerNumber = int.Parse(EmployerNumber);

            string row = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}",
                TcString.AppendSpacesToEnd(NICNumber, 20),
                TcString.AppendSpacesToEnd(LastName, 40),
                TcString.AppendSpacesToEnd(Initials, 20),
                TcString.AppendSpacesToFront(memberNumber.ToString(), 6),
                CurrencyToString(TotalContribution, 10),
                CurrencyToString(EmployersContribution, 10),
                CurrencyToString(MembersContribution, 10),
                CurrencyToString(TotalEarnings, 12),
                MemberStatus,
                ZoneCode,
                TcString.AppendSpacesToFront(employerNumber.ToString(), 6),
                ContributionPeriod.ToDate().ToString("yyyyMM"),
                TcString.AppendSpacesToFront(SubmissionId.ToString(), 2),
                TcString.AppendSpacesToFront(DaysOfWork.ToString("N2").Replace(",", ""), 5),
                TcString.AppendSpacesToFront(OCGrade.ToString(), 3));

            return row;
        }

        private string CurrencyToString(decimal value, int length)
        {
            string newValue = TcString.AppendSpacesToFront(value.ToString("N2").Replace(",", ""), length);

            return newValue;
        }

        public TcCsvDataRow GetCsvRow()
        {
            TcCsvDataRow row = new TcCsvDataRow();

            row.Fields.Add(new TcCsvDataField(NICNumber));
            row.Fields.Add(new TcCsvDataField(LastName));
            row.Fields.Add(new TcCsvDataField(Initials));
            row.Fields.Add(new TcCsvDataField(MemberNumber));
            row.Fields.Add(new TcCsvDataField(TotalContribution.ToString("N2")));
            row.Fields.Add(new TcCsvDataField(EmployersContribution));
            row.Fields.Add(new TcCsvDataField(MembersContribution));
            row.Fields.Add(new TcCsvDataField(TotalEarnings));
            row.Fields.Add(new TcCsvDataField(MemberStatus));
            row.Fields.Add(new TcCsvDataField(ZoneCode));
            row.Fields.Add(new TcCsvDataField(EmployerNumber));
            row.Fields.Add(new TcCsvDataField(ContributionPeriod.ToString()));
            row.Fields.Add(new TcCsvDataField(SubmissionId));
            row.Fields.Add(new TcCsvDataField(DaysOfWork));
            row.Fields.Add(new TcCsvDataField(OCGrade));

            return row;
        }

        public static TcCsvDataRow GetCsvHeaderRow()
        {
            TcCsvDataRow row = new TcCsvDataRow();

            row.Fields.Add(new TcCsvDataField("NICNumber"));
            row.Fields.Add(new TcCsvDataField("LastName"));
            row.Fields.Add(new TcCsvDataField("Initials"));
            row.Fields.Add(new TcCsvDataField("MemberAcNumber"));
            row.Fields.Add(new TcCsvDataField("TotalContribution"));
            row.Fields.Add(new TcCsvDataField("EmployersContribution"));
            row.Fields.Add(new TcCsvDataField("MembersContribution"));
            row.Fields.Add(new TcCsvDataField("TotalEarnings"));
            row.Fields.Add(new TcCsvDataField("MemberStatus"));
            row.Fields.Add(new TcCsvDataField("ZoneCode"));
            row.Fields.Add(new TcCsvDataField("EmployerNumber"));
            row.Fields.Add(new TcCsvDataField("ContributionPeriod"));
            row.Fields.Add(new TcCsvDataField("SubmitionId"));
            row.Fields.Add(new TcCsvDataField("DaysOfWork"));
            row.Fields.Add(new TcCsvDataField("OccupationClassificationGrade"));

            return row;
        }

        public string GetErrors()
        {
            string errors = "";

            int index = 1;
            foreach (string error in Errors.Values)
            {
                errors += string.Format("({0}) {1}\n", index, error);
                index++;
            }

            return errors;
        }
    }
}
