using DUPALPayroll.Library.Date;

// Harshan Nishantha
// 2014-01-08

namespace DUPALPayroll.General
{
    public class TcVersions
    {
        public static bool IsEpfEtfSupported(TcYearMonth workingYearMonth)
        {
            if (workingYearMonth.Year > 2013)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsConfigFileSupported(TcYearMonth workingYearMonth)
        {
            if (workingYearMonth.Year > 2013)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsPayeSupported(TcYearMonth workingYearMonth)
        {
            if (workingYearMonth.Year == 2014 && workingYearMonth.Month > 2)
            {
                return true;
            }
            else if (workingYearMonth.Year > 2014)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * Added following fields support to Customer Care from 2014-05
         * 
         * Performance Incentive (Sales Commission)
         * Performance Incentive (Upselling & E-Billing Incentive)
         * 
         * */
        public static bool IsCustomerCareFR001Supported(TcYearMonth workingYearMonth)
        {
            if (workingYearMonth.Year == 2014 && workingYearMonth.Month > 4)
            {
                return true;
            }
            else if (workingYearMonth.Year > 2014)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
