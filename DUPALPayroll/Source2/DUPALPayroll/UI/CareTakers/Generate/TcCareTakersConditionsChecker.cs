using DUPALPayroll.UI.CareTakers.Analyze;
using DUPALPayroll.UI.Common.PayMaster;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Generate
{
    public class TcCareTakersConditionsChecker
    {
        private TcCareTakersForm master;
        private Dictionary<TeCareTakersAnalyzeFilter, int> AnalyzeErrors = new Dictionary<TeCareTakersAnalyzeFilter, int>();

        public TcCareTakersConditionsChecker(TcCareTakersForm master)
        {
            this.master = master;
        }

        public List<TcMandatoryCondition> GetList()
        {
            LoadAnalyzeErrors();

            List<TcMandatoryCondition> conditionsList = new List<TcMandatoryCondition>();

            TcMandatoryCondition tempCondition = new TcMandatoryCondition(
                                                    "All agents have valid bank account numbers",
                                                    "Some agents have invalid bank account numbers",
                                                    true,
                                                    AllAgentsBankAccountNumbersAreValid());
            
            if (AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Bank_Account_Number_Invalid))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, AnalyzeErrors[TeCareTakersAnalyzeFilter.Agent_Bank_Account_Number_Invalid]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            tempCondition = new TcMandatoryCondition(
                                "Bank Code and Branch Code found for all entries in Payments file",
                                "Bank Code or Branch Code not found for some entries in Payments file",
                                true,
                                BankAndBranchCodeFoundForAllRows());

            if (AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, AnalyzeErrors[TeCareTakersAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            conditionsList.Add(new TcMandatoryCondition(
                "All agent account numbers match with account number in master file",
                "Some agent account numbers do not match with account number in master file",
                true,
                AccountNumberMatchWithMaster()));

            tempCondition = new TcMandatoryCondition(
                                "All agents Banks are supported by PayMaster",
                                "Some agents Banks are not supported by PayMaster",
                                false,
                                AllAgentsBanksAreSupported());
            
            if (AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, AnalyzeErrors[TeCareTakersAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            conditionsList.Add(new TcMandatoryCondition(
                "All agents have non empty NIC in Payments file",
                "Some agents have empty NIC in Payments file",
                false,
                !HasEmptyVNandNICRows()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents are over 18", 
                "Some agents are less than 18 years old",
                false,
                AllAgentsAreOver18()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents have valid NIC numbers", 
                "Some agents have invalid NIC numbers",
                false,
                AllAgentsNICNumbersAreValid()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents in Payments file are unique",
                "Some agents have duplicate entries in Payments file",
                false,
                AllAgentsInCommissionFileAreUnique()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents have unique entries in Master file", 
                "Some agents have duplicate entries in Master file",
                false,
                !HasDuplicateRowsForAgentsInCommissionsFile()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents are found in Master file", 
                "Some agents are not found in Master file",
                false,
                AllAgentsFoundInMaster()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents have positive Bank Transfer Amounts",
                "Some agents have negative Bank Transfer Amounts",
                false,
                AllAgentsHavePositiveBankTransferAmounts()));

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

            foreach (TcCareTakersAnalyzedRow data in master.AnalyzeForm.AnalyzedRows)
            {
                foreach (TeCareTakersAnalyzeFilter error in data.Errors.Keys)
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

        private bool AllAgentsAreOver18()
        {
            bool over18 = !AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Age_Less_Than_18);

            return over18;
        }

        private bool AllAgentsBankAccountNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Bank_Account_Number_Invalid);

            return valid;
        }

        private bool AllAgentsNICNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_NIC_Number_Invalid);

            return valid;
        }

        private bool HasDuplicateRowsForAgentsInCommissionsFile()
        {
            bool hasDuplicates = AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Duplicate_Rows_in_Master_File_for_Agents);

            return hasDuplicates;
        }

        public bool BankAndBranchCodeFoundForAllRows()
        {
            bool hasRows = AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found);

            return !hasRows;
        }

        public bool AccountNumberMatchWithMaster()
        {
            bool misMatches = AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Account_Number_Does_not_Match_with_Master);

            return !misMatches;
        }

        public bool AllAgentsBanksAreSupported()
        {
            bool supported = !AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster);

            return supported;
        }

        public bool HasEmptyVNandNICRows()
        {
            bool hasEmpty = AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_NIC_Empty);

            return hasEmpty;
        }

        public bool AllAgentsInCommissionFileAreUnique()
        {
            bool unique = !(master.PymentsForm.CommissionsTable.HasNICDuplicates());

            return unique;
        }

        public bool AllAgentsFoundInMaster()
        {
            bool found = !AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agents_not_found_in_Master);

            return found;
        }

        public bool AllAgentsHavePositiveBankTransferAmounts()
        {
            bool allPositive = !AnalyzeErrors.ContainsKey(TeCareTakersAnalyzeFilter.Agent_Bank_Transfer_Amount_is_Negative);

            return allPositive;
        }
    }
}
