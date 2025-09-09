using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Analyze
{
    public enum TeCareTakersAnalyzeFilter
    {
        All,
        Valid,
        Agent_Age_Less_Than_18,
        Agent_Bank_and_Branch_Code_not_Found,
        Agent_Bank_Account_Number_Invalid,
        Agent_Bank_is_not_Supported_by_PayMaster,
        Agent_NIC_Number_Invalid,
        Agent_NIC_Empty,
        Agents_not_found_in_Master,
        Agent_Account_Number_Does_not_Match_with_Master,
        Agent_Bank_Transfer_Amount_is_Negative,
        Duplicate_Rows_in_Master_File_for_Agents,
        Master_Bank_or_Branch_Empty,
        Master_Bank_or_Branch_Does_not_Match_with_Agent,
        Master_Bank_or_Branch_Code_Empty,
        Master_Bank_or_Branch_Code_Does_not_Match_with_Agent,
        Master_NIC_not_Match_with_Agents
    }
}
