using Payroll.Library;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-ll-04

namespace Payroll.UI.Controls
{
    public class TcListSearchHelper<T> where T:TiSearchable
    {
        public List<T> Search(List<T> data, string searchText)
        {
            List<T> results = new List<T>();

            if (string.IsNullOrEmpty(searchText))
            {
                return data;
            }

            if (data != null && searchText != null)
            {
                foreach (T row in data)
                {
                    string[] fields = row.SearchableFields();

                    foreach (string field in fields)
                    {
                        if (!string.IsNullOrEmpty(field))
                        {
                            Match match = Regex.Match(field, searchText, RegexOptions.IgnoreCase);
                            if (match.Success)
                            {
                                results.Add(row);
                                break;
                            }
                        }
                    }
                }
            }

            return results;
        }
    }
}
