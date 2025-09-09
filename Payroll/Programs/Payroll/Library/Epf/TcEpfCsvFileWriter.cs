using Payroll.Library.Csv;
using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2013-12-31

namespace Payroll.Library.Epf
{
    public class TcEpfCsvFileWriter
    {
        public TcEpfFile File { get; set; }
        public string FilePath { get; private set; }

        public TcEpfCsvFileWriter(TcEpfFile file, string filePath)
        {
            File        = file;
            FilePath    = filePath;
        }

        public bool Write()
        {
            TcCsvFile csvFile = new TcCsvFile();
            TcCsvDataRow row = TcEpfRow.GetCsvHeaderRow();
            csvFile.Rows.Add(row);

            foreach (TcEpfRow data in File.Rows)
            {
                row = data.GetCsvRow();
                csvFile.Rows.Add(row);
            }

            csvFile.Save(FilePath);

            return true;
        }
    }
}
