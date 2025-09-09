using System.Text.RegularExpressions;

// Harshan Nishantha
// 2013-08-28

namespace DUPALPayroll.Library
{
    public class TcString
    {
        public static string AppendZerosToFront(string text, int length)
        {
            string fixedText = text == null ? "" : text;

            if (fixedText.Length > length)
            {
                fixedText = fixedText.Substring(0, length);
            }
            else
            {
                int count = length - fixedText.Length;
                for (int i = 0; i < count; i++)
                {
                    fixedText = ("0" + fixedText);
                }
            }

            return fixedText;
        }

        public static string AppendSpacesToEnd(string text, int length)
        {
            string fixedText = text == null ? "" : text;

            if (fixedText.Length > length)
            {
                fixedText = fixedText.Substring(0, length);
            }
            else
            {
                int count = length - fixedText.Length;
                for (int i = 0; i < count; i++)
                {
                    fixedText += " ";
                }
            }

            return fixedText;
        }

        public static string AppendSpacesToFront(string text, int length)
        {
            string fixedText = text == null ? "" : text;

            if (fixedText.Length > length)
            {
                fixedText = fixedText.Substring(0, length);
            }
            else
            {
                int count = length - fixedText.Length;
                for (int i = 0; i < count; i++)
                {
                    fixedText = " " + fixedText;
                }
            }

            return fixedText;
        }

        public static string CamelCaseToSpacesSeparatedText(string text)
        {
            string result = text;

            if (!string.IsNullOrEmpty(text))
            {
                result = Regex.Replace(text, "([A-Z])", " $1").Trim();
            }

            return result;
        }

        public static bool IsNumeric(string text)
        {
            if (Regex.IsMatch(text, "^[0-9]+$", RegexOptions.Compiled))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool ContainOnlyZeros(string text)
        {
            if (Regex.IsMatch(text, "^[0]+$", RegexOptions.Compiled))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidFormttedCurrencyString(string value, int decimalPlaces)
        {
            if (value.Length > decimalPlaces)
            {
                int decimalPlacesInValue = value.Length - value.LastIndexOf(".") - 1;
                if (decimalPlacesInValue == decimalPlaces)
                {
                    decimal temp = 0;
                    if (decimal.TryParse(value, out temp))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string TrimAndUpper(string text)
        {
            string result = text;

            if (!string.IsNullOrEmpty(text))
            {
                result = text.Trim().ToUpper();
            }

            return result;
        }
    }
}
