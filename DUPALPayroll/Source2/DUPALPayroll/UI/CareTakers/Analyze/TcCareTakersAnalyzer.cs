using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.CareTakers.MasterData;
using DUPALPayroll.UI.CareTakers.Payments;
using DUPALPayroll.UI.Common.BanksAndBranches;
using System;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Analyze
{
    public class TcCareTakersAnalyzer
    {
        private TcBindingList<TcCareTakersAnalyzedRow> nicEmptyList = new TcBindingList<TcCareTakersAnalyzedRow>();

        public TcBindingList<TcCareTakersAnalyzedRow> Analyze(TcCareTakersForm master)
        {
            nicEmptyList.Clear();

            TcBindingList<TcCareTakersAnalyzedRow> list = new TcBindingList<TcCareTakersAnalyzedRow>();

            TcCareTakersMasterTable         masterTable             = master.MasterForm.MasterTable;
            TcBanksAndBranchesTable         banksAndBranchesTable   = master.BanksAndBranchesForm.BanksAndBranchesTable;
            TcCareTakersPaymentsTable       paymentsTable           = master.PymentsForm.CommissionsTable;

            DateTime dobBoundryDate = new DateTime(master.SettingsForm.WorkingYearMonth.Year, master.SettingsForm.WorkingYearMonth.Month, 1);

            foreach (TcCareTakersPaymentsRow commissionRow in paymentsTable.All)
            {
                TcCareTakersAnalyzedRow paymasterRow = GetNewPayMasterData(commissionRow, dobBoundryDate);

                CheckAge(paymasterRow);
                
                CheckBankAccountNumber(paymasterRow);

                CheckNICNumber(paymasterRow);

                CheckEmptyNIC(paymasterRow);

                CheckBankTransferAmount(paymasterRow);

                TcCareTakersMasterRow masterRow = masterTable.GetRow(paymasterRow.NIC);
                LoadBanksAndBranchesData(banksAndBranchesTable, paymasterRow, masterRow);
                if (masterRow == null)
                {
                    string error = string.Format("Row is not found in master file. NIC: [{0}]", paymasterRow.NIC);
                    paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agents_not_found_in_Master, error);
                }
                else
                {
                    CheckNICWithMaster(paymasterRow, masterRow);

                    CheckBankAndBranchWithMaster(paymasterRow, masterRow);

                    CheckBankAndBranchCodeWithMaster(paymasterRow, masterRow);

                    CheckAccountWithMaster(paymasterRow, masterRow);

                    paymasterRow.DuplicateMasterRows = masterTable.GetCommissionRowDuplicates(paymasterRow.NIC);
                    if (paymasterRow.DuplicateMasterRows.Count > 0)
                    {
                        string lineNumbers = "";
                        foreach (TcCareTakersMasterRow tempMasterRow in paymasterRow.DuplicateMasterRows)
                        {
                            lineNumbers += ("[" + tempMasterRow.LineNumber + "] ");
                        }

                        string error = string.Format("Duplicate rows [{0}] found in master file.\n\tLine Numbers: {1}", paymasterRow.DuplicateMasterRows.Count, lineNumbers);
                        paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Duplicate_Rows_in_Master_File_for_Agents, error);
                    }
                }

                list.Add(paymasterRow);
            }

            return list;
        }

        private static void CheckAge(TcCareTakersAnalyzedRow paymasterRow)
        {
            if (paymasterRow.Age < 18)
            {
                string error = string.Format("Age is less than 18. NIC [{0}], Date of Birth [{1}], Age[{2}]", 
                    paymasterRow.NIC, 
                    paymasterRow.DateOfBirth.HasValue ? paymasterRow.DateOfBirth.Value.ToString("yyyy-MM-dd") : "--", 
                    paymasterRow.Age);

                paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agent_Age_Less_Than_18, error);
            }
        }

        private static TcCareTakersAnalyzedRow GetNewPayMasterData(TcCareTakersPaymentsRow data, DateTime dobBoundryDate)
        {
            TcCareTakersAnalyzedRow paymasterRow = new TcCareTakersAnalyzedRow();

            paymasterRow.LineNumber                = data.LineNumber;
            paymasterRow.SiteName                  = data.SiteName;
            paymasterRow.SiteCode                  = data.SiteCode;
            paymasterRow.SiteEngineer              = data.SiteEngineer;
            paymasterRow.Bank                      = data.Bank;
            paymasterRow.Branch                    = data.Branch;
            paymasterRow.DestinationAccount        = data.AccountNumber;
            paymasterRow.DestinationAccountName    = data.Name;
            paymasterRow.NIC                       = data.NIC;
            paymasterRow.Payment                   = data.Payment;
            paymasterRow.Hold                      = data.Hold;
            paymasterRow.BankCode                  = string.Empty;
            paymasterRow.BranchCode                = string.Empty;

            paymasterRow.DOBBoundryDate            = dobBoundryDate;

            return paymasterRow;
        }

        private static void LoadBanksAndBranchesData(TcBanksAndBranchesTable banksAndBranchesTable, TcCareTakersAnalyzedRow paymasterRow, TcCareTakersMasterRow masterRow)
        {
            TcBanksAndBranchesRow banksAndBranchesData = banksAndBranchesTable.GetRow(paymasterRow.Bank, paymasterRow.Branch);
            if (banksAndBranchesData == null)
            {
                bool gotFromMaster = false;

                string error = string.Format("Could not find bank and branch code. Bank [{0}], Branch [{1}]", paymasterRow.Bank, paymasterRow.Branch);
                if (masterRow != null)
                {
                    if (TcValidator.IsValidBank(masterRow.Bank) &&
                        TcValidator.IsValidBranch(masterRow.Branch) &&
                        TcValidator.IsValidBankCode(masterRow.BankCode) &&
                        TcValidator.IsValidBranchCode(masterRow.BranchCode) &&
                        paymasterRow.Bank == masterRow.Bank &&
                        paymasterRow.Branch == masterRow.Branch)
                    {
                        paymasterRow.BankCode = masterRow.BankCode;
                        paymasterRow.BranchCode = masterRow.BranchCode;

                        gotFromMaster = true;
                    }
                    else
                    {
                        error += string.Format("\n\tMaster Bank: [{0}], Master Branch: [{1}], Master Bank Code: [{2}], Master Branch Code: [{3}]",
                        masterRow.Bank, masterRow.Branch, masterRow.BankCode, masterRow.BranchCode);
                    }
                }

                if (!gotFromMaster)
                {
                    if (TcValidator.IsPaymasterSupportedBank(paymasterRow.Bank))
                    {
                        paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agent_Bank_and_Branch_Code_not_Found, error);
                    }
                    else
                    {
                        error = string.Format("Bank [{0}] is not suppoted by PayMaster", masterRow.Bank);
                        paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agent_Bank_is_not_Supported_by_PayMaster, error);
                    }
                }
            }
            else
            {
                paymasterRow.BankCode      = TcString.AppendZerosToFront(banksAndBranchesData.BankCode.ToString(), 4);
                paymasterRow.BranchCode    = TcString.AppendZerosToFront(banksAndBranchesData.BranchCode.ToString(), 3);
            }
        }

        private static void CheckBankAccountNumber(TcCareTakersAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidBankAccountNumber(paymasterRow.DestinationAccount))
            {
                string error = string.Format("Invalid bank account Number [{0}]", paymasterRow.DestinationAccount);
                paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agent_Bank_Account_Number_Invalid, error);
            }
        }

        private static void CheckNICNumber(TcCareTakersAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidNIC(paymasterRow.NIC))
            {
                string error = string.Format("Invalid NIC Number [{0}]", paymasterRow.NIC);
                paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agent_NIC_Number_Invalid, error);
            }
        }

        public static void CheckBankTransferAmount(TcCareTakersAnalyzedRow paymasterRow)
        {
            if (paymasterRow.Amount < 0)
            {
                string error = string.Format("Agent Bank Transfer Amount is Negative [{0}]", paymasterRow.Amount);
                paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agent_Bank_Transfer_Amount_is_Negative, error);
            }
        }

        private void CheckEmptyNIC(TcCareTakersAnalyzedRow paymasterRow)
        {
            if (string.IsNullOrEmpty(paymasterRow.NIC))
            {
                if (!string.IsNullOrEmpty(paymasterRow.DestinationAccountName))
                {
                    string error = string.Format("NIC is empty. NIC: [{0}]", paymasterRow.NIC);
                    paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agent_NIC_Empty, error);

                    nicEmptyList.Add(paymasterRow);
                }
            }
        }

        private static void CheckNICWithMaster(TcCareTakersAnalyzedRow paymasterRow, TcCareTakersMasterRow masterRow)
        {
            if (masterRow.NIC != paymasterRow.NIC)
            {
                string error = string.Format("NIC does not match with master file. NIC: [{0}], NIC in Master: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.NIC, masterRow.NIC, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Master_NIC_not_Match_with_Agents, error);
            }
        }

        private static void CheckBankAndBranchWithMaster(TcCareTakersAnalyzedRow paymasterRow, TcCareTakersMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(masterRow.Bank) || string.IsNullOrEmpty(masterRow.Branch))
            {
                string error = string.Format("Bank or branch empty in master file.\n\tBank: [{0}], Branch: [{1}]\n\tMaster Bank: [{2}], Master Branch: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.Bank, paymasterRow.Branch, masterRow.Bank, masterRow.Branch, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Master_Bank_or_Branch_Empty, error);
            }
            else
            {
                if (paymasterRow.Bank != masterRow.Bank ||
                    paymasterRow.Branch != masterRow.Branch)
                {
                    string error = string.Format("Bank or branch does not match with master file.\n\tBank: [{0}], Branch: [{1}]\n\tMaster Bank: [{2}], Master Branch: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.Bank, paymasterRow.Branch, masterRow.Bank, masterRow.Branch, masterRow.LineNumber);
                    paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Master_Bank_or_Branch_Does_not_Match_with_Agent, error);
                }
            }
        }

        private static void CheckBankAndBranchCodeWithMaster(TcCareTakersAnalyzedRow paymasterRow, TcCareTakersMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(masterRow.BankCode) || string.IsNullOrEmpty(masterRow.BranchCode) ||
                masterRow.BankCode == "0000" || masterRow.BranchCode == "000")
            {
                string error = string.Format("Bank Code or Branch Code empty in master file.\n\tBank Code: [{0}], Branch Code: [{1}]\n\tMaster Bank Code: [{2}], Master Branch Code: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.BankCode, paymasterRow.BranchCode, masterRow.BankCode, masterRow.BranchCode, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Master_Bank_or_Branch_Code_Empty, error);
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
                        paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Master_Bank_or_Branch_Code_Does_not_Match_with_Agent, error);
                    }
                }
            }
        }

        private static void CheckAccountWithMaster(TcCareTakersAnalyzedRow paymasterRow, TcCareTakersMasterRow masterRow)
        {
            string emptyAccountNumber = TcString.AppendZerosToFront("", 12);
            if (masterRow.AccountNumber != emptyAccountNumber && paymasterRow.DestinationAccount != masterRow.AccountNumber)
            {
                string error = string.Format("Account not match with master file. Account Number: [{0}], Master Account Number: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.DestinationAccount, masterRow.AccountNumber, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeCareTakersAnalyzeFilter.Agent_Account_Number_Does_not_Match_with_Master, error);
            }
        }
    }
}
