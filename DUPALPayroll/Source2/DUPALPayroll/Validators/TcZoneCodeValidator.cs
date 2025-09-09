using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-01-07

namespace DUPALPayroll.Validators
{
    public class TcZoneCodeValidator
    {
        public static bool IsValid(string zoneCode)
        {
            if (!string.IsNullOrEmpty(zoneCode))
            {
                if (zoneCode.Length == 1 && char.IsLetter(zoneCode.ToUpper(), 0))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
