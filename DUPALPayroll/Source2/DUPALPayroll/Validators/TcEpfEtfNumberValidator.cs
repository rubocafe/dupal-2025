using DUPALPayroll.General;

// Harshan Nishantha
// 2014-01-02

namespace DUPALPayroll.Validators
{
    public class TcEpfEtfNumberValidator
    {
        public static bool IsValidAfterClean(string number)
        {
            return IsValid(TcFormatter.TrimAndUpper(number));
        }

        public static bool IsValid(string number)
        {
            if (!string.IsNullOrEmpty(number))
            {
                int parsedNumber = 0;
                if (int.TryParse(number, out parsedNumber))
                {
                    if (parsedNumber >= 0 && parsedNumber <= 999999)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
