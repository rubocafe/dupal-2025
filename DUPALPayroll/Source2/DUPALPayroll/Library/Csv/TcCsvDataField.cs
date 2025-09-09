
// Harshan Nishantha
// 2013-08-22

namespace DUPALPayroll.Library.Csv
{
    public class TcCsvDataField
    {
        public int Index { get; set; }
        public string Value { get; set; }
        public bool IsNumber { get; set; }

        public TcCsvDataField()
        {
            Index   = 0;
            Value   = string.Empty;
            IsNumber = false;
        }

        public TcCsvDataField(string text)
        {
            IsNumber    = false;
            Value       = text;
        }

        public TcCsvDataField(int number)
        {
            AddNumber(number.ToString());
        }

        public TcCsvDataField(long number)
        {
            AddNumber(number.ToString());
        }

        public TcCsvDataField(float number)
        {
            AddNumber(number.ToString());
        }

        public TcCsvDataField(double number)
        {
            AddNumber(number.ToString());
        }

        public TcCsvDataField(decimal number)
        {
            AddNumber(number.ToString());
        }

        private void AddNumber(string number)
        {
            IsNumber = true;
            Value = number;
        }

        public void SetValue(int index, string value)
        {
            Index   = index;
            Value   = value;
        }
    }
}
