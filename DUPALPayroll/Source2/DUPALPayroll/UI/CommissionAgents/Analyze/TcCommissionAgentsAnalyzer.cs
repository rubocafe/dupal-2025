using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.CommissionAgents.Commissions;
using DUPALPayroll.UI.CommissionAgents.CommissionsHeld;
using DUPALPayroll.UI.CommissionAgents.MasterData;
using DUPALPayroll.UI.Common.BanksAndBranches;
using DUPALPayroll.Validators;
using System;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.CommissionAgents.Analyze
{
    public class TcCommissionAgentsAnalyzer
    {
        private TcBindingList<TcCommissionAgentsAnalyzedRow> vnAndNICEmptyList = new TcBindingList<TcCommissionAgentsAnalyzedRow>();

        public TcBindingList<TcCommissionAgentsAnalyzedRow> Analyze(TcCommissionAgentsForm master)
        {
            vnAndNICEmptyList.Clear();

            TcBindingList<TcCommissionAgentsAnalyzedRow> list = new TcBindingList<TcCommissionAgentsAnalyzedRow>();

            TcCommissionAgentsMasterTable   masterTable             = master.MasterForm.MasterTable;
            TcBanksAndBranchesTable         banksAndBranchesTable   = master.BanksAndBranchesForm.BanksAndBranchesTable;
            TcCommissionsTable              commissionsTable        = master.CommissionsForm.CommissionsTable;
            TcCommissionsHeldTable          commissionsHeldTable    = master.CommissionHeldForm.CommissionsHeldTable;

            DateTime dobBoundryDate = new DateTime(master.SettingsForm.WorkingYearMonth.Year, master.SettingsForm.WorkingYearMonth.Month, 1);

            foreach (TcCommissionsRow commissionRow in commissionsTable.All)
            {
                TcCommissionAgentsAnalyzedRow paymasterRow = GetNewPayMasterData(commissionRow, dobBoundryDate);

                CheckValidEmployeeNumber(paymasterRow);

                CheckValidMemberStatus(paymasterRow);

                CheckVirtualNumber(paymasterRow);

                CheckAge(paymasterRow);
                
                CheckBankAccountNumber(paymasterRow);

                CheckNICNumber(paymasterRow);

                CheckEmptyVNandNIC(paymasterRow);

                CheckHeldAmount(commissionsHeldTable, paymasterRow);

                CheckBankTransferAmount(paymasterRow);

                TcCommissionAgentsMasterRow masterRow = masterTable.GetRow(paymasterRow.VirtualNumber, paymasterRow.NIC);
                LoadBanksAndBranchesData(banksAndBranchesTable, paymasterRow, masterRow);
                
                if (masterRow == null)
                {
                    string error = string.Format("Row is not found in master file. Virtual Number: [{0}], NIC: [{1}]", paymasterRow.VirtualNumber, paymasterRow.NIC);
                    paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agents_not_found_in_Master, error);
                }
                else
                {
                    LoadOtherMasterData(paymasterRow, masterRow);

                    CheckVirtualNumberWithMaster(paymasterRow, masterRow);

                    CheckEmployeeNumberWithMaster(paymasterRow, masterRow);

                    CheckNICWithMaster(paymasterRow, masterRow);

                    CheckBankAndBranchWithMaster(paymasterRow, masterRow);

                    CheckBankAndBranchCodeWithMaster(paymasterRow, masterRow);

                    CheckAccountWithMaster(paymasterRow, masterRow);

                    CheckValidLastNameInMaster(paymasterRow, masterRow);

                    CheckValidOCGradeInMaster(paymasterRow, masterRow);

                    paymasterRow.DuplicateMasterRows = masterTable.GetCommissionRowDuplicates(paymasterRow.VirtualNumber, paymasterRow.NIC);
                    if (paymasterRow.DuplicateMasterRows.Count > 0)
                    {
                        string lineNumbers = "";
                        foreach (TcCommissionAgentsMasterRow tempMasterRow in paymasterRow.DuplicateMasterRows)
                        {
                            lineNumbers += ("[" + tempMasterRow.LineNumber + "] ");
                        }

                        string error = string.Format("Duplicate rows [{0}] found in master file.\n\tLine Numbers: {1}", paymasterRow.DuplicateMasterRows.Count, lineNumbers);
                        paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Duplicate_Rows_in_Master_File_for_Agents, error);
                    }
                }

                list.Add(paymasterRow);
            }

            return list;
        }

        private static void CheckValidEmployeeNumber(TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidEmployeeOrEmployerNumberAfterClean(paymasterRow.EmployeeNumber))
            {
                string error = string.Format("Employee Number [{0}] is invalid", paymasterRow.EmployeeNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Employee_Number_Invalid, error);
            }
        }

        private static void CheckValidMemberStatus(TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidEpfMemberStatus(paymasterRow.MemberStatus))
            {
                string error = string.Format("Member Status [{0}] is invalid", paymasterRow.MemberStatus);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Member_Status_Invalid, error);
            }
        }

        private static void CheckVirtualNumber(TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            if (string.IsNullOrEmpty(paymasterRow.VirtualNumber) || paymasterRow.VirtualNumber == "0")
            {
                string error = string.Format("Virtual number [{0}] is empty", paymasterRow.VirtualNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Virtual_Number_Empty, error);
            }
        }

        private static void CheckAge(TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            if (paymasterRow.Age < 18)
            {
                string error = string.Format("Age is less than 18. NIC [{0}], Date of Birth [{1}], Age[{2}]", 
                    paymasterRow.NIC, 
                    paymasterRow.DateOfBirth.HasValue ? paymasterRow.DateOfBirth.Value.ToString("yyyy-MM-dd") : "--", 
                    paymasterRow.Age);

                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Age_Less_Than_18, error);
            }
        }

        private static TcCommissionAgentsAnalyzedRow GetNewPayMasterData(TcCommissionsRow data, DateTime dobBoundryDate)
        {
            TcCommissionAgentsAnalyzedRow paymasterRow = new TcCommissionAgentsAnalyzedRow();

            paymasterRow.LineNumber                = data.LineNumber;
            paymasterRow.VirtualNumber             = data.VirtualNumber;
            paymasterRow.Bank                      = data.Bank;
            paymasterRow.Branch                    = data.Branch;
            paymasterRow.DestinationAccount        = data.AccountNumber;
            paymasterRow.DestinationAccountName    = data.Name;
            paymasterRow.NIC                       = data.NIC;
            paymasterRow.NetCommission             = data.NetCommission;
            paymasterRow.Hold                      = 0;
            paymasterRow.BankCode                  = string.Empty;
            paymasterRow.BranchCode                = string.Empty;
            paymasterRow.DateOfJoin                = data.DateOfJoin;
            paymasterRow.TLorBPO                   = data.TLorBPO;

            paymasterRow.DOBBoundryDate            = dobBoundryDate;

            paymasterRow.EmployeeNumber     = data.EmployeeNumber;
            paymasterRow.EPFContribution    = data.EPFContribution;
            paymasterRow.ETFContribution    = data.ETFContribution;
            paymasterRow.MemberStatus       = data.MemberStatus;
            paymasterRow.DaysWorked         = data.DaysWorked;
            paymasterRow.EPFDeduction       = data.EPFDeduction;
            paymasterRow.GrossCommission    = data.GrossCommission;

            paymasterRow.Paye               = data.Paye;

            return paymasterRow;
        }

        private static void LoadBanksAndBranchesData(TcBanksAndBranchesTable banksAndBranchesTable, TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            TcBanksAndBranchesRow banksAndBranchesData = banksAndBranchesTable.GetRow(paymasterRow.Bank, paymasterRow.Branch);
            if (banksAndBranchesData == null)
            {
                bool gotFromMaster = false;

                string error = string.Format("Could not find bank and branch code. Bank [{0}], Branch [{1}]", paymasterRow.Bank, paymasterRow.Branch);
                if (masterRow != null)
                {
                    if (TcValidator.IsValidBank(masterRow.BankAcronym) &&
                        TcValidator.IsValidBranch(masterRow.Branch) &&
                        TcValidator.IsValidBankCode(masterRow.BankCode) &&
                        TcValidator.IsValidBranchCode(masterRow.BranchCode) &&
                        paymasterRow.Bank == masterRow.BankAcronym &&
                        paymasterRow.Branch == masterRow.Branch)
                    {
                        paymasterRow.BankCode = masterRow.BankCode;
                        paymasterRow.BranchCode = masterRow.BranchCode;

                        gotFromMaster = true;
                    }
                    else
                    {
                        error += string.Format("\n\tMaster Bank: [{0}], Master Branch: [{1}], Master Bank Code: [{2}], Master Branch Code: [{3}]",
                        masterRow.BankAcronym, masterRow.Branch, masterRow.BankCode, masterRow.BranchCode);
                    }
                }

                if (!gotFromMaster)
                {
                    if (TcValidator.IsPaymasterSupportedBank(paymasterRow.Bank))
                    {
                        paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found, error);
                    }
                    else
                    {
                        error = string.Format("Bank [{0}] is not suppoted by PayMaster", masterRow.Bank);
                        paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster, error);
                    }
                }
            }
            else
            {
                paymasterRow.BankCode      = TcString.AppendZerosToFront(banksAndBranchesData.BankCode.ToString(), 4);
                paymasterRow.BranchCode    = TcString.AppendZerosToFront(banksAndBranchesData.BranchCode.ToString(), 3);
            }
        }

        public static void LoadOtherMasterData(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            paymasterRow.Initials = masterRow.Initials;
            paymasterRow.LastName = masterRow.LastName;
            paymasterRow.OCGrade  = masterRow.OCGrade;
        }

        private static void CheckBankAccountNumber(TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidBankAccountNumber(paymasterRow.DestinationAccount))
            {
                string error = string.Format("Invalid bank account Number [{0}]", paymasterRow.DestinationAccount);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Bank_Account_Number_Invalid, error);
            }
        }

        private static void CheckNICNumber(TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidNIC(paymasterRow.NIC))
            {
                string error = string.Format("Invalid NIC Number [{0}]", paymasterRow.NIC);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_NIC_Number_Invalid, error);
            }
        }

        private void CheckEmptyVNandNIC(TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            if ((string.IsNullOrEmpty(paymasterRow.VirtualNumber) || paymasterRow.VirtualNumber == "0") && string.IsNullOrEmpty(paymasterRow.NIC))
            {
                if (!string.IsNullOrEmpty(paymasterRow.DestinationAccountName))
                {
                    string error = string.Format("Virtual Number and NIC is empty. Virtual Number: [{0}], NIC: [{1}]", paymasterRow.VirtualNumber, paymasterRow.NIC);
                    paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Virtual_Number_and_NIC_Empty, error);

                    vnAndNICEmptyList.Add(paymasterRow);
                }
            }
        }

        private void CheckHeldAmount(TcCommissionsHeldTable commissionHeldTable, TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            TcCommissionsHeldRow heldData = commissionHeldTable.GetRow(paymasterRow.VirtualNumber);

            if (heldData != null)
            {
                paymasterRow.Hold = heldData.Hold;

                if (paymasterRow.NetCommission != heldData.NetCommission)
                {
                    string error = string.Format("Agent Net commission mismatch with Held." +
                        "\n\tCommissions Net Commission: [{0}]" +
                        "\n\tCommissions Held Net Commission: [{1}]", paymasterRow.NetCommission, heldData.NetCommission);
                    paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Net_Commission_mismatch_with_Held, error);
                }
            }
        }

        public static void CheckBankTransferAmount(TcCommissionAgentsAnalyzedRow paymasterRow)
        {
            if (paymasterRow.Amount < 0)
            {
                string error = string.Format("Agent Bank Transfer Amount is Negative [{0}]", paymasterRow.Amount);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Bank_Transfer_Amount_is_Negative, error);
            }
        }

        private static void CheckVirtualNumberWithMaster(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            if (masterRow.VirtualNumber != paymasterRow.VirtualNumber)
            {
                string error = string.Format("Virtual number does not match with master file. Virtual Number: [{0}], Virtual Number in Master: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.VirtualNumber, masterRow.VirtualNumber, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_Virtual_Number_not_Match_with_Agent, error);
            }
        }

        private static void CheckEmployeeNumberWithMaster(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            if (masterRow.EmployeeNumber != paymasterRow.EmployeeNumber)
            {
                string error = string.Format("Employee number does not match with master file. Employee Number: [{0}], Employee Number in Master: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.EmployeeNumber, masterRow.EmployeeNumber, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_Employee_Number_not_Match_with_Agent, error);
            }
        }

        private static void CheckNICWithMaster(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            if (masterRow.NIC != paymasterRow.NIC)
            {
                string error = string.Format("NIC does not match with master file. NIC: [{0}], NIC in Master: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.NIC, masterRow.NIC, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_NIC_not_Match_with_Agents, error);
            }
        }

        private static void CheckBankAndBranchWithMaster(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(masterRow.Bank) || string.IsNullOrEmpty(masterRow.Branch))
            {
                string error = string.Format("Bank or branch empty in master file.\n\tBank: [{0}], Branch: [{1}]\n\tMaster Bank: [{2}], Master Branch: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.Bank, paymasterRow.Branch, masterRow.Bank, masterRow.Branch, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_Bank_or_Branch_Empty, error);
            }
            else
            {
                if (paymasterRow.Bank != masterRow.Bank ||
                    paymasterRow.Branch != masterRow.Branch)
                {
                    string error = string.Format("Bank or branch does not match with master file.\n\tBank: [{0}], Branch: [{1}]\n\tMaster Bank: [{2}], Master Branch: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.Bank, paymasterRow.Branch, masterRow.Bank, masterRow.Branch, masterRow.LineNumber);
                    paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_Bank_or_Branch_Does_not_Match_with_Agent, error);
                }
            }
        }

        private static void CheckBankAndBranchCodeWithMaster(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(masterRow.BankCode) || string.IsNullOrEmpty(masterRow.BranchCode) ||
                masterRow.BankCode == "0000" || masterRow.BranchCode == "000")
            {
                string error = string.Format("Bank Code or Branch Code empty in master file.\n\tBank Code: [{0}], Branch Code: [{1}]\n\tMaster Bank Code: [{2}], Master Branch Code: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.BankCode, paymasterRow.BranchCode, masterRow.BankCode, masterRow.BranchCode, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_Bank_or_Branch_Code_Empty, error);
            }
            else
            {
                if (!string.IsNullOrEmpty(paymasterRow.BankCode) && !string.IsNullOrEmpty(paymasterRow.BranchCode))
                {
                    if (paymasterRow.BankCode != masterRow.BankCode ||
                        paymasterRow.BranchCode != masterRow.BranchCode)
                    {
                        string error = string.Format("Bank Code or Branch Code does not match with master file.\n\tBank Code: [{0}], Branch Code: [{1}]\n\tMaster Bank Code: [{2}], Master Branch Code: [{3}]. Master Line Number: [{4}]",
                        paymasterRow.BankCode, paymasterRow.BranchCode, masterRow.BankCode, masterRow.BranchCode, masterRow.LineNumber);
                        paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_Bank_or_Branch_Code_Does_not_Match_with_Agent, error);
                    }
                }
            }
        }

        private static void CheckAccountWithMaster(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            string emptyAccountNumber = TcString.AppendZerosToFront("", 12);
            if (masterRow.AccountNumber != emptyAccountNumber && paymasterRow.DestinationAccount != masterRow.AccountNumber)
            {
                string error = string.Format("Account not match with master file. Account Number: [{0}], Master Account Number: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.DestinationAccount, masterRow.AccountNumber, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Agent_Account_Number_Does_not_Match_with_Master, error);
            }
        }

        private static void CheckValidLastNameInMaster(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(paymasterRow.LastName))
            {
                string error = string.Format("LastName [{0}] is invalid in master file. Master Line Number: [{1}]", 
                    masterRow.LastName, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_LastName_is_Invalid, error);
            }
        }

        private static void CheckValidOCGradeInMaster(TcCommissionAgentsAnalyzedRow paymasterRow, TcCommissionAgentsMasterRow masterRow)
        {
            if (!TcValidator.IsValidOCGrade(paymasterRow.OCGrade))
            {
                string error = string.Format("OC Grade [{0}] is invalid in master file. Master Line Number: [{1}]",
                    masterRow.OCGrade, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCommissionAgentsAnalyzeFilter.Master_OC_Grade_is_Invalid, error);
            }
        }
    }
}
