using DUPALPayroll.Library;
using System.Collections.Generic;
using System.Text.RegularExpressions;

// Harshan Nishantha
// 2013-12-05

namespace DUPALPayroll.Validators
{
    public class TcPaymasterCharacterValidator
    {
        public Dictionary<int, char> InvalidCharachters = new Dictionary<int, char>();
        private List<char> validCharachters = new List<char>();

        public TcPaymasterCharacterValidator()
        {
            LoadValidChars();
        }

        private void LoadValidChars()
        {
            for (int i = 'a'; i <= 'z'; i++)
            {
                validCharachters.Add((char)i);
            }

            for (int i = 'A'; i <= 'Z'; i++)
            {
                validCharachters.Add((char)i);
            }

            for (int i = '0'; i <= '9'; i++)
            {
                validCharachters.Add((char)i);
            }

            validCharachters.Add('.');
            validCharachters.Add('@');
            validCharachters.Add('(');
            validCharachters.Add(')');
            validCharachters.Add(' ');
            validCharachters.Add('\'');
        }

        public bool IsValid(string line)
        {
            InvalidCharachters.Clear();

            if (!string.IsNullOrEmpty(line))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    char charchter = line[i];
                    if (!validCharachters.Contains(charchter))
                    {
                        InvalidCharachters.Add(i, charchter);
                    }
                }
            }

            if (InvalidCharachters.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetInvalidCharchtersString()
        {
            string text = "";

            foreach (KeyValuePair<int, char> pair in InvalidCharachters)
            {
                text += string.Format("[{0}:{1}]", pair.Key, (int)pair.Value);
            }

            return text;
        }
    }
}
