using Payroll.Library.General;
using Payroll.Library.Payments;
using Payroll.UI.Business.Analyze;
using System.Collections.Generic;

// Harshan Nishantha
// 2015-11-06

namespace Payroll.UI.Business.Generate
{
    public class TcBusinessConditionsChecker
    {
        private TcBusinessForm master;
        private Dictionary<TeBusinessAnalyzeFilter, int> AnalyzeErrors = new Dictionary<TeBusinessAnalyzeFilter, int>();

        public TcBusinessConditionsChecker(TcBusinessForm master)
        {
            this.master = master;
        }

        public List<TcMandatoryCondition> GetList()
        {
            LoadAnalyzeErrors();

            List<TcMandatoryCondition> conditionsList = new List<TcMandatoryCondition>();

            TcMandatoryCondition tempCondition = new TcMandatoryCondition(
                                                    "All employees have valid bank account numbers",
                                                    "Some employees have invalid bank account numbers",
                                                    true,
                                                    AllEmployeesBankAccountNumbersAreValid());

            if (AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Bank_Account_Number_Invalid))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, 
                    AnalyzeErrors[TeBusinessAnalyzeFilter.Employee_Bank_Account_Number_Invalid]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            tempCondition = new TcMandatoryCondition(
                            "Bank Code and Branch Code found for all entries in Salary file",
                            "Bank Code or Branch Code not found for some entries in Salary file",
                            true,
                            BankAndBranchCodeFoundForAllRows());

            if (AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, 
                    AnalyzeErrors[TeBusinessAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            conditionsList.Add(new TcMandatoryCondition(
                "All employees account numbers match with account number in master file",
                "Some employees account numbers do not match with account number in master file",
                true,
                AccountNumberMatchWithMaster()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees have valid NIC numbers",
                "Some employees have invalid NIC numbers",
                true,
                AllEmployeesNICNumbersAreValid()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees are found in Master file",
                "Some employees are not found in Master file",
                true,
                AllEmployeesFoundInMaster()));

            conditionsList.Add(new TcMandatoryCondition(
                    "All employees have valid Employee numbers",
                    "Some employees have invalid Employee numbers",
                    true,
                    AllEmployeesEmployeeNumbersAreValid()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees have valid Member status",
                "Some employees have invalid Member status",
                true,
                AllMemberStatusAreValid()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees found in master file have valid LastNames",
                "Some employees found in master file have invalid LastNames",
                true,
                !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Master_LastName_is_Invalid)));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees found in master file have valid OC Grades",
                "Some employees found in master file have invalid OC Grades",
                true,
                !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Master_OC_Grade_is_Invalid)));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees found in master file have maching Employee Number",
                "Some employees found in master file have mismaches in Employee Number",
                true,
                !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Master_Employee_Number_not_Match)));

            tempCondition = new TcMandatoryCondition(
                                "All employees Banks are supported by PayMaster",
                                "Some employees Banks are not supported by PayMaster",
                                false,
                                AllEmployeesBanksAreSupported());

            if (AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, 
                    AnalyzeErrors[TeBusinessAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            conditionsList.Add(new TcMandatoryCondition(
                "All employees have valid EPF Deduction Values",
                "Some employees have invalid EPF Deduction Values",
                false,
                AllMembersEPFDeductionIsValid()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees have valid EPF Contribution Values",
                "Some employees have invalid EPF Contribution Values",
                false,
                AllMembersEPFContributionIsValid()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees have valid ETF Contribution Values",
                "Some employees have invalid ETF Contribution Values",
                false,
                AllMembersETFContributionIsValid()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees have non empty NIC in Salary file",
                "Some employees have empty NIC in Salary file",
                false,
                !HasEmptyNICRows()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees are over 16", 
                "Some employees are less than 16 years old",
                false,
                AllEmployeesAreOver18()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees in Salary file are unique", 
                "Some employees have duplicate entries in Salary file",
                false,
                AllEmployeesInSalaryFileAreUnique()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees with NIC have unique entries in Master file", 
                "Some employees with NIC have duplicate entries in Master file",
                false,
                !HasDuplicateRowsForEmployeesInSalaryFile()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees have positive Bank Transfer Amounts",
                "Some employees have negative Bank Transfer Amounts",
                false,
                AllEmployeesHavePositiveBankTransferAmounts()));

            List<TcMandatoryCondition> orderedConditionsList = new List<TcMandatoryCondition>();
            int errorConditionIndex = 0;
            foreach (TcMandatoryCondition condition in conditionsList)
            {
                if (condition.Satisfied)
                {
                    orderedConditionsList.Add(condition);
                }
                else
                {
                    orderedConditionsList.Insert(errorConditionIndex, condition);
                    errorConditionIndex++;
                }
            }

            return orderedConditionsList;
        }

        private string AppendPayMasterExcludeCountText(string text, int count)
        {
            return string.Format("{0} [{1} record(s) will be excluded from PayMaster]", text, count);
        }

        private void LoadAnalyzeErrors()
        {
            AnalyzeErrors.Clear();

            foreach (TcBusinessAnalyzedRow data in master.AnalyzeForm.AnalyzedRows)
            {
                foreach (TeBusinessAnalyzeFilter error in data.Errors.Keys)
                {
                    if (!AnalyzeErrors.ContainsKey(error))
                    {
                        AnalyzeErrors.Add(error, 1);
                    }
                    else
                    {
                        AnalyzeErrors[error]++;
                    }
                }
            }
        }

        private bool AllEmployeesAreOver18()
        {
            bool over18 = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Age_Less_Than_16);

            return over18;
        }

        private bool AllEmployeesBankAccountNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Bank_Account_Number_Invalid);

            return valid;
        }

        private bool AllEmployeesNICNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_NIC_Number_Invalid);

            return valid;
        }

        private bool AllEmployeesEmployeeNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Number_Invalid);

            return valid;
        }

        public bool AllMemberStatusAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Member_Status_Invalid);

            return valid;
        }

        public bool AllMembersEPFDeductionIsValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_EPF_Deduction_Invalid);

            return valid;
        }

        public bool AllMembersEPFContributionIsValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_EPF_Contribution_Invalid);

            return valid;
        }

        public bool AllMembersETFContributionIsValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_ETF_Contribution_Invalid);

            return valid;
        }

        private bool HasDuplicateRowsForEmployeesInSalaryFile()
        {
            bool hasDuplicates = AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Duplicate_Rows_in_Master_File_for_Employees);

            return hasDuplicates;
        }

        public bool BankAndBranchCodeFoundForAllRows()
        {
            bool hasRows = AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found);

            return !hasRows;
        }

        public bool AccountNumberMatchWithMaster()
        {
            bool misMatches = AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Account_Number_Does_not_Match_with_Master);

            return !misMatches;
        }

        public bool AllEmployeesBanksAreSupported()
        {
            bool supported = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster);

            return supported;
        }

        public bool HasEmptyNICRows()
        {
            bool hasEmpty = AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_NIC_Number_is_Empty);

            return hasEmpty;
        }

        public bool AllEmployeesInSalaryFileAreUnique()
        {
            bool unique = !(master.SalaryForm.Engine.HasNICDuplicates());

            return unique;
        }

        public bool AllEmployeesFoundInMaster()
        {
            bool found = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_not_found_in_Master);

            return found;
        }

        public bool AllEmployeesHavePositiveBankTransferAmounts()
        {
            bool allPositive = !AnalyzeErrors.ContainsKey(TeBusinessAnalyzeFilter.Employee_Bank_Transfer_Amount_is_Negative);

            return allPositive;
        }
    }
}
