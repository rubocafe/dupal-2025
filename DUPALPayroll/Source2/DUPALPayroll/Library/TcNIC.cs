using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-09-24

namespace DUPALPayroll.Library
{
    public class TcNIC
    {
        public static Nullable<DateTime> GetDateOfBirthFromNIC(string nicNumber)
        {
            Nullable<DateTime> dateOfBirth = null;

            try
            {
                if (!string.IsNullOrEmpty(nicNumber))
                {
                    string yearString = nicNumber.Substring(0, 2);
                    string daysString = nicNumber.Substring(2, 3);

                    int yearInt = int.Parse(yearString);
                    int daysInt = int.Parse(daysString);

                    int firstDigitsOfCurrentYear = int.Parse(DateTime.Now.ToString("yyyy").Substring(0, 2));
                    int lastDigitsOfCurrentYear = int.Parse(DateTime.Now.ToString("yyyy").Substring(2, 2));
                    int year = 0;
                    if (yearInt < lastDigitsOfCurrentYear)
                    {
                        year = int.Parse(string.Format("{0}{1}", firstDigitsOfCurrentYear, yearString));
                    }
                    else
                    {
                        year = int.Parse(string.Format("{0}{1}", firstDigitsOfCurrentYear - 1, yearString));
                    }

                    if (daysInt > 500) // Female
                    {
                        daysInt = daysInt - 500;
                    }

                    dateOfBirth = new DateTime(year, 1, 1).AddDays(daysInt - 1);
                }
            }
            catch (Exception)
            {
            }

            return dateOfBirth;
        }
    }
}
