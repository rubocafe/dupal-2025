using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using System;
using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2013-12-23

namespace DUPALPayroll.UI.Common.Epf
{
    public class TcEpfFileReader
    {
        public Dictionary<int, string> ErrorLines { get; set; }
        public TcEpfFile File { get; set; }
        public string FilePath { get; private set; }

        private TcEpfFileReader(string filePath)
        {
            FilePath    = filePath;
            File        = new TcEpfFile();
            ErrorLines  = new Dictionary<int, string>();
        }

        public bool Read()
        {
            ErrorLines.Clear();

            using (StreamReader reader = new StreamReader(FilePath))
            {
                string line;
                int lineNumber = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        TcEpfRow data = GetEpfRow(lineNumber, line);
                        File.Rows.Add(data);
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

        private TcEpfRow GetEpfRow(int lineNumber, string line)
        {
            TcEpfRow data = new TcEpfRow();

            data.LineNumber = lineNumber;

            data.NICNumber                      = line.Substring(0, 20);                                // 20 Text
            data.LastName                       = line.Substring(20, 40);                               // 40 Text
            data.Initials                       = line.Substring(60, 20);                               // 20 Text
            data.MemberNumber                   = line.Substring(80, 6);                                // 6 numeric
            data.TotalContribution              = TcDecimal.GetDecimalFromText(line.Substring(86, 10)); // 9.2
            data.EmployersContribution          = TcDecimal.GetDecimalFromText(line.Substring(96, 10)); // 9.2 = There should be maximum of 10 digits including 7 integers, decimal point & 2 decimals. E.g. 0001535.73
            data.MembersContribution            = TcDecimal.GetDecimalFromText(line.Substring(106, 10));// 9.2
            data.TotalEarnings                  = TcDecimal.GetDecimalFromText(line.Substring(116, 12));// 11.2 = There should be maximum of  12 digits including 9 integers, decimal point  & 2 decimals.  E.g. 000014758.55
            data.MemberStatus                   = line.Substring(128, 1);                               // 1 Text (E=Extg, N=New, V=Vacated)
            data.ZoneCode                       = line.Substring(129, 1);                               // 1 Text "A"
            data.EmployerNumber                 = line.Substring(130, 6);                               // 6 numeric
            data.ContributionPeriod             = DateTimeForYearMonthString(line.Substring(136, 6));   // 6 numeric Contribution YYYYMM
            data.SubmitionId                    = int.Parse(line.Substring(142, 2));                    // 2 numeric, 01 = all staff in one file / 02 = Staff in separate categories as "Executive", "non-Executive"
            data.DaysOfWork                     = TcDecimal.GetDecimalFromText(line.Substring(144, 4)); // 4.2 Numeric
            data.OccupationClassificationGrade  = line.Substring(148, 3);                    // 3 Numeric

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
