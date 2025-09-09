using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-09-05

namespace DUPALPayroll.General
{
    public class TcEnum
    {
        public static string GetTextForEnum<T>(T filter) where T : struct
        {
            string text = filter.ToString().Replace("_", " ");

            return text;
        }

        public static T GetEnumForText<T>(T defaultOption, string text) where T : struct
        {
            T filter = defaultOption;
            string enumText = text.Replace(" ", "_");
            Enum.TryParse<T>(enumText, out filter);

            return filter;
        }
    }
}
