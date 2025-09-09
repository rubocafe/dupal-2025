using System.Collections.Generic;
using System.IO;

// Harshan Nishantha
// 2013-12-23

namespace DUPALPayroll.UI.Common.Epf
{
    public class TcEpfFileWriter
    {
        public TcEpfFile File { get; set; }
        public string FilePath { get; private set; }
        public List<TcEpfRow> ErrorRows { get; set; }

        public TcEpfFileWriter(TcEpfFile file, string filePath)
        {
            File        = file;
            FilePath    = filePath;
            ErrorRows   = new List<TcEpfRow>();
        }

        public bool Write()
        {
            ErrorRows.Clear();

            using (StreamWriter writer = new StreamWriter(FilePath))
            {
                foreach (TcEpfRow data in File.Rows)
                {
                    if (data.IsValid())
                    {
                        string line = data.GetPayMasterRow();
                        writer.WriteLine(line);
                    }
                    else
                    {
                        ErrorRows.Add(data);
                    }
                }
            }

            return ErrorRows.Count > 0 ? false : true;
        }
    }
}
