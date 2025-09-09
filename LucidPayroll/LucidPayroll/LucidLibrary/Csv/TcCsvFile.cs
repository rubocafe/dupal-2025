using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// Harshan Nishantha
// 2013-08-22

namespace LucidLibrary.Csv
{
    public class TcCsvFile
    {
        private const char DOUBLE_QUOTE = '"';

        public char Separator { get; set; }
        public List<TcCsvDataRow> Rows { get; set; }

        public TcCsvFile() : this(',')
        {
        }

        public TcCsvFile(char separator)
        {
            Separator = separator;
            Rows = new List<TcCsvDataRow>();
        }

        public void Load(string csvFilePath)
        {
            int lineNumber = 1;
            string rawData = string.Empty;

            using (StreamReader reader = new StreamReader(
                new FileStream(csvFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), 
                Encoding.Default))
            {
                while ((rawData = reader.ReadLine()) != null)
                {
                    while (ShouldReadNextLine(rawData))
                    {
                        string newLine = reader.ReadLine();
                        if (newLine == null)
                        {
                            break;
                        }
                        else
                        {
                            rawData += newLine;
                            lineNumber++;
                        }
                    }

                    TcCsvDataRow row = ProcessRawData(lineNumber, rawData);
                    Rows.Add(row);

                    lineNumber++;
                }
            }
        }

        public void Save(string target)
        {
            string contents = string.Empty;

            using (StreamWriter writer = new StreamWriter(target))
            {
                foreach (TcCsvDataRow row in Rows)
                {
                    string rowData = string.Empty;

                    foreach (TcCsvDataField feild in row.Fields)
                    {
                        if (feild.IsNumber)
                        {
                            if (rowData == string.Empty)
                            {
                                rowData = string.Format("{0}", feild.Value);
                            }
                            else
                            {
                                rowData = string.Format("{0},{1}", rowData, feild.Value);
                            }
                        }
                        else
                        {
                            if (rowData == string.Empty)
                            {
                                rowData = string.Format("\"{0}\"", feild.Value);
                            }
                            else
                            {
                                rowData = string.Format("{0},\"{1}\"", rowData, feild.Value);
                            }
                        }
                    }

                    rowData += "\n";
                    writer.Write(rowData);
                }
            }
        }

        private bool ShouldReadNextLine(string rawData)
        {
            int doubleQuoteCount = rawData.Count(c => c == DOUBLE_QUOTE);

            if ((doubleQuoteCount % 2) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public TcCsvDataRow ProcessRawData(int lineNumber, string rawData)
        {
            TcCsvDataRow row    = new TcCsvDataRow();
            row.LineNumber      = lineNumber;
            row.RawData         = rawData;
            string token        = String.Empty;

            rawData = ReplaceNbspWithSp(rawData);

            bool inQuotes = false;
            bool lineFeed = false;
            int index = 0;
            for (int i = 0; i < rawData.Length; i++)
            {
                char c = rawData[i];

                if (c == DOUBLE_QUOTE)
                {
                    if (inQuotes)
                    {
                        inQuotes = false;
                    }
                    else
                    {
                        inQuotes = true;
                    }

                    continue;
                }
                else if (c == Separator)
                {
                    if (inQuotes)
                    {
                        token += c;
                    }
                    else
                    {
                        AddFeildToRow(row, index, token);

                        index++;
                        inQuotes = false;
                        token = string.Empty;
                    }
                }
                else if (c == '\r' || c == '\n')
                {
                    if ((i == (rawData.Length - 2) && c == '\r') ||
                        (i == (rawData.Length - 1) && c == '\n')) // The last carriage return || new line 
                    {
                        lineFeed = true;
                        AddFeildToRow(row, index, token);
                        index++;
                        break;
                    }
                    else // new line exists in raw data
                    {
                        token += c;
                    }
                }
                else
                {
                    token += c;
                }
            }

            if (!lineFeed)
            {
                AddFeildToRow(row, index, token);
                index++;
            }

            return row;
        }

        private string ReplaceNbspWithSp(string rawData)
        {
            string result = rawData;

            if (!string.IsNullOrEmpty(rawData))
            {
                result = rawData.Replace((char)160, ' ');
            }

            return result;
        }

        private void AddFeildToRow(TcCsvDataRow row, int index, string token)
        {
            TcCsvDataField feild = new TcCsvDataField();
            feild.SetValue(index, token);

            row.AddField(feild);
        }
    }
}
