using DUPALPayroll.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-09-09

namespace DUPALPayroll.Validators
{
    public class TcBankAndBranchValidator
    {
        public static bool IsValidBank(string bank)
        {
            bool valid = !string.IsNullOrEmpty(bank);

            return valid;
        }

        public static bool IsValidBranch(string branch)
        {
            bool valid = !string.IsNullOrEmpty(branch);

            return valid;
        }

        public static bool IsValidBankCode(string bankCode)
        {
            if (string.IsNullOrEmpty(bankCode))
            {
                return false;
            }

            if (bankCode.Length > 4 ||
                (!TcString.IsNumeric(bankCode)) ||
                TcString.ContainOnlyZeros(bankCode))
            {
                return false;
            }

            return true;
        }

        public static bool IsValidBranchCode(string branchCode)
        {
            if (string.IsNullOrEmpty(branchCode))
            {
                return false;
            }

            if (branchCode.Length > 3 ||
                (!TcString.IsNumeric(branchCode)) ||
                TcString.ContainOnlyZeros(branchCode))
            {
                return false;
            }

            return true;
        }

        public static bool IsPaymasterSupportedBank(string bank)
        {
            string bankAcronym = bank == null ? "" : bank.ToUpper().Trim();

            switch (bankAcronym)
            {
                case "RDB":
                case "KDB":
                    return false;
            }

            return true;
        }
    }
}
