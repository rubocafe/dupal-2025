using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-08-30

namespace DUPALPayroll.UI.CommissionAgents.Tools.Compare
{
    public enum TePayMasterCompareFilter
    {
        All,
        Not_Matched,
        Destination_Bank_is_Different,
        Destination_Branch_is_Different,
        Destination_Account_is_Different,
        Destination_Account_Name_is_Different,
        Credit_Debit_Code_is_Different,
        Amount_is_Different,
        Originating_Bank_is_Different,
        Originating_Branch_is_Different,
        Originating_Account_is_Different,
        Originating_Account_Name_is_Different,
        NIC_is_Different,
        Reference_is_Different,
        Value_Date_is_Different
    }
}
