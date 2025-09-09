using Payroll.Library;
using Payroll.Library.General;
using System.Text.RegularExpressions;

// Harshan Nishantha
// 2013-08-27

namespace Payroll.Validators
{
    public class TcBankAccountNumberValidator
    {
        public static bool IsValid(string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                return false;
            }

            if (accountNumber.Length > 12 ||
                (!TcString.IsNumeric(accountNumber)))
            {
                return false;
            }

            if (TcString.ContainOnlyZeros(accountNumber))
            {
                return false;
            }

            return true;
        }
    }
}
