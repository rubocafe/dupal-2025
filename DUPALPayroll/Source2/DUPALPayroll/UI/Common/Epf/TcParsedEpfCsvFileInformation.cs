using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2014-01-02

namespace DUPALPayroll.UI.Common.Epf
{
    public class TcParsedEpfCsvFileInformation : TcParsedCsvFileInformation
    {
        public TcEpfCsvFileReader Reader { get; set; }
        public TcEpfFileValidator Validator { get; set; }

        public TcParsedEpfCsvFileInformation(string identifier, string filePath) : 
            base(identifier, filePath)
        {
        }

        public void ReadEpfFile(TcEpfOriginData originData)
        {
            Total                   = 0;
            ValidRowsTotal          = 0;
            InvalidRowsTotal        = 0;
            ParsedRowsCount         = 0;
            ValidRowsCount          = 0;
            InvalidRowsCount        = 0;

            if (Exists)
            {
                Reader = new TcEpfCsvFileReader(FilePath);
                Reader.Read();

                Reader.ReplaceOriginData(originData);

                Total                   = Reader.File.GetTotal();
                ParsedRowsCount         = Reader.File.Rows.Count;
                if (Reader.ErrorLines.Count > 0)
                {
                    foreach (KeyValuePair<int, string> pair in Reader.ErrorLines)
                    {
                        string error = string.Format("invalid csv file [{0}]\nFailed to parse the line [{1}]\n{2}",
                        FilePath, pair.Key, pair.Value);

                        throw new Exception(error);
                    }
                }

                Validator = new TcEpfFileValidator(Reader.File);
                Validator.Validate();

                ValidRowsCount      = Validator.ValidRows.Count;
                InvalidRowsCount    = Validator.InvalidRows.Count;
                ValidRowsTotal      = Validator.GetValidRowsTotal();
                InvalidRowsTotal    = Validator.GetInvalidRowsTotal();
            }
        }
    }
}
