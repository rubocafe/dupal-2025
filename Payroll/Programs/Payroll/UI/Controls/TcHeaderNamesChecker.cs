using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2015-10-06

namespace Payroll.UI.Controls
{
    public class TcHeaderNamesChecker
    {
        public bool AllHeadersFound { get; set; }
        public string Error { get; set; }

        public bool CheckHeaderNames(Dictionary<string, int> headerIndexes, List<string> mandatoryHeaderNames)
        {
            AllHeadersFound = false;
            Error = string.Empty;

            List<string> headersNotFound = new List<string>();
            foreach (string headerName in mandatoryHeaderNames)
            {
                if (!headerIndexes.ContainsKey(headerName))
                {
                    headersNotFound.Add(headerName);
                }
            }

            if (headersNotFound.Count > 0)
            {
                string notFoundHeaders = string.Empty;
                foreach (string notFoundHeader in headersNotFound)
                {
                    if (notFoundHeaders == string.Empty)
                    {
                        notFoundHeaders = string.Format("{0}", notFoundHeader);
                    }
                    else
                    {
                        notFoundHeaders = string.Format("{0}, {1}", notFoundHeaders, notFoundHeader);
                    }
                }

                AllHeadersFound = false;
                Error = string.Format("Some Header rows [{0}] not found", notFoundHeaders);
            }
            else
            {
                AllHeadersFound = true;
            }

            return AllHeadersFound;
        }
    }
}
