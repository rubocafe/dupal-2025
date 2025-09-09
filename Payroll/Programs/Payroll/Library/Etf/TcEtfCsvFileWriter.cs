using Payroll.Library.Csv;
using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2013-12-31

namespace Payroll.Library.Etf
{
    public class TcEtfCsvFileWriter
    {
        public TcEtfFile File { get; set; }
        public string FilePath { get; private set; }

        public TcEtfCsvFileWriter(TcEtfFile file, string filePath)
        {
            File                = file;
            FilePath            = filePath;
        }

        public bool Write()
        {
            TcCsvFile csvFile = new TcCsvFile();
            TcCsvDataRow row = TcEtfDetailRow.GetCsvHeaderRow();
            csvFile.Rows.Add(row);

            foreach (TcEtfDetailRow data in File.Rows)
            {
                row = data.GetCsvRow();
                csvFile.Rows.Add(row);
            }

            if (File.HeaderRow != null && File.HeaderRow.IsValid())
            {

                row = File.HeaderRow.GetCsvRow();
                csvFile.Rows.Add(row);
            }

            csvFile.Save(FilePath);

            return true;
        }
    }
}
