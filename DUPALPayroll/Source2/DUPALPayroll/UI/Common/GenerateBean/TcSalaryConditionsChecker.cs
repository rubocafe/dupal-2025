using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.AnalyzeBean;
using DUPALPayroll.UI.Common.MasterBean;
using DUPALPayroll.UI.Common.PayMaster;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-10-01

namespace DUPALPayroll.UI.Common.GenerateBean
{
    public class TcSalaryConditionsChecker<T> where T : TcSalaryAnalyzedRow
    {
        private TcBindingList<T> rows;
        private bool allEmployeesInSalaryFileAreUnique;
        private Dictionary<TeEmployeeAnalyzeFilter, int> AnalyzeErrors = new Dictionary<TeEmployeeAnalyzeFilter, int>();
        private TcYearMonth workingYearMonth;

        public TcSalaryConditionsChecker(TcBindingList<T> rows, TcYearMonth workingYearMonth, bool allEmployeesInSalaryFileAreUnique)
        {
            this.rows = rows;
            this.workingYearMonth = workingYearMonth;
            this.allEmployeesInSalaryFileAreUnique = allEmployeesInSalaryFileAreUnique;
        }

        public List<TcMandatoryCondition> GetList()
        {
            LoadAnalyzeErrors();

            List<TcMandatoryCondition> conditionsList = new List<TcMandatoryCondition>();

            TcMandatoryCondition tempCondition = new TcMandatoryCondition(
                                                "All bank account numbers are valid",
                                                "Some bank account numbers are invalid",
                                                true,
                                                AllEmployeesBankAccountNumbersAreValid());

            if (AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Bank_Account_Number_Invalid))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, 
                    AnalyzeErrors[TeEmployeeAnalyzeFilter.Employee_Bank_Account_Number_Invalid]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            tempCondition = new TcMandatoryCondition(
                                "Bank Code and Branch Code found for all entries in Salary file",
                                "Bank Code or Branch Code not found for some entries in Salary file",
                                true,
                                BankAndBranchCodeFoundForAllRows());


            if (AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, 
                    AnalyzeErrors[TeEmployeeAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

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
                "All employee account numbers match with account number in master file",
                "Some employee account numbers do not match with account number in master file",
                true,
                AccountNumberMatchWithMaster()));

            conditionsList.Add(new TcMandatoryCondition(
                    "All employees found in master file have maching Employee Number",
                    "Some employees found in master file have mismaches in Employee Number",
                    true,
                    !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Master_Employee_Number_not_Match_with_Employee)));

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                conditionsList.Add(new TcMandatoryCondition(
                    "All employees have valid Member status",
                    "Some employees have invalid Member status",
                    true,
                    AllMemberStatusAreValid()));

                conditionsList.Add(new TcMandatoryCondition(
                    "All employees found in master file have valid LastNames",
                    "Some employees found in master file have invalid LastNames",
                    true,
                    !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Master_LastName_is_Invalid)));

                conditionsList.Add(new TcMandatoryCondition(
                    "All employees found in master file have valid OC Grades",
                    "Some employees found in master file have invalid OC Grades",
                    true,
                    !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Master_OC_Grade_is_Invalid)));
            }

            tempCondition = new TcMandatoryCondition(
                            "All employees Banks are supported by PayMaster",
                            "Some employees Banks are not supported by PayMaster",
                            false,
                            AllEmployeesBanksAreSupported());

            if (AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster))
            {
                tempCondition.UnsatisfiedDescription = 
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, AnalyzeErrors[TeEmployeeAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            conditionsList.Add(new TcMandatoryCondition(
                "All employees have non empty employee number or NIC in Salary file",
                "Some employees have empty employee number and NIC in Salary file",
                false,
                !HasEmptyENandNICRows()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees are over 18", 
                "Some employees are less than 18 years old",
                false,
                AllEmployeesAreOver18()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees in Salary file are unique",
                "Some employees have duplicate entries in salary file",
                false,
                AllEmployeesInSalaryFileAreUnique()));

            conditionsList.Add(new TcMandatoryCondition(
                "All employees with non-zero employee number have unique entries in Master file", 
                "Some employees with non-zero employee number have duplicate entries in Master file",
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

            foreach (T data in rows)
            {
                foreach (TeEmployeeAnalyzeFilter error in data.Errors.Keys)
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
            bool over18 = !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Age_Less_Than_18);

            return over18;
        }

        private bool AllEmployeesBankAccountNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Bank_Account_Number_Invalid);

            return valid;
        }

        public bool AccountNumberMatchWithMaster()
        {
            bool misMatches = AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Account_Number_Does_not_Match_with_Master);

            return !misMatches;
        }

        public bool AllMemberStatusAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Member_Status_Invalid);

            return valid;
        }

        public bool AllEmployeesBanksAreSupported()
        {
            bool supported = !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster);

            return supported;
        }

        private bool AllEmployeesNICNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_NIC_Number_Invalid);

            return valid;
        }

        private bool AllEmployeesEmployeeNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Number_Invalid);

            return valid;
        }

        private bool HasDuplicateRowsForEmployeesInSalaryFile()
        {
            bool hasDuplicates = AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Duplicate_Rows_in_Master_File_for_Employees);

            return hasDuplicates;
        }

        public bool BankAndBranchCodeFoundForAllRows()
        {
            bool hasRows = AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found);

            return !hasRows;
        }

        public bool HasEmptyENandNICRows()
        {
            bool hasEmpty = AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Number_and_NIC_Empty);

            return hasEmpty;
        }

        public bool AllEmployeesInSalaryFileAreUnique()
        {
            //bool unique = !(master.SalaryForm.SalaryTable.HasNICDuplicates() || master.SalaryForm.SalaryTable.HasEmployeeNumberDuplicates());

            //return unique;
            return allEmployeesInSalaryFileAreUnique;
        }

        public bool AllEmployeesFoundInMaster()
        {
            bool found = !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_not_found_in_Master);

            return found;
        }

        public bool AllEmployeesHavePositiveBankTransferAmounts()
        {
            bool allPositive = !AnalyzeErrors.ContainsKey(TeEmployeeAnalyzeFilter.Employee_Bank_Transfer_Amount_is_Negative);

            return allPositive;
        }
    }
}
