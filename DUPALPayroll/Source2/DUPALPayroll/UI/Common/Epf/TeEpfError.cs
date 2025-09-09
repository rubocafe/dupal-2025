using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2014-01-02

namespace DUPALPayroll.UI.Common.Epf
{
    public enum TeEpfError
    {
        All,
        Valid,
        Empty_NIC_Number,
        Empty_Member_Name,
        Invalid_NIC_Number,
        Invalid_Member_Number,
        Invalid_Employer_Number,
        Invalid_Member_Status,
        Invalid_Days_of_Work_Value,
        Invalid_Total_Contribution_Value,
        Invalid_Employer_Contribution_Value,
        Invalid_Member_Contribution_Value,
        Invalid_Total_Earnings_Value,
        Invalid_OCGrade
    }
}
