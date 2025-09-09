using DUPALPayroll.General;
using DUPALPayroll.UI.CommissionAgents.Analyze;
using DUPALPayroll.UI.Common.PayMaster;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-06

namespace DUPALPayroll.UI.CommissionAgents.Generate
{
    public class TcCommissionAgentsConditionsChecker
    {
        private TcCommissionAgentsForm master;
        private Dictionary<TeCommissionAgentsAnalyzeFilter, int> AnalyzeErrors = new Dictionary<TeCommissionAgentsAnalyzeFilter, int>();

        public TcCommissionAgentsConditionsChecker(TcCommissionAgentsForm master)
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

            if (AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Bank_Account_Number_Invalid))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, AnalyzeErrors[TeCommissionAgentsAnalyzeFilter.Agent_Bank_Account_Number_Invalid]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            tempCondition = new TcMandatoryCondition(
                            "Bank Code and Branch Code found for all entries in Commissions file",
                            "Bank Code or Branch Code not found for some entries in Commissions file",
                            true,
                            BankAndBranchCodeFoundForAllRows());

            if (AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, AnalyzeErrors[TeCommissionAgentsAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            conditionsList.Add(new TcMandatoryCondition(
                "All Net Commision amounts in CommissionsHeld file matches with the Net Commissions in Commissions file",
                "Some Net Commission amounts in CommissionsHeld file does not match with Net Commisions amount in Commissions file",
                true,
                NetCommissionMatchInHeldFile()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agent account numbers match with account number in master file",
                "Some agent account numbers do not match with account number in master file",
                true,
                AccountNumberMatchWithMaster()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents have valid NIC numbers",
                "Some agents have invalid NIC numbers",
                true,
                AllAgentsNICNumbersAreValid()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents are found in Master file",
                "Some agents are not found in Master file",
                true,
                AllAgentsFoundInMaster()));

            if (TcVersions.IsEpfEtfSupported(master.SettingsForm.WorkingYearMonth))
            {
                conditionsList.Add(new TcMandatoryCondition(
                    "All agents have valid Employee numbers",
                    "Some agents have invalid Employee numbers",
                    true,
                    AllAgentsEmployeeNumbersAreValid()));

                conditionsList.Add(new TcMandatoryCondition(
                    "All agents have valid Member status",
                    "Some agents have invalid Member status",
                    true,
                    AllMemberStatusAreValid()));

                conditionsList.Add(new TcMandatoryCondition(
                    "All agents found in master file have valid LastNames",
                    "Some agents found in master file have invalid LastNames",
                    true,
                    !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Master_LastName_is_Invalid)));

                conditionsList.Add(new TcMandatoryCondition(
                    "All agents found in master file have valid OC Grades",
                    "Some agents found in master file have invalid OC Grades",
                    true,
                    !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Master_OC_Grade_is_Invalid)));

                conditionsList.Add(new TcMandatoryCondition(
                    "All agents found in master file have maching Employee Number",
                    "Some agents found in master file have mismaches in Employee Number",
                    true,
                    !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Master_Employee_Number_not_Match_with_Agent)));
            }

            tempCondition = new TcMandatoryCondition(
                                "All agents Banks are supported by PayMaster",
                                "Some agents Banks are not supported by PayMaster",
                                false,
                                AllAgentsBanksAreSupported());

            if (AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster))
            {
                tempCondition.UnsatisfiedDescription =
                    AppendPayMasterExcludeCountText(tempCondition.UnsatisfiedDescription, AnalyzeErrors[TeCommissionAgentsAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster]);
                tempCondition.RowDisplayType = TeRowDisplaytype.Error;
            }
            conditionsList.Add(tempCondition);

            conditionsList.Add(new TcMandatoryCondition(
                "All agents have non empty virtual number or NIC in Commissions file",
                "Some agents have empty virtual number and NIC in Commissions file",
                false,
                !HasEmptyVNandNICRows()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents are over 18", 
                "Some agents are less than 18 years old",
                false,
                AllAgentsAreOver18()));


            conditionsList.Add(new TcMandatoryCondition(
                "All agents in Commissions Held file are unique", 
                "Some agents have duplicate entries in Commissions Held file",
                false,
                AgentsInCommissionHeldAreUnique()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents in Commissions Held file are found in Commissions file", 
                "Some agents in Commissions Held file are not found in Commissions file",
                false,
                !HasCommissionsHeldRowsNotMappedWithCommissionsAgents()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents in Commissions file are unique", 
                "Some agents have duplicate entries in Commissions file",
                false,
                AllAgentsInCommissionFileAreUnique()));

            conditionsList.Add(new TcMandatoryCondition(
                "All agents with non-zero virtual number have unique entries in Master file", 
                "Some agents with non-zero virtual number have duplicate entries in Master file",
                false,
                !HasDuplicateRowsForAgentsInCommissionsFile()));

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

            foreach (TcCommissionAgentsAnalyzedRow data in master.AnalyzeForm.AnalyzedRows)
            {
                foreach (TeCommissionAgentsAnalyzeFilter error in data.Errors.Keys)
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
            bool over18 = !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Age_Less_Than_18);

            return over18;
        }

        private bool AllAgentsBankAccountNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Bank_Account_Number_Invalid);

            return valid;
        }

        private bool AllAgentsNICNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_NIC_Number_Invalid);

            return valid;
        }

        private bool AllAgentsEmployeeNumbersAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Employee_Number_Invalid);

            return valid;
        }

        public bool AllMemberStatusAreValid()
        {
            bool valid = !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Member_Status_Invalid);

            return valid;
        }

        private bool AgentsInCommissionHeldAreUnique()
        {
            bool unique = !master.CommissionHeldForm.CommissionsHeldTable.HasDuplicates();

            return unique;
        }

        private bool HasCommissionsHeldRowsNotMappedWithCommissionsAgents()
        {
            int count = master.CommissionHeldForm.CommissionsHeldTable.GetCommissionsHeldRowsNotMappedWithCommissionsAgents(master.CommissionsForm.CommissionsTable).Count;

            return (count > 0);
        }

        private bool HasDuplicateRowsForAgentsInCommissionsFile()
        {
            bool hasDuplicates = AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Duplicate_Rows_in_Master_File_for_Agents);

            return hasDuplicates;
        }

        public bool BankAndBranchCodeFoundForAllRows()
        {
            bool hasRows = AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found);

            return !hasRows;
        }

        public bool NetCommissionMatchInHeldFile()
        {
            bool misMatches = AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Net_Commission_mismatch_with_Held);

            return !misMatches;
        }

        public bool AccountNumberMatchWithMaster()
        {
            bool misMatches = AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Account_Number_Does_not_Match_with_Master);

            return !misMatches;
        }

        public bool AllAgentsBanksAreSupported()
        {
            bool supported = !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster);

            return supported;
        }

        public bool HasEmptyVNandNICRows()
        {
            bool hasEmpty = AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Virtual_Number_and_NIC_Empty);

            return hasEmpty;
        }

        public bool AllAgentsInCommissionFileAreUnique()
        {
            bool unique = !(master.CommissionsForm.CommissionsTable.HasNICDuplicates() || master.CommissionsForm.CommissionsTable.HasVNDuplicates());

            return unique;
        }

        public bool AllAgentsFoundInMaster()
        {
            bool found = !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agents_not_found_in_Master);

            return found;
        }

        public bool AllAgentsHavePositiveBankTransferAmounts()
        {
            bool allPositive = !AnalyzeErrors.ContainsKey(TeCommissionAgentsAnalyzeFilter.Agent_Bank_Transfer_Amount_is_Negative);

            return allPositive;
        }
    }
}
