using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using System;
using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2014-01-02

namespace DUPALPayroll.UI.Common.Etf
{
    public class TcEtfCsvFileReader
    {
        public Dictionary<int, string>  ErrorLines { get; set; }
        public TcEtfFile                File { get; set; }
        public string                   FilePath { get; private set; }
        public TcCsvFile                CsvFile { get; set; }

        public TcEtfCsvFileReader(string filePath)
        {
            FilePath    = filePath;
            File        = new TcEtfFile();
            CsvFile     = new TcCsvFile();
            ErrorLines  = new Dictionary<int, string>();
        }

        public bool Read()
        {
            ErrorLines.Clear();

            CsvFile = new TcCsvFile();
            CsvFile.Load(FilePath);

            File = new TcEtfFile();

            foreach (TcCsvDataRow row in CsvFile.Rows)
            {
                try
                {
                    if (row.LineNumber == 1)
                    {
                        continue; // Skip Header Row
                    }

                    TcEtfDetailRow data = GetEtfRow(row);
                    File.Rows.Add(data);
                }
                catch (Exception ex)
                {
                    ErrorLines.Add(row.LineNumber, string.Format("{0}\n{1}", row.RawData, ex.Message));
                }
            }

            return ErrorLines.Count > 0 ? false : true;
        }

        public void ReplaceOriginData(TcEtfDetailOriginData origin)
        {
            foreach (TcEtfDetailRow row in File.Rows)
            {
                row.EmployerNumber  = origin.EmployerNumber;
                row.From            = origin.From;
                row.To              = origin.To;
            }
        }

        private TcEtfDetailRow GetEtfRow(TcCsvDataRow row)
        {
            TcEtfDetailRow data = new TcEtfDetailRow();

            data.LineNumber = row.LineNumber;

            //data.Identification   = row.Fields[0].Value;                                  // 1 text, Default "D"
            data.EmployerNumber     = row.Fields[1].Value;                                  // 8 text AANNNNNN
            data.MemberNumber       = row.Fields[2].Value;                                  // 6 numeric
            data.Initials           = row.Fields[3].Value;                                  // 20 text
            data.Surname            = row.Fields[4].Value;                                  // 30 text
            data.NICNumber          = row.Fields[5].Value;                                  // 10 text
            data.From               = TcYearMonth.LoadSafelyFromText(row.Fields[6].Value);  // 6 YYYYMM, 2008 July Month 200807 
            data.To                 = TcYearMonth.LoadSafelyFromText(row.Fields[7].Value);  // 6 YYYYMM, 2008 July Month 200807 
            data.TotalContribution  = TcDecimal.GetDecimalFromText(row.Fields[8].Value);    // 14 numeric, in cents

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
