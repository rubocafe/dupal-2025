using Payroll.General;
using Payroll.Library;
using Payroll.Library.General;
using Payroll.Library.MetaData;
using Payroll.UI.Business.MasterData;
using Payroll.UI.Business.Salary;
using Payroll.UI.Common.BanksAndBranches;
using Payroll.UI.Controls;
using System;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.UI.Business.Analyze
{
    public class TcBusinessAnalyzer
    {
        private TcBindingList<TcBusinessAnalyzedRow> nicEmptyList = new TcBindingList<TcBusinessAnalyzedRow>();

        public TcBindingList<TcBusinessAnalyzedRow> Analyze(TcBusinessForm master, TcMetaData metaData)
        {
            nicEmptyList.Clear();

            TcBindingList<TcBusinessAnalyzedRow> list = new TcBindingList<TcBusinessAnalyzedRow>();

            TcBusinessMasterEngine   masterTable            = master.MasterForm.Engine;
            TcBanksAndBranchesEngine banksAndBranchesTable  = master.BanksAndBranchesForm.Engine;
            TcBusinessSalaryEngine   salaryTable            = master.SalaryForm.Engine; 

            DateTime dobBoundryDate = new DateTime(master.SettingsForm.WorkingYearMonth.Year, master.SettingsForm.WorkingYearMonth.Month, 1);

            foreach (TcBusinessSalaryRow salaryRow in salaryTable.All)
            {
                TcBusinessAnalyzedRow paymasterRow = GetNewPayMasterData(salaryRow, dobBoundryDate);

                CheckValidEmployeeNumber(paymasterRow);

                CheckValidMemberStatus(paymasterRow);

                CheckAge(paymasterRow);
                
                CheckBankAccountNumber(paymasterRow);

                CheckNICNumber(paymasterRow);

                CheckEmptyNIC(paymasterRow);

                CheckBankTransferAmount(paymasterRow);

                TcBusinessMasterRow masterRow = masterTable.GetRowWithNIC(paymasterRow.NIC);
                LoadBanksAndBranchesData(banksAndBranchesTable, paymasterRow, masterRow);
                
                if (masterRow == null)
                {
                    string error = string.Format("Row is not found in master file. NIC: [{0}]", paymasterRow.NIC);
                    paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_not_found_in_Master, error);
                }
                else
                {
                    LoadOtherMasterData(paymasterRow, masterRow);

                    CheckEmployeeNumberWithMaster(paymasterRow, masterRow);

                    CheckNICWithMaster(paymasterRow, masterRow);

                    CheckBankAndBranchWithMaster(paymasterRow, masterRow);

                    CheckBankAndBranchCodeWithMaster(paymasterRow, masterRow);

                    CheckAccountWithMaster(paymasterRow, masterRow);

                    CheckValidLastNameInMaster(paymasterRow, masterRow);

                    CheckValidOCGradeInMaster(paymasterRow, masterRow);

                    CheckEPFDeduction(paymasterRow, metaData.Settings.EPFDeductionPercentage);

                    CheckEPFContribution(paymasterRow, metaData.Settings.EPFContributionPercentage);

                    CheckETFContribution(paymasterRow, metaData.Settings.ETFContributionPercentage);

                    paymasterRow.DuplicateMasterRows = masterTable.GetNICDuplicates(paymasterRow.NIC);
                    if (paymasterRow.DuplicateMasterRows.Count > 0)
                    {
                        string lineNumbers = "";
                        foreach (TcBusinessMasterRow tempMasterRow in paymasterRow.DuplicateMasterRows)
                        {
                            lineNumbers += ("[" + tempMasterRow.LineNumber + "] ");
                        }

                        string error = string.Format("Duplicate rows [{0}] found in master file." +
                            "\n\tLine Numbers: {1}", paymasterRow.DuplicateMasterRows.Count, lineNumbers);
                        paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Duplicate_Rows_in_Master_File_for_Employees, error);
                    }
                }

                list.Add(paymasterRow);
            }

            return list;
        }

        private static void CheckValidEmployeeNumber(TcBusinessAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidEmployeeOrEmployerNumberAfterClean(paymasterRow.EmployeeNumber))
            {
                string error = string.Format("Employee Number [{0}] is invalid", paymasterRow.EmployeeNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_Number_Invalid, error);
            }
        }

        private static void CheckValidMemberStatus(TcBusinessAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidEpfMemberStatus(paymasterRow.MemberStatus))
            {
                string error = string.Format("Member Status [{0}] is invalid", paymasterRow.MemberStatus);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_Member_Status_Invalid, error);
            }
        }

        private static void CheckAge(TcBusinessAnalyzedRow paymasterRow)
        {
            if (paymasterRow.Age < 16)
            {
                string error = string.Format("Age is less than 16. NIC [{0}], Date of Birth [{1}], Age[{2}]", 
                    paymasterRow.NIC, 
                    paymasterRow.DateOfBirth.HasValue ? paymasterRow.DateOfBirth.Value.ToString("yyyy-MM-dd") : "--", 
                    paymasterRow.Age);

                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_Age_Less_Than_16, error);
            }
        }

        private static TcBusinessAnalyzedRow GetNewPayMasterData(TcBusinessSalaryRow data, DateTime dobBoundryDate)
        {
            TcBusinessAnalyzedRow paymasterRow = new TcBusinessAnalyzedRow();

            paymasterRow.LineNumber         = data.LineNumber;
            paymasterRow.EmployeeNumber     = data.EmployeeNumber;
            paymasterRow.Bank               = data.Bank;
            paymasterRow.Branch             = data.Branch;
            paymasterRow.AccountNumber      = data.AccountNumber;
            paymasterRow.NameWithInitials   = data.NameWithInitials;
            paymasterRow.NIC                = data.NIC;
            paymasterRow.BankCode           = string.Empty;
            paymasterRow.BranchCode         = string.Empty;

            paymasterRow.Designation        = data.Designation;
            paymasterRow.DateOfJoin         = data.DateOfJoin;
            paymasterRow.AddressLine1       = data.AddressLine1;
            paymasterRow.AddressLine2       = data.AddressLine2;
            paymasterRow.City               = data.City;

            paymasterRow.BasicSalary        = data.BasicSalary;
            paymasterRow.GrossSalary        = data.GrossSalary;
            paymasterRow.EpfDeduction       = data.EpfDeduction;
            paymasterRow.NetSalary          = data.NetSalary;
            paymasterRow.EpfContribution    = data.EpfContribution;
            paymasterRow.EtfContribution    = data.EtfContribution;
            paymasterRow.GrossSalary        = data.GrossSalary;

            paymasterRow.TotalRemuneration  = data.TotalRemuneration;
            paymasterRow.BankTransferAmount = data.BankTransferAmount;

            paymasterRow.MemberStatus   = data.MemberStatus;
            paymasterRow.DaysWorked     = data.DaysWorked;

            paymasterRow.DOBBoundaryDate = dobBoundryDate;

            paymasterRow.Allowances = data.Allowances;
            paymasterRow.NoPays     = data.NoPays;
            paymasterRow.Deductions = data.Deductions;
            paymasterRow.Incentives = data.Incentives;

            return paymasterRow;
        }

        private static void LoadBanksAndBranchesData(TcBanksAndBranchesEngine banksAndBranchesTable, 
            TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            TcBanksAndBranchesRow banksAndBranchesData = banksAndBranchesTable.GetRow(paymasterRow.Bank, paymasterRow.Branch);
            if (banksAndBranchesData == null)
            {
                bool gotFromMaster = false;

                string error = string.Format("Could not find bank and branch code. Bank [{0}], Branch [{1}]", 
                    paymasterRow.Bank, paymasterRow.Branch);
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
                        paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found, error);
                    }
                    else
                    {
                        error = string.Format("Bank [{0}] is not suppoted by PayMaster", masterRow.Bank);
                        paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster, error);
                    }
                }
            }
            else
            {
                paymasterRow.BankCode      = TcString.AppendZerosToFront(banksAndBranchesData.BankCode.ToString(), 4);
                paymasterRow.BranchCode    = TcString.AppendZerosToFront(banksAndBranchesData.BranchCode.ToString(), 3);
            }
        }

        public static void LoadOtherMasterData(TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            paymasterRow.Initials = masterRow.Initials;
            paymasterRow.LastName = masterRow.LastName;
            paymasterRow.OCGrade  = masterRow.OCGrade;
        }

        private static void CheckBankAccountNumber(TcBusinessAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidBankAccountNumber(paymasterRow.AccountNumber))
            {
                string error = string.Format("Invalid Bank Account Number [{0}]", paymasterRow.AccountNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_Bank_Account_Number_Invalid, error);
            }
        }

        private static void CheckNICNumber(TcBusinessAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidNIC(paymasterRow.NIC))
            {
                string error = string.Format("Invalid NIC Number [{0}]", paymasterRow.NIC);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_NIC_Number_Invalid, error);
            }
        }

        private void CheckEmptyNIC(TcBusinessAnalyzedRow paymasterRow)
        {
            if (string.IsNullOrEmpty(paymasterRow.NIC))
            {
                if (!string.IsNullOrEmpty(paymasterRow.NIC))
                {
                    string error = string.Format("NIC is empty. NIC: [{0}]", paymasterRow.NIC);
                    paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_NIC_Number_is_Empty, error);

                    nicEmptyList.Add(paymasterRow);
                }
            }
        }

        public static void CheckBankTransferAmount(TcBusinessAnalyzedRow paymasterRow)
        {
            if (paymasterRow.BankTransferAmount < 0)
            {
                string error = string.Format("Agent Bank Transfer Amount is Negative [{0}]", paymasterRow.BankTransferAmount);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_Bank_Transfer_Amount_is_Negative, error);
            }
        }

        public static void CheckEPFDeduction(TcBusinessAnalyzedRow paymasterRow, decimal epfDeductionPercentage)
        {
            var expected = (paymasterRow.GrossSalary * epfDeductionPercentage) / 100;
            if (!TcDecimal.EqualFor2DecimalPoints(paymasterRow.EpfDeduction ,expected))
            {
                string error = string.Format("Invalid EPF Deduction Amount. Actual: [{0}], Expected: [{1}]", 
                    paymasterRow.EpfDeduction, expected);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_EPF_Deduction_Invalid, error);
            }
        }

        public static void CheckEPFContribution(TcBusinessAnalyzedRow paymasterRow, decimal epfContributionPercentage)
        {
            var expected = (paymasterRow.GrossSalary * epfContributionPercentage) / 100;
            if (!TcDecimal.EqualFor2DecimalPoints(paymasterRow.EpfContribution ,expected))
            {
                string error = string.Format("Invalid EPF Contribution Amount. Actual: [{0}], Expected: [{1}]",
                    paymasterRow.EpfContribution, expected);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_EPF_Contribution_Invalid, error);
            }
        }

        public static void CheckETFContribution(TcBusinessAnalyzedRow paymasterRow, decimal etfContributionPercentage)
        {
            var expected = (paymasterRow.GrossSalary * etfContributionPercentage) / 100;
            if (!TcDecimal.EqualFor2DecimalPoints(paymasterRow.EtfContribution ,expected))
            {
                string error = string.Format("Invalid ETF Contribution Amount. Actual: [{0}], Expected: [{1}]",
                    paymasterRow.EtfContribution, expected);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_ETF_Contribution_Invalid, error);
            }
        }

        private static void CheckEmployeeNumberWithMaster(TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            if (masterRow.EmployeeNumber != paymasterRow.EmployeeNumber)
            {
                string error = string.Format("Employee number does not match with master file. " +
                    "Employee Number: [{0}], Employee Number in Master: [{1}], Master Index: [{2}]",
                    paymasterRow.EmployeeNumber, masterRow.EmployeeNumber, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Master_Employee_Number_not_Match, error);
            }
        }

        private static void CheckNICWithMaster(TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            if (masterRow.NIC != paymasterRow.NIC)
            {
                string error = string.Format("NIC does not match with master file. NIC: [{0}], "+
                    "NIC in Master: [{1}]. Master Index: [{2}]",
                    paymasterRow.NIC, masterRow.NIC, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Master_NIC_not_Match_with_Employee, error);
            }
        }

        private static void CheckBankAndBranchWithMaster(TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(masterRow.Bank) || string.IsNullOrEmpty(masterRow.Branch))
            {
                string error = string.Format("Bank or branch empty in master file.\n\tBank: [{0}], "+
                    "Branch: [{1}]\n\tMaster Bank: [{2}], Master Branch: [{3}]. Master Index: [{4}]",
                    paymasterRow.Bank, paymasterRow.Branch, masterRow.Bank, masterRow.Branch, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Master_Bank_or_Branch_Empty, error);
            }
            else
            {
                if (paymasterRow.Bank != masterRow.Bank ||
                    paymasterRow.Branch != masterRow.Branch)
                {
                    string error = string.Format("Bank or branch does not match with master file." +
                        "\n\tBank: [{0}], Branch: [{1}]\n\tMaster Bank: [{2}], Master Branch: [{3}]. Master Index: [{4}]",
                    paymasterRow.Bank, paymasterRow.Branch, masterRow.Bank, masterRow.Branch, masterRow.LineNumber);
                    paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Master_Bank_or_Branch_Does_not_Match_with_Employee, error);
                }
            }
        }

        private static void CheckBankAndBranchCodeWithMaster(TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(masterRow.BankCode) || string.IsNullOrEmpty(masterRow.BranchCode) ||
                masterRow.BankCode == "0000" || masterRow.BranchCode == "000")
            {
                string error = string.Format("Bank Code or Branch Code empty in master file." +
                    "\n\tBank Code: [{0}], Branch Code: [{1}]\n\tMaster Bank Code: [{2}], Master Branch Code: [{3}]. Master Index: [{4}]",
                    paymasterRow.BankCode, paymasterRow.BranchCode, masterRow.BankCode, masterRow.BranchCode, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Master_Bank_or_Branch_Code_Empty, error);
            }
            else
            {
                if (!string.IsNullOrEmpty(paymasterRow.BankCode) && !string.IsNullOrEmpty(paymasterRow.BranchCode))
                {
                    if (paymasterRow.BankCode != masterRow.BankCode ||
                        paymasterRow.BranchCode != masterRow.BranchCode)
                    {
                        string error = string.Format("Bank Code or Branch Code does not match with master file." +
                            "\n\tBank Code: [{0}], Branch Code: [{1}]\n\tMaster Bank Code: [{2}], " +
                            "Master Branch Code: [{3}]. Master Index: [{4}]",
                        paymasterRow.BankCode, paymasterRow.BranchCode, masterRow.BankCode, masterRow.BranchCode, masterRow.LineNumber);
                        paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Master_Bank_or_Branch_Code_Does_not_Match_with_Employee, error);
                    }
                }
            }
        }

        private static void CheckAccountWithMaster(TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            string emptyAccountNumber = TcString.AppendZerosToFront("", 12);
            if (masterRow.AccountNumber != emptyAccountNumber && paymasterRow.AccountNumber != masterRow.AccountNumber)
            {
                string error = string.Format("Account not match with master file. Account Number: [{0}], " +
                    "Master Account Number: [{1}]. Master Index: [{2}]",
                    paymasterRow.AccountNumber, masterRow.AccountNumber, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Employee_Account_Number_Does_not_Match_with_Master, error);
            }
        }

        private static void CheckValidLastNameInMaster(TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(paymasterRow.LastName))
            {
                string error = string.Format("LastName [{0}] is invalid in master file. Master Index: [{1}]", 
                    masterRow.LastName, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Master_LastName_is_Invalid, error);
            }
        }

        private static void CheckValidOCGradeInMaster(TcBusinessAnalyzedRow paymasterRow, TcBusinessMasterRow masterRow)
        {
            if (!TcValidator.IsValidOCGrade(paymasterRow.OCGrade))
            {
                string error = string.Format("OC Grade [{0}] is invalid in master file. Master Index: [{1}]",
                    masterRow.OCGrade, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeBusinessAnalyzeFilter.Master_OC_Grade_is_Invalid, error);
            }
        }
    }
}
