using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.BanksAndBranches;
using DUPALPayroll.UI.Common.MasterBean;

// Harshan Nishantha
// 2013-09-24

namespace DUPALPayroll.UI.Common.AnalyzeBean
{
    public class TcValidityChecker
    {
        public static void CheckPaymasterRow(TcSalaryAnalyzedRow paymasterRow)
        {
            CheckValidEmployeeNumber(paymasterRow);

            CheckValidMemberStatus(paymasterRow);

            CheckEmployeeBankTransferAmount(paymasterRow);

            CheckAgeIsLessThan18(paymasterRow);

            CheckInvalidBankAccountNumber(paymasterRow);

            CheckInvalidNICNumber(paymasterRow);
        }

        public static void CheckPaymasterRowWithMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            LoadOtherMasterData(paymasterRow, masterRow);

            CheckEmployeeNumberWithMaster(paymasterRow, masterRow);

            CheckNICWithMaster(paymasterRow, masterRow);

            CheckBankAndBranchWithMaster(paymasterRow, masterRow);

            CheckBankAndBranchCodeWithMaster(paymasterRow, masterRow);

            CheckAccountWithMaster(paymasterRow, masterRow);

            CheckBasicSalaryWithMaster(paymasterRow, masterRow);

            CheckValidLastNameInMaster(paymasterRow, masterRow);

            CheckValidOCGradeInMaster(paymasterRow, masterRow);
        }

