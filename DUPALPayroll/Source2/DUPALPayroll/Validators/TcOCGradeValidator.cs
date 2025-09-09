using DUPALPayroll.Library;

// Harshan Nishantha
// 2014-02-07

namespace DUPALPayroll.Validators
{
    class TcOCGradeValidator
    {
        public static bool IsValid(string ocGrade)
        {
            if (!string.IsNullOrEmpty(ocGrade))
            {
                if (ocGrade.Length <= 3)
                {
                    if (TcString.IsNumeric(ocGrade))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
