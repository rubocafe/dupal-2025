using Payroll.Library;
using Payroll.Library.Csv;
using Payroll.Library.Date;
using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Harshan Nishantha
// 2013-12-23

namespace Payroll.Library.Etf
{
    public class TcEtfHeaderRow
    {
        public int LineNumber { get; set; } // Needed in read from file senario

        /*
         * All numeric fields should be right aligned and filled with leading zeros.
         * */

        public string Identification { get; private set; }      // 1 text, Default "H"
        public string EmployerNumber { get; set; }              // 8 text AANNNNNN
        public TcYearMonth From { get; set; }                   // 6 YYYYMM, 2008 July Month 200807 
        public TcYearMonth To { get; set; }                     // 6 YYYYMM, 2008 July Month 200807 
        public int TotalMembers { get; set; }                   // 6 numeric
        public decimal TotalContribution { get; set; }          // 14 numeric, in cents
        public int NumberOfLinesPerPage { get; set; }           // 2 numeric, Number of lines per page in page2 hard copy. Default 24

        public Dictionary<TeEtfError, string> Errors { get; set; }

        public TcEtfHeaderRow()
        {
            Identification = "H";
            EmployerNumber = "";
            NumberOfLinesPerPage = 24;

            Errors = new Dictionary<TeEtfError, string>();
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

            decimal maximum = new decimal(9999999.99);
            if (TotalContribution > maximum)
            {
                valid = false;
                Errors.Add(TeEtfError.Invalid_Total_Contribution_Value, string.Format("Total contribution value[{0}] is too large", TotalContribution.ToString("N2")));
            }

            return valid;
        }

        public string GetPayMasterRow()
        {
            string row = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                Identification,
                EmployerNumber,
                From.ToDate().ToString("yyyyMM"),
                To.ToDate().ToString("yyyyMM"),
                TcString.AppendZerosToFront(TotalMembers.ToString(), 6),
                TcString.AppendZerosToFront(TotalContribution.ToString("N2").Replace(",", "").Replace(".", ""), 14),
                TcString.AppendZerosToFront(NumberOfLinesPerPage.ToString(), 2));

            return row;
        }

        public TcCsvDataRow GetCsvRow()
        {
            TcCsvDataRow row = new TcCsvDataRow();

            row.Fields.Add(new TcCsvDataField(Identification));
            row.Fields.Add(new TcCsvDataField(EmployerNumber));
            row.Fields.Add(new TcCsvDataField(From.ToString()));
            row.Fields.Add(new TcCsvDataField(To.ToString()));
            row.Fields.Add(new TcCsvDataField(TotalMembers));
            row.Fields.Add(new TcCsvDataField(TotalContribution));
            row.Fields.Add(new TcCsvDataField(NumberOfLinesPerPage));

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
