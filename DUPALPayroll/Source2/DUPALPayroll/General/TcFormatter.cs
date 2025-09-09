using DUPALPayroll.Library;
using DUPALPayroll.Validators;

// Harshan Nishantha
// 2014-01-10

namespace DUPALPayroll.General
{
    public class TcFormatter
    {
        public static string GetFormattedBankAccountNumber(string accountNumber)
        {
            string formatted = "";

            if (!string.IsNullOrEmpty(accountNumber))
            {
                string cleaned = accountNumber.Replace("-", "").Replace(" ", "");
                
                // 2014-02-12 Bugfix
                //
                if (cleaned.Length > 12)
                {
                    cleaned = cleaned.Substring(cleaned.Length - 12, 12);
                }
                
                formatted = TcString.AppendZerosToFront(cleaned, 12);
            }

            return formatted;
        }

        public static string GetFormattedBranchCode(string branchCode)
        {
            string formatted = "";

            if (!string.IsNullOrEmpty(branchCode))
            {
                string cleaned = branchCode.Trim().ToUpper();
                formatted = TcString.AppendZerosToFront(cleaned, 3);
            }

            return formatted;
        }

        public static string GetFormattedBankCode(string bankCode)
        {
            string formatted = "";

            if (!string.IsNullOrEmpty(bankCode))
            {
                string cleaned = bankCode.Trim().ToUpper();
                formatted = TcString.AppendZerosToFront(cleaned, 4);
            }

            return formatted;
        }

        public static string GetFormattedZoneCode(string zoneCode)
        {
            string formatted = "";

            if (TcValidator.IsValidZoneCode(zoneCode))
            {
                formatted = zoneCode.ToUpper()[0].ToString();
            }

            return formatted;
        }

        public static string GetFormattedNIC(string nic)
        {
            string formatted = "";

            if (!string.IsNullOrEmpty(nic))
            {
                formatted = nic.Replace(" ", "").ToUpper();
            }

            return formatted;
        }

        public static string GetFormattedMemberStatus(string status)
        {
            string formatted = "";
            if (!string.IsNullOrEmpty(status))
            {
                string cleaned = status.Trim().ToUpper()[0].ToString();
                if (cleaned == "E" || cleaned == "N" || cleaned == "V")
                {
                    formatted = cleaned;
                }
            }

            return formatted;
        }

        public static string GetFormattedInitials(string initials)
        {
            string cleaned = "";

            if (!string.IsNullOrEmpty(initials))
            {
                cleaned = initials.Replace(".", "").Replace(",", "").Replace(" ", "").ToUpper();
                string tempInitials = "";
                for (int i = 0; i < cleaned.Length; i++)
                {
                    tempInitials += (cleaned[i] + " ");
                }

                cleaned = tempInitials.Trim();
            }

            return cleaned;
        }

        public static string GetFormattedLastName(string lastName)
        {
            string cleaned = "";

            if (!string.IsNullOrEmpty(lastName))
            {
                cleaned = lastName.Trim().Replace(".", "").Replace(",", "").ToUpper();
            }

            return cleaned;
        }

        public static string TrimAndUpper(string text)
        {
            string formatted = "";

            if (!string.IsNullOrEmpty(text))
            {
                formatted = text.Trim().ToUpper();
            }

            return formatted;
        }

        public static string UpperAndRemoveSpaces(string text)
        {
            string formatted = "";

            if (!string.IsNullOrEmpty(text))
            {
                formatted = text.ToUpper().Replace(" ", "");
            }

            return formatted;
        }
    }
}
