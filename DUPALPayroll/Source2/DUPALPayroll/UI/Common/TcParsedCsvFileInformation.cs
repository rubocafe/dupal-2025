using DUPALPayroll.Properties;
using System;
using System.Drawing;
using System.IO;

// Harshan Nishantha
// 2014-01-01

namespace DUPALPayroll.UI.Common
{
    public class TcParsedCsvFileInformation
    {
        public string               Identifier { get; set; }
        public string               FilePath { get; set; }
        public bool                 Exists { get; set; }
        public string               FileName { get; set; }
        public Nullable<DateTime>   ModifiedDate { get; set; }
        public Nullable<DateTime>   CreatedDate { get; set; }
        public int                  ParsedRowsCount { get; set; }
        public int                  ValidRowsCount { get; set; }
        public int                  InvalidRowsCount { get; set; }
        public decimal              Total { get; set; }
        public decimal              ValidRowsTotal { get; set; }
        public decimal              InvalidRowsTotal { get; set; }

        public TcParsedCsvFileInformation(string identifier, string filePath)
        {
            Identifier  = identifier;
            FilePath    = filePath;

            Update();
        }

        public void Update()
        {
            FileName                = "";
            ModifiedDate            = null;
            CreatedDate             = null;
            ParsedRowsCount         = 0;
            ValidRowsCount          = 0;
            InvalidRowsCount        = 0;
            Exists = File.Exists(FilePath);
            if (Exists)
            {
                FileInfo info = new FileInfo(FilePath);
                FileName        = info.Name;
                CreatedDate     = info.CreationTime;
                ModifiedDate    = info.LastWriteTime;
            }
        }

        public Image Image
        {
            get
            {
                if (Exists)
                {
                    if (InvalidRowsCount > 0)
                    {
                        return Resources.Cross;
                    }
                    else
                    {
                        return Resources.Tick;
                    }
                }
                else
                {
                    return Resources.Cross;
                }
            }
        }
    }
}
