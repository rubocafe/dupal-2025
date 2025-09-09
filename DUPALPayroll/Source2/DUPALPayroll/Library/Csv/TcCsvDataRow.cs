using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-22

namespace DUPALPayroll.Library.Csv
{
    public class TcCsvDataRow
    {
        public int LineNumber { get; set; }
        public string RawData { get; set; }
        public List<TcCsvDataField> Fields { get; set; }

        private int index = 0;

        public TcCsvDataRow()
        {
            LineNumber = 0;
            Fields = new List<TcCsvDataField>();
        }

        public void AddField(TcCsvDataField field)
        {
            field.Index = ++index;
            Fields.Add(field);
        }
    }
}
