
// Harshan Nishantha
// 2014-01-08

namespace Payroll.Validators
{
    public class TcEpfMemberStatusValidator
    {
        public static bool IsValid(string status)
        {
            if (!string.IsNullOrEmpty(status))
            {
                if (status.Length == 1)
                {
                    if (status == "E" || status == "V" || status == "N")
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
