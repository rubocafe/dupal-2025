using DUPALPayroll.Library;
using DUPALPayroll.UI.CallCenterOutbound.MasterData;
using DUPALPayroll.UI.CallCenterOutbound.Salary;
using DUPALPayroll.UI.Common.AnalyzeBean;
using DUPALPayroll.UI.Common.BanksAndBranches;
using System;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterOutbound.Analyze
{
    public class TcCallCenterOutboundAnalyzer
    {
        private TcBindingList<TcCallCenterOutboundAnalyzedRow> enAndNICEmptyList = new TcBindingList<TcCallCenterOutboundAnalyzedRow>();

        public TcBindingList<TcCallCenterOutboundAnalyzedRow> Analyze(TcCallCenterOutboundForm master)
        {
            enAndNICEmptyList.Clear();

            TcBindingList<TcCallCenterOutboundAnalyzedRow> list = new TcBindingList<TcCallCenterOutboundAnalyzedRow>();

            TcCallCenterOutboundMasterTable  masterTable             = master.MasterForm.MasterTable;
            TcBanksAndBranchesTable         banksAndBranchesTable   = master.BanksAndBranchesForm.BanksAndBranchesTable;
            TcCallCenterOutboundSalaryTable  salaryTable             = master.SalaryForm.SalaryTable;

            DateTime dobBoundryDate = new DateTime(master.SettingsForm.WorkingYearMonth.Year, master.SettingsForm.WorkingYearMonth.Month, 1);

            foreach (TcCallCenterOutboundSalaryRow row in salaryTable.All)
            {
                TcCallCenterOutboundAnalyzedRow paymasterRow = GetNewPayMasterData(row, dobBoundryDate);

                TcValidityChecker.CheckPaymasterRow(paymasterRow);

                CheckEmptyENandNIC(paymasterRow);

                TcCallCenterOutboundMasterRow masterRow = masterTable.GetRow(paymasterRow.EmployeeNumber, paymasterRow.NIC);
                TcValidityChecker.LoadBanksAndBranchesData(banksAndBranchesTable, paymasterRow, masterRow);
                if (masterRow == null)
                {
                    string error = string.Format("Row is not found in master file. Employee Number: [{0}], NIC: [{1}]", paymasterRow.EmployeeNumber, paymasterRow.NIC);
                    paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_not_found_in_Master, error);
                }
                else
                {
                    TcValidityChecker.CheckPaymasterRowWithMaster(paymasterRow, masterRow);

                    CheckMasterDuplicateRows(masterTable, paymasterRow);
                }

                list.Add(paymasterRow);
            }

            return list;
        }

        private void CheckMasterDuplicateRows(TcCallCenterOutboundMasterTable masterTable, TcCallCenterOutboundAnalyzedRow paymasterRow)
        {
            paymasterRow.DuplicateMasterRows = masterTable.GetSalaryRowDuplicates(paymasterRow.EmployeeNumber, paymasterRow.NIC);
            if (paymasterRow.DuplicateMasterRows.Count > 0)
            {
                string lineNumbers = "";
                foreach (TcCallCenterOutboundMasterRow tempMasterRow in paymasterRow.DuplicateMasterRows)
                {
                    lineNumbers += ("[" + tempMasterRow.LineNumber + "] ");
                }

                string error = string.Format("Duplicate rows [{0}] found in master file.\n\tLine Numbers: {1}", paymasterRow.DuplicateMasterRows.Count, lineNumbers);
                paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Duplicate_Rows_in_Master_File_for_Employees, error);
            }
        }

        private void CheckEmptyENandNIC(TcCallCenterOutboundAnalyzedRow paymasterRow)
        {
            if (string.IsNullOrEmpty(paymasterRow.EmployeeNumber) && string.IsNullOrEmpty(paymasterRow.NIC))
            {
                if (!string.IsNullOrEmpty(paymasterRow.Name))
                {
                    string error = string.Format("Employee Number and NIC is empty. Employee Number: [{0}], NIC: [{1}]", paymasterRow.EmployeeNumber, paymasterRow.NIC);
                    paymasterRow.Errors.Add(TeEmployeeAnalyzeFilter.Employee_Number_and_NIC_Empty, error);

                    enAndNICEmptyList.Add(paymasterRow);
                }
            }
        }

        private TcCallCenterOutboundAnalyzedRow GetNewPayMasterData(TcCallCenterOutboundSalaryRow data, DateTime dobBoundryDate)
        {
            TcCallCenterOutboundAnalyzedRow paymasterRow = new TcCallCenterOutboundAnalyzedRow();

            paymasterRow.LineNumber                 = data.LineNumber;
            paymasterRow.EmployeeNumber             = data.EmployeeNumber;
            paymasterRow.Bank                       = data.Bank;
            paymasterRow.Branch                     = data.Branch;
            paymasterRow.AccountNumber              = data.AccountNumber;
            paymasterRow.Name                       = data.Name;
            paymasterRow.NIC                        = data.NIC;
            paymasterRow.BankCode                   = string.Empty;
            paymasterRow.BranchCode                 = string.Empty;

            paymasterRow.Designation                = data.Designation;
            paymasterRow.DateOfJoin                 = data.DateOfJoin;
            paymasterRow.AddressLine1               = data.AddressLine1;
            paymasterRow.AddressLine2               = data.AddressLine2;
            paymasterRow.City                       = data.City;

            paymasterRow.BasicSalary                = data.BasicSalary;
            paymasterRow.BRA                        = data.BRA;
            paymasterRow.OTNormal                   = data.OTNormal;
            paymasterRow.OTDouble                   = data.OTDouble;
            paymasterRow.NoPay                      = data.NoPay;
            paymasterRow.GrossSalary                = data.GrossSalary;
            paymasterRow.EPFDeduction               = data.EPFDeduction;
            paymasterRow.NetSalary                  = data.NetSalary;
            paymasterRow.EPFContribution            = data.EPFContribution;
            paymasterRow.ETFContribution            = data.ETFContribution;
            paymasterRow.Paye                       = data.Paye;

            paymasterRow.AttendanceIncentive        = data.AttendanceIncentive;
            paymasterRow.PBI                        = data.PBI;
            paymasterRow.UpsellingAndEBillingIncentive = data.UpsellingAndEBillingIncentive;
            paymasterRow.TotalRemuneration          = data.TotalRemuneration;
            paymasterRow.Payment                    = data.Payment;
            paymasterRow.Hold                       = data.Hold;
            paymasterRow.BankTransferAmount         = data.BankTransferAmount;

            paymasterRow.MemberStatus           = data.MemberStatus;
            paymasterRow.DaysWorked             = data.DaysWorked;

            paymasterRow.DOBBoundryDate = dobBoundryDate;

            return paymasterRow;
        }
    }
}
