using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using System;
using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2014-01-01

namespace DUPALPayroll.UI.Common.Epf
{
    public class TcEpfCsvFileReader
    {
        public Dictionary<int, string> ErrorLines { get; set; }
        public TcEpfFile File { get; set; }
        public string FilePath { get; private set; }
        public TcCsvFile CsvFile { get; set; }

        public TcEpfCsvFileReader(string filePath)
        {
            FilePath    = filePath;
            File        = new TcEpfFile();
            CsvFile     = new TcCsvFile();
            ErrorLines  = new Dictionary<int, string>();
        }

        public bool Read()
        {
            ErrorLines.Clear();

            CsvFile = new TcCsvFile();
            CsvFile.Load(FilePath);

            File = new TcEpfFile();

            foreach (TcCsvDataRow row in CsvFile.Rows)
            {
                try
                {
                    if (row.LineNumber == 1)
                    {
                        continue; // Skip Header Row
                    }

                    TcEpfRow data = GetEpfRow(row);
                    File.Rows.Add(data);
                }
                catch (Exception ex)
                {
                    ErrorLines.Add(row.LineNumber, string.Format("{0}\n{1}", row.RawData, ex.Message));
                }
            }

            return ErrorLines.Count > 0 ? false : true;
        }

        public void ReplaceOriginData(TcEpfOriginData origin)
        {
            foreach (TcEpfRow row in File.Rows)
            {
                row.ContributionPeriod  = origin.ContributionPeriod;
                row.ZoneCode            = origin.ZoneCode;
                row.EmployerNumber      = origin.EmployerNumber;
                row.SubmitionId         = origin.SubmitionId;
            }
        }

        private TcEpfRow GetEpfRow(TcCsvDataRow row)
        {
            TcEpfRow data = new TcEpfRow();

            data.LineNumber = row.LineNumber;

            data.NICNumber              = row.Fields[0].Value;                                  // 20 Text
            data.LastName               = row.Fields[1].Value;                                  // 40 Text
            data.Initials               = row.Fields[2].Value;                                  // 20 Text
            data.MemberNumber           = row.Fields[3].Value;                                  // 6 numeric
            data.TotalContribution      = TcDecimal.GetDecimalFromText(row.Fields[4].Value);    // 9.2
            data.EmployersContribution  = TcDecimal.GetDecimalFromText(row.Fields[5].Value);    // 9.2 = There should be maximum of 10 digits including 7 integers, decimal point & 2 decimals. E.g. 0001535.73
            data.MembersContribution    = TcDecimal.GetDecimalFromText(row.Fields[6].Value);    // 9.2
            data.TotalEarnings          = TcDecimal.GetDecimalFromText(row.Fields[7].Value);    // 11.2 = There should be maximum of  12 digits including 9 integers, decimal point  & 2 decimals.  E.g. 000014758.55
            data.MemberStatus           = row.Fields[8].Value;                                  // 1 Text (E=Extg, N=New, V=Vacated)
            data.ZoneCode               = row.Fields[9].Value;                                  // 1 Text "A"
            data.EmployerNumber         = row.Fields[10].Value;                                 // 6 numeric
            data.ContributionPeriod     = TcYearMonth.LoadSafelyFromText(row.Fields[11].Value); // 6 numeric Contribution YYYYMM
            data.SubmitionId            = int.Parse(row.Fields[12].Value);                      // 2 numeric, 01 = all staff in one file / 02 = Staff in separate categories as "Executive", "non-Executive"
            data.DaysOfWork             = TcDecimal.GetDecimalFromText(row.Fields[13].Value);   // 4.2 Numeric
            data.OccupationClassificationGrade = row.Fields[14].Value;                          // 3 Numeric

            return data;
        }

        public string GetHeaderText()
        {
            string text = string.Format("Total Rows: {0}, Valid: {1}, Invalid {2}", File.Rows.Count + ErrorLines.Count, File.Rows.Count, ErrorLines.Count);

            return text;
        }

        public string GetLog()
        {
            string log = GetHeaderText();

            if (ErrorLines.Count > 0)
            {
                log += "\n\n";
                foreach (KeyValuePair<int, string> pair in ErrorLines)
                {
                    log += string.Format("Line {0}: {1}\n", pair.Key, pair.Value);
                    log += "\n";
                }
            }

            return log;
        }
    }
}
