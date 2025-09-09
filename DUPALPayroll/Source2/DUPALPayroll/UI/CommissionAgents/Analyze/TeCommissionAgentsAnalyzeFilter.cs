using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.CommissionAgents.Analyze
{
    public enum TeCommissionAgentsAnalyzeFilter
    {
        All,
        Valid,
        Agent_Age_Less_Than_18,
        Agent_Bank_and_Branch_Code_not_Found,
        Agent_Bank_Account_Number_Invalid,
        Agent_Bank_is_not_Supported_by_PayMaster,
        Agent_NIC_Number_Invalid,
        Agent_Employee_Number_Invalid,
        Agent_Member_Status_Invalid,
        Agent_Virtual_Number_Empty,
        Agent_Virtual_Number_and_NIC_Empty,
        Agents_not_found_in_Master,
        Agent_Account_Number_Does_not_Match_with_Master,
        Agent_Net_Commission_mismatch_with_Held,
        Agent_Bank_Transfer_Amount_is_Negative,
        Duplicate_Rows_in_Master_File_for_Agents,
        Master_Bank_or_Branch_Empty,
        Master_Bank_or_Branch_Does_not_Match_with_Agent,
        Master_Bank_or_Branch_Code_Empty,
        Master_Bank_or_Branch_Code_Does_not_Match_with_Agent,
        Master_NIC_not_Match_with_Agents,
        Master_Virtual_Number_not_Match_with_Agent,
        Master_Employee_Number_not_Match_with_Agent,
        Master_LastName_is_Invalid,
        Master_OC_Grade_is_Invalid
    }
}
