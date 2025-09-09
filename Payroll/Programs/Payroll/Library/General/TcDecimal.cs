using System;

// Harshan Nishantha
// 2015-11-18

namespace Payroll.Library.General
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

        public static bool EqualFor2DecimalPoints(decimal actual, decimal expected)
        {
            decimal roundedActual = decimal.Round(actual, 2);
            decimal roundedExpected = decimal.Round(expected, 2);

            bool equal = false;
            if (roundedActual == roundedExpected)
            {
                equal = true;
            }

            return equal;
        }

        public static string MoneyWithoutDecimalPoint(decimal amount, int length)
        {
            string result = string.Empty;

            decimal positiveAmount = amount > 0 ? amount : 0;
            positiveAmount = Math.Round(positiveAmount, 2);
            result  = TcString.AppendZerosToFront(positiveAmount.ToString("N2").Replace(",", "").Replace(".", ""), length);

            return result;
        }
    }
}
