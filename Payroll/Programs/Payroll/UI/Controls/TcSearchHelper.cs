using Payroll.Library;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

// Harshan Nishantha
// 2015-ll-04

namespace Payroll.UI.Controls
{
    public class TcSearchHelper<T> where T:TiSearchable
    {
        public TcBindingList<T> Search(BindingSource source, string searchText)
        {
            TcBindingList<T> list = source.DataSource as TcBindingList<T>;

            TcBindingList<T> results = new TcBindingList<T>();

            if (string.IsNullOrEmpty(searchText))
            {
                return list;
            }

            if (list != null && searchText != null)
            {
                foreach (T row in list)
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
