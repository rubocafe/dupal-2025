using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-10-01

namespace DUPALPayroll.UI.Common.AnalyzeBean
{
    public enum TeEmployeeAnalyzeFilter
    {
        All,
        Valid,
        Employee_Age_Less_Than_18,
        Employee_Bank_and_Branch_Code_not_Found,
        Employee_Bank_Account_Number_Invalid,
        Employee_Bank_is_not_Supported_by_PayMaster,
        Employee_NIC_Number_Invalid,
        Employee_Number_Invalid,
        Employee_Member_Status_Invalid,
        Employee_Number_and_NIC_Empty,
        Employee_not_found_in_Master,
        Employee_Account_Number_Does_not_Match_with_Master,
        Employee_Bank_Transfer_Amount_is_Negative,
        Duplicate_Rows_in_Master_File_for_Employees,
        Master_Bank_or_Branch_Empty,
        Master_Bank_or_Branch_Does_not_Match_with_Employee,
        Master_Bank_or_Branch_Code_Empty,
        Master_Bank_or_Branch_Code_Does_not_Match_with_Employee,
        Master_NIC_not_Match_with_Employee,
        Master_Employee_Number_not_Match_with_Employee,
        Master_Basic_Salary_Does_not_Match_with_Employee,
        Master_LastName_is_Invalid,
        Master_OC_Grade_is_Invalid
    }
}
