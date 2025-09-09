using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2013-12-23

namespace Payroll.Library.Etf
{
    public class TcEtfFileWriter
    {
        public TcEtfFile File { get; set; }
        public string FilePath { get; private set; }
        public List<TcEtfDetailRow> ErrorRows { get; set; }


        public TcEtfFileWriter(TcEtfFile file, string filePath)
        {
            File                = file;
            FilePath            = filePath;
            ErrorRows     = new List<TcEtfDetailRow>();
        }

        public bool Write()
        {
            ErrorRows.Clear();

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (TcEtfDetailRow data in File.Rows)
                {
                    if (data.IsValid())
                    {
                        string line = data.GetFormattedRow();
                        writer.WriteLine(line);
                    }
                    else
                    {
                        ErrorRows.Add(data);
                    }
                }

                if (File.HeaderRow != null && File.HeaderRow.IsValid())
                {
                    string line = File.HeaderRow.GetPayMasterRow();
                    writer.WriteLine(line);
                }
            }

            return AllRowsWritten();
        }

        public bool AllRowsWritten()
        {
            if (ErrorRows.Count == 0 && File.HeaderRow != null && File.HeaderRow.IsValid())
            {
                return true;
            }

            return false;
        }
    }
}
