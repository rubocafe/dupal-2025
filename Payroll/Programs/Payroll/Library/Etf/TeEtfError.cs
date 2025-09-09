using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-01-02

namespace Payroll.Library.Etf
{
    public enum TeEtfError
    {
        All,
        Valid,
        Empty_NIC_Number,
        Empty_Member_Name,
        Invalid_NIC_Number,
        Invalid_Member_Number,
        Invalid_Employer_Number,
        Invalid_Total_Contribution_Value
    }
}
