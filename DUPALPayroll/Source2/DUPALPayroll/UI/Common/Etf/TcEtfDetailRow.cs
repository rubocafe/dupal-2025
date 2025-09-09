using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Harshan Nishantha
// 2013-12-23

namespace DUPALPayroll.UI.Common.Etf
{
    public class TcEtfDetailRow
    {
        public int LineNumber { get; set; } // Needed in read from file senario
        /*
         * All numeric fields should be right aligned and filled with leading zeros.
         * */

        public string Identification { get; private set; }      // 1 text, Default "D"
        public string EmployerNumber { get; set; }              // 8 text AANNNNNN
        public string MemberNumber { get; set; }                // 6 numeric
        public string Initials { get; set; }                    // 20 text
        public string Surname { get; set; }                     // 30 text
        public string NICNumber { get; set; }                   // 10 text
        public TcYearMonth From { get; set; }                      // 6 YYYYMM, 2008 July Month 200807 
        public TcYearMonth To { get; set; }                        // 6 YYYYMM, 2008 July Month 200807 
        public decimal TotalContribution { get; set; }          // 14 numeric, in cents

        public Dictionary<TeEtfError, string> Errors { get; set; }

        public TcEtfDetailRow()
        {
            Identification  = "D";
            EmployerNumber  = "";
            Initials        = "";
            Surname         = "";
            NICNumber       = "";

            Errors = new Dictionary<TeEtfError, string>();
        }

        public static TcEtfDetailRow GetEtfDetailRow(TcEtfDetailOriginData origin, TcEtfDetailDestinationData destination)
        {
            TcEtfDetailRow row = new TcEtfDetailRow();

            row.EmployerNumber      = origin.EmployerNumber;
            row.MemberNumber        = destination.MemberNumber;
            row.Initials            = destination.Initials;
            row.Surname             = destination.Surname;
            row.NICNumber           = destination.NICNumber;
            row.From                = origin.From;
            row.To                  = origin.To;
            row.TotalContribution   = destination.TotalContribution;

            return row;
        }

        public bool IsValid()
        {
            bool valid = true;
            Errors.Clear();

            if (!Regex.IsMatch(EmployerNumber, "^[a-zA-z][a-zA-Z ][0-9]{6}"))
            {
                valid = false;
                Errors.Add(TeEtfError.Invalid_Employer_Number, string.Format("Employee number[{0}] is invalid", EmployerNumber));
            }

            if (!TcValidator.IsValidEmployeeOrEmployerNumberAfterClean(MemberNumber))
            {
                valid = false;
                Errors.Add(TeEtfError.Invalid_Member_Number, string.Format("Member Number[{0}] is not valid", MemberNumber));
            }

            if (string.IsNullOrEmpty(Surname))
            {
                valid = false;
                Errors.Add(TeEtfError.Empty_Member_Name, string.Format("Member name is not set"));
            }

            if (string.IsNullOrEmpty(NICNumber))
            {
                valid = false;
                Errors.Add(TeEtfError.Empty_NIC_Number, "Empty NIC Number");
            }
            else
            {
                if (!TcValidator.IsValidNIC(NICNumber))
                {
                    valid = false;
                    Errors.Add(TeEtfError.Invalid_NIC_Number, string.Format("Invalid NIC Number [{0}]", NICNumber));
                }
            }

            decimal maximum = new decimal(9999999.99);
            if (!TcValidator.IsValueInRange<decimal>(TotalContribution, 0, maximum))
            {
                valid = false;
                Errors.Add(TeEtfError.Invalid_Total_Contribution_Value,
                    string.Format("{0} value [{1}] is invalid. Value must be in between [{2}] and [{3}]",
                    "Total contribution", TotalContribution.ToString("N2"), 0.ToString("N2"), maximum.ToString("N2")));
            }

            return valid;
        }

        public string GetPayMasterRow()
        {
            string row = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                Identification,
                EmployerNumber,
                TcString.AppendZerosToFront(MemberNumber.ToString(), 6),
                TcString.AppendSpacesToEnd(Initials, 20),
                TcString.AppendSpacesToEnd(Surname, 30),
                TcString.AppendSpacesToFront(NICNumber, 10),
                From.ToDate().ToString("yyyyMM"),
                To.ToDate().ToString("yyyyMM"),
                TcString.AppendZerosToFront(TotalContribution.ToString("N2").Replace(",", "").Replace(".", ""), 9));

            return row;
        }

        public TcCsvDataRow GetCsvRow()
        {
            TcCsvDataRow row = new TcCsvDataRow();

            
            row.Fields.Add(new TcCsvDataField(Identification));
            row.Fields.Add(new TcCsvDataField(EmployerNumber));
            row.Fields.Add(new TcCsvDataField(MemberNumber));
            row.Fields.Add(new TcCsvDataField(Initials));
            row.Fields.Add(new TcCsvDataField(Surname));
            row.Fields.Add(new TcCsvDataField(NICNumber));
            row.Fields.Add(new TcCsvDataField(From.ToString()));
            row.Fields.Add(new TcCsvDataField(To.ToString()));
            row.Fields.Add(new TcCsvDataField(TotalContribution.ToString("N2")));

            return row;
        }

        public static TcCsvDataRow GetCsvHeaderRow()
        {
            TcCsvDataRow row = new TcCsvDataRow();

            row.Fields.Add(new TcCsvDataField("Identification"));
            row.Fields.Add(new TcCsvDataField("EmployerNumber"));
            row.Fields.Add(new TcCsvDataField("MemberNumber"));
            row.Fields.Add(new TcCsvDataField("Initials"));
            row.Fields.Add(new TcCsvDataField("Surname"));
            row.Fields.Add(new TcCsvDataField("NICNumber"));
            row.Fields.Add(new TcCsvDataField("From"));
            row.Fields.Add(new TcCsvDataField("To"));
            row.Fields.Add(new TcCsvDataField("TotalContribution"));

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
