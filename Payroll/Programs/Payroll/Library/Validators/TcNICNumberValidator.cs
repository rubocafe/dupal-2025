using Payroll.Library;
using Payroll.Library.General;
using System.Text.RegularExpressions;

// Harshan Nishantha
// 2013-08-27

namespace Payroll.Validators
{
    public class TcNICNumberValidator
    {
        public static bool IsValid(string nicNumber)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(nicNumber))
            {
                if (nicNumber.Length == 10)         // Legacy format
                {
                    Match match = Regex.Match(nicNumber, @"^\d{9}(V|X)$");
                    isValid = match.Success;
                }
                else if (nicNumber.Length == 12)    // New format
                {
                    Match match = Regex.Match(nicNumber, @"^\d{12}$");
                    isValid = match.Success;
                }
            }

            return isValid;
        }
    }
}
