using DUPALPayroll.Validators;
using System;

// Harshan Nishantha
// 2014-01-10

namespace DUPALPayroll.General
{
    public class TcValidator
    {
        public static bool IsValidBankAccountNumber(string accountNumber)
        {
            bool valid = TcBankAccountNumberValidator.IsValid(accountNumber);

            return valid;
        }

        public static bool IsValidZoneCode(string zoneCode)
        {
            bool valid = TcZoneCodeValidator.IsValid(zoneCode);

            return valid;
        }

        public static bool IsValidBank(string bank)
        {
            bool valid = TcBankAndBranchValidator.IsValidBank(bank);

            return valid;
        }

        public static bool IsValidBranch(string branch)
        {
            bool valid = TcBankAndBranchValidator.IsValidBranch(branch);

            return valid;
        }

        public static bool IsValidBankCode(string bankCode)
        {
            bool valid = TcBankAndBranchValidator.IsValidBankCode(bankCode);

            return valid;
        }

        public static bool IsValidBranchCode(string branchCode)
        {
            bool valid = TcBankAndBranchValidator.IsValidBranchCode(branchCode);

            return valid;
        }

        public static bool IsPaymasterSupportedBank(string bank)
        {
            bool valid = TcBankAndBranchValidator.IsPaymasterSupportedBank(bank);

            return valid;
        }

        public static bool IsValidEmployeeOrEmployerNumber(string memberNumber)
        {
            bool valid = TcEpfEtfNumberValidator.IsValid(memberNumber);

            return valid;
        }

        public static bool IsValidEmployeeOrEmployerNumberAfterClean(string memberNumber)
        {
            bool valid = TcEpfEtfNumberValidator.IsValidAfterClean(memberNumber);

            return valid;
        }

        public static bool IsValidEpfMemberStatus(string status)
        {
            bool valid = TcEpfMemberStatusValidator.IsValid(status);

            return valid;
        }

        public static bool IsValidNIC(string nic)
        {
            bool valid = TcNICNumberValidator.IsValid(nic);

            return valid;
        }

        public static bool IsValueInRange<T>(T value, T minimum, T maximum) where T : IComparable<T>
        {
            if (value.CompareTo(minimum) >= 0 && value.CompareTo(maximum) <= 0)
            {
                return true;
            }

            return false;
        }

        public static bool IsValidOCGrade(string ocGrade)
        {
            bool valid = TcOCGradeValidator.IsValid(ocGrade);

            return valid;
        }
    }
}
