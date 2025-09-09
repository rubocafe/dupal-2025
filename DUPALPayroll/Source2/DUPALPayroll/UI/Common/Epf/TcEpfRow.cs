using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-12-23

namespace DUPALPayroll.UI.Common.Epf
{
    public class TcEpfRow
    {
        public int LineNumber { get; set; } // Needed in read from file senario

        public string NICNumber { get; set; }                   // 20 Text
        public string LastName { get; set; }                    // 40 Text
        public string Initials { get; set; }                    // 20 Text
        public string MemberNumber { get; set; }                 // 6 numeric
        public decimal TotalContribution { get; set; }          // 9.2
        public decimal EmployersContribution { get; set; }      // 9.2 = There should be maximum of 10 digits including 7 integers, decimal point & 2 decimals. E.g. 0001535.73
        public decimal MembersContribution { get; set; }        // 9.2
        public decimal TotalEarnings { get; set; }              // 11.2 = There should be maximum of  12 digits including 9 integers, decimal point  & 2 decimals.  E.g. 000014758.55
        public string MemberStatus { get; set; }                // 1 Text (E=Extg, N=New, V=Vacated)
        public string ZoneCode { get; set; }                    // 1 Text "A"
        public string EmployerNumber { get; set; }                 // 6 numeric
        public TcYearMonth ContributionPeriod { get; set; }        // 6 numeric Contribution YYYYMM
        public int SubmitionId { get; set; }                    // 2 numeric, 01 = all staff in one file / 02 = Staff in separate categories as "Executive", "non-Executive"
        public decimal DaysOfWork { get; set; }                 // 4.2 Numeric
        public string OccupationClassificationGrade { get; set; }  // 3 Numeric

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
            SubmitionId             = 1;
            DaysOfWork              = 0;
            OccupationClassificationGrade = "";

            Errors = new Dictionary<TeEpfError, string>();
        }

        public static TcEpfRow GetEpfRow(TcEpfOriginData origin, TcEpfDestinationData destination)
        {
            TcEpfRow row = new TcEpfRow();

            row.NICNumber                       = destination.NICNumber;
            row.LastName                        = destination.LastName;
            row.Initials                        = destination.Initials;
            row.MemberNumber                    = destination.MemberNumber;
            row.TotalContribution               = destination.TotalContribution;
            row.EmployersContribution           = destination.EmployersContribution;
            row.MembersContribution             = destination.MembersContribution;
            row.TotalEarnings                   = destination.TotalEarnings;
            row.MemberStatus                    = destination.MemberStatus;
            row.ZoneCode                        = origin.ZoneCode;
            row.EmployerNumber                  = origin.EmployerNumber;
            row.ContributionPeriod              = origin.ContributionPeriod;
            row.SubmitionId                     = origin.SubmitionId;
            row.DaysOfWork                      = destination.DaysOfWork;
            row.OccupationClassificationGrade   = destination.OccupationClassificationGrade;

            return row;
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

            if (!IsValueInRange(DaysOfWork, 0, 365, "Days of Work", TeEpfError.Invalid_Days_of_Work_Value))
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

            if (!TcValidator.IsValidOCGrade(OccupationClassificationGrade))
            {
                valid = false;
                Errors.Add(TeEpfError.Invalid_OCGrade, string.Format("OC Grade [{0}] is not valid", OccupationClassificationGrade));
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
                TcString.AppendSpacesToFront(SubmitionId.ToString(), 2),
                TcString.AppendSpacesToFront(DaysOfWork.ToString("N2").Replace(",", ""), 5),
                TcString.AppendSpacesToFront(OccupationClassificationGrade.ToString(), 3));

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
            row.Fields.Add(new TcCsvDataField(SubmitionId));
            row.Fields.Add(new TcCsvDataField(DaysOfWork));
            row.Fields.Add(new TcCsvDataField(OccupationClassificationGrade));

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