        private static void CheckValidEmployeeNumber(TcSalaryAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidEmployeeOrEmployerNumberAfterClean(paymasterRow.EmployeeNumber))
            {
                string error = string.Format("Employee does not have valid  Employee Number [{0}]", paymasterRow.EmployeeNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Number_Invalid, error);
            }
        }

        private static void CheckValidMemberStatus(TcSalaryAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidEpfMemberStatus(paymasterRow.MemberStatus))
            {
                string error = string.Format("Member Status [{0}] is invalid", paymasterRow.MemberStatus);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Member_Status_Invalid, error);
            }
        }

        private static void CheckEmployeeBankTransferAmount(TcSalaryAnalyzedRow paymasterRow)
        {
            if (paymasterRow.BankTransferAmount < 0)
            {
                string error = string.Format("Employee Bank Transfer Amount is Negative [{0}]", paymasterRow.BankTransferAmount);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Bank_Transfer_Amount_is_Negative, error);
            }
        }

        private static void CheckAgeIsLessThan18(TcSalaryAnalyzedRow paymasterRow)
        {
            if (paymasterRow.Age < 18)
            {
                string error = string.Format("Age is less than 18. NIC [{0}], Date of Birth [{1}], Age[{2}]",
                    paymasterRow.NIC,
                    paymasterRow.DateOfBirth.HasValue ? paymasterRow.DateOfBirth.Value.ToString("yyyy-MM-dd") : "--",
                    paymasterRow.Age);

                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Age_Less_Than_18, error);
            }
        }

        private static void CheckInvalidBankAccountNumber(TcSalaryAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidBankAccountNumber(paymasterRow.AccountNumber))
            {
                string error = string.Format("Invalid Bank Account Number [{0}]", paymasterRow.AccountNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Bank_Account_Number_Invalid, error);
            }
        }

        private static void CheckInvalidNICNumber(TcSalaryAnalyzedRow paymasterRow)
        {
            if (!TcValidator.IsValidNIC(paymasterRow.NIC))
            {
                string error = string.Format("Invalid NIC Number [{0}]", paymasterRow.NIC);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_NIC_Number_Invalid, error);
            }
        }

        private static void CheckEmployeeNumberWithMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            if (masterRow.EmployeeNumber != paymasterRow.EmployeeNumber)
            {
                string error = string.Format("Employee Number does not match with master file. Employee Number: [{0}], Employee Number in Master: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.EmployeeNumber, masterRow.EmployeeNumber, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_Employee_Number_not_Match_with_Employee, error);
            }
        }

        private static void CheckNICWithMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            if (masterRow.NIC != paymasterRow.NIC)
            {
                string error = string.Format("NIC does not match with master file. NIC: [{0}], NIC in Master: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.NIC, masterRow.NIC, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_NIC_not_Match_with_Employee, error);
            }
        }

        private static void CheckBankAndBranchWithMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(masterRow.Bank) || string.IsNullOrEmpty(masterRow.Branch))
            {
                string error = string.Format("Bank or branch empty in master file.\n\tBank: [{0}], Branch: [{1}]\n\tMaster Bank: [{2}], Master Branch: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.Bank, paymasterRow.Branch, masterRow.Bank, masterRow.Branch, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_Bank_or_Branch_Empty, error);
            }
            else
            {
                if (paymasterRow.Bank != masterRow.Bank ||
                    paymasterRow.Branch != masterRow.Branch)
                {
                    string error = string.Format("Bank or branch does not match with master file.\n\tBank: [{0}], Branch: [{1}]\n\tMaster Bank: [{2}], Master Branch: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.Bank, paymasterRow.Branch, masterRow.Bank, masterRow.Branch, masterRow.LineNumber);
                    paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_Bank_or_Branch_Does_not_Match_with_Employee, error);
                }
            }
        }

        private static void CheckBankAndBranchCodeWithMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(masterRow.BankCode) || string.IsNullOrEmpty(masterRow.BranchCode) ||
                masterRow.BankCode == "0000" || masterRow.BranchCode == "000")
            {
                string error = string.Format("Bank Code or Branch Code empty in master file.\n\tBank Code: [{0}], Branch Code: [{1}]\n\tMaster Bank Code: [{2}], Master Branch Code: [{3}]. Master Line Number: [{4}]",
                    paymasterRow.BankCode, paymasterRow.BranchCode, masterRow.BankCode, masterRow.BranchCode, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_Bank_or_Branch_Code_Empty, error);
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
                        paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_Bank_or_Branch_Code_Does_not_Match_with_Employee, error);
                    }
                }
            }
        }

        private static void CheckAccountWithMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            string emptyAccountNumber = TcString.AppendZerosToFront("", 12);
            if (masterRow.AccountNumber != emptyAccountNumber && paymasterRow.AccountNumber != masterRow.AccountNumber)
            {
                string error = string.Format("Account not match with master file. Account Number: [{0}], Master Account Number: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.AccountNumber, masterRow.AccountNumber, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Account_Number_Does_not_Match_with_Master, error);
            }
        }

        private static void CheckBasicSalaryWithMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            if (paymasterRow.BasicSalary != masterRow.BasicSalary)
            {
                string error = string.Format("Basic Salary not match with master file. Basic Salary: [{0}], Master Basic Salary: [{1}]. Master Line Number: [{2}]",
                    paymasterRow.BasicSalary, masterRow.BasicSalary, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_Basic_Salary_Does_not_Match_with_Employee, error);
            }
        }

        public static void LoadBanksAndBranchesData(TcBanksAndBranchesTable banksAndBranchesTable, TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
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
                        paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Bank_and_Branch_Code_not_Found, error);
                    }
                    else
                    {
                        error = string.Format("Bank [{0}] is not suppoted by PayMaster", masterRow.Bank);
                        paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Bank_is_not_Supported_by_PayMaster, error);
                    }
                }
            }
            else
            {
                paymasterRow.BankCode = TcString.AppendZerosToFront(banksAndBranchesData.BankCode.ToString(), 4);
                paymasterRow.BranchCode = TcString.AppendZerosToFront(banksAndBranchesData.BranchCode.ToString(), 3);
            }
        }

        private static void LoadOtherMasterData(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            paymasterRow.Initials   = masterRow.Initials;
            paymasterRow.LastName   = masterRow.LastName;
            paymasterRow.OCGrade    = masterRow.OCGrade;
        }

        private static void CheckValidLastNameInMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            if (string.IsNullOrEmpty(paymasterRow.LastName))
            {
                string error = string.Format("LastName [{0}] is invalid in master file. Master Line Number: [{1}]", 
                    masterRow.LastName, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_LastName_is_Invalid, error);
            }
        }

        private static void CheckValidOCGradeInMaster(TcSalaryAnalyzedRow paymasterRow, TcSalaryMasterRow masterRow)
        {
            if (!TcValidator.IsValidOCGrade(paymasterRow.OCGrade))
            {
                string error = string.Format("OC Grade [{0}] is invalid in master file. Master Line Number: [{1}]", 
                    masterRow.OCGrade, masterRow.LineNumber);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Master_OC_Grade_is_Invalid, error);
            }
        }
    }
}
