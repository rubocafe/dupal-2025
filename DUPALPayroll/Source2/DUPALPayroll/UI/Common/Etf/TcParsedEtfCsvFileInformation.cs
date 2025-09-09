using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.Etf;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2014-01-02

namespace DUPALPayroll.UI.Etf
{
    public class TcParsedEtfCsvFileInformation : TcParsedCsvFileInformation
    {
        public TcEtfCsvFileReader Reader { get; set; }
        public TcEtfFileValidator Validator { get; set; }

        public TcParsedEtfCsvFileInformation(string identifier, string filePath) : 
            base(identifier, filePath)
        {
        }

        public void ReadEtfFile(TcEtfDetailOriginData originData)
        {
            Total                   = 0;
            ValidRowsTotal          = 0;
            InvalidRowsTotal        = 0;
            ParsedRowsCount         = 0;
            ValidRowsCount          = 0;
            InvalidRowsCount        = 0;

            if (Exists)
            {
                Reader = new TcEtfCsvFileReader(FilePath);
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

                Validator = new TcEtfFileValidator(Reader.File);
                Validator.Validate();

                ValidRowsCount      = Validator.ValidRows.Count;
                InvalidRowsCount    = Validator.InvalidRows.Count;
                ValidRowsTotal      = Validator.GetValidRowsTotal();
                InvalidRowsTotal    = Validator.GetInvalidRowsTotal();
            }
        }
    }
}
