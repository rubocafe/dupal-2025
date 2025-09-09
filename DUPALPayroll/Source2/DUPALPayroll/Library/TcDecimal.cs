using System;

// Harshan Nishantha
// 2013-12-24

namespace DUPALPayroll.Library
{
    public class TcDecimal
    {
        public static decimal GetDecimalFromText(string text)
        {
            decimal value = GetDecimalFromText(text, 0);

            return value;
        }

        public static decimal GetDecimalFromText(string text, int decimalPoints)
        {
            string formattedText = text.Trim();

            if (text.Length < decimalPoints)
            {
                string ex = string.Format("Length of the text [{0}] is shorter than the expected decimal points [{1}]", text, decimalPoints);
                throw new Exception(ex);
            }

            if (decimalPoints > 0)
            {
                formattedText = text.Insert(text.Length - decimalPoints, ".");
            }
            
            decimal value = decimal.Parse(formattedText);

            return value;
        }
    }
}
