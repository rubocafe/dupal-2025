using Payroll.Library;
using Payroll.Library.Date;
using Payroll.Library.General;
using System;
using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2013-12-23

namespace Payroll.Library.Etf
{
    public class TcEtfFileReader
    {
        public Dictionary<int, string> ErrorLines { get; set; }
        public TcEtfFile File { get; set; }
        public string FilePath { get; private set; }

        private TcEtfFileReader(string filePath)
        {
            FilePath    = filePath;
            File        = new TcEtfFile();
            ErrorLines  = new Dictionary<int, string>();
        }

        public bool Read()
        {
            File = new TcEtfFile();
            ErrorLines.Clear();

            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                int lineNumber = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        if (line.StartsWith("D"))
                        {
                            TcEtfDetailRow data = GetEtfDetailRow(lineNumber, line);
                            File.Rows.Add(data);
                        }
                        else if (line.StartsWith("H"))
                        {
                            TcEtfHeaderRow data = GetEtfHeaderRow(lineNumber, line);
                            File.HeaderRow = data;
                        }
                    }
                    catch (Exception)
                    {
                        ErrorLines.Add(lineNumber, line);
                    }
                    
                    lineNumber++;
                }
            }

            return ErrorLines.Count > 0 ? false : true;
        }

        private TcEtfDetailRow GetEtfDetailRow(int lineNumber, string line)
        {
            TcEtfDetailRow data = new TcEtfDetailRow();

            data.LineNumber = lineNumber;

            //data.Identification   = line.Substring(0, 1);                                     // 1 text, Default "D"
            data.EmployerNumber     = line.Substring(1, 8);                                     // 8 text AANNNNNN
            data.MemberNumber       = line.Substring(9, 6);                          // 6 numeric
            data.Initials           = line.Substring(15, 20);                                   // 20 text
            data.Surname            = line.Substring(35, 30);                                   // 30 text
            data.NICNumber          = line.Substring(65, 10);                                   // 10 text
            data.From               = DateTimeForYearMonthString(line.Substring(75, 6));        // 6 YYYYMM, 2008 July Month 200807 
            data.To                 = DateTimeForYearMonthString(line.Substring(81, 6));        // 6 YYYYMM, 2008 July Month 200807 
            data.TotalContribution = TcDecimal.GetDecimalFromText(line.Substring(87, 14), 2);   // 14 numeric, in cents

            return data;
        }

        private TcEtfHeaderRow GetEtfHeaderRow(int lineNumber, string line)
        {
            TcEtfHeaderRow data = new TcEtfHeaderRow();

            //data.Identification       = line.Substring(0, 1);                                     // 1 text, Default "H"
            data.EmployerNumber         = line.Substring(1, 8);                                     // 8 text AANNNNNN
            data.From                   = DateTimeForYearMonthString(line.Substring(9, 6));         // 6 YYYYMM, 2008 July Month 200807 
            data.To                     = DateTimeForYearMonthString(line.Substring(15, 6));        // 6 YYYYMM, 2008 July Month 200807 
            data.TotalMembers           = int.Parse(line.Substring(21, 6));                         // 6 numeric
            data.TotalContribution      = TcDecimal.GetDecimalFromText(line.Substring(27, 14), 2);  // 14 numeric, in cents
            data.NumberOfLinesPerPage   = int.Parse(line.Substring(43, 2));                         // 2 numeric, Number of lines per page in page2 hard copy. Default 24

            return data;
        }

        private static TcYearMonth DateTimeForYearMonthString(string yyyymm)
        {
            DateTime datetime = new DateTime(int.Parse(yyyymm.Substring(0, 4)), int.Parse(yyyymm.Substring(4, 2)), 1);

            return TcYearMonth.OfDateTime(datetime);
        }

        public string GetErrorLinesAsString()
        {
            string errors = "";

            foreach (KeyValuePair<int, string> pair in ErrorLines)
            {
                if (!string.IsNullOrEmpty(errors))
                {
                    errors += "\n";
                }

                errors += string.Format("{0} : {1}", pair.Key, pair.Value);
            }

            return errors;
        }
    }
}
