using DUPALPayroll.Library;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.Validators
{
    public class TcNICNumberValidator
    {
        public static bool IsValid(string nicNumber)
        {
            if (!string.IsNullOrEmpty(nicNumber))
            {
                if (nicNumber.Length == 10)
                {
                    string numberPart = nicNumber.Substring(0, 9);
                    if (TcString.IsNumeric(numberPart))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
