using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.MasterData
{
    public class TcCommissionAgentsMasterDataLoader
    {
        private TcYearMonth workingYearMonth;

        public TcCommissionAgentsMasterDataLoader(TcYearMonth workingYearMonth)
        {
            this.workingYearMonth = workingYearMonth;
        }

        public TcBindingList<TcCommissionAgentsMasterRow> LoadFromCSV(string csvFilePath)
        {
            TcBindingList<TcCommissionAgentsMasterRow> list = new TcBindingList<TcCommissionAgentsMasterRow>();

            TcCsvFile csvFile = new TcCsvFile();
            csvFile.Load(csvFilePath);

            bool headerFound = false;
            Dictionary<string, int> headerIndexes = new Dictionary<string, int>();
            foreach (TcCsvDataRow row in csvFile.Rows)
            {
                if (!headerFound)
                {
                    if (row.Fields[1].Value.Trim() == "T")
                    {
                        int headerIndex = 0;
                        foreach (TcCsvDataField feild in row.Fields)
                        {
                            string headerName = feild.Value.Trim().Replace(" ", "_").ToUpper();
                            if (!headerIndexes.ContainsKey(headerName))
                            {
                                headerIndexes.Add(headerName, headerIndex);
                            }
                            headerIndex++;
                        }

                        headerFound = true;
                        CheckHeaderNames(headerIndexes);
                    }

                    continue; // Skip upto and including header row
                }

                TcCommissionAgentsMasterRow data = GetEmployeeDataFromCSVRow(row, headerIndexes);
                if (IsValidRow(data)) // remove empty rows
                {
                    list.Add(data);
                }
            }

            if (!headerFound)
            {
                throw new Exception("Invalid master data file. Header row not found");
            }

            return list;
        }

        private bool IsValidRow(TcCommissionAgentsMasterRow data)
        {
            bool isValid = data.VirtualNumber.Length > 0 ||
                            data.NIC.Length > 0 ||
                            data.NameWithInitials.Length > 0;

            return isValid;
        }

        private void CheckHeaderNames(Dictionary<string, int> headerIndexes)
        {
            List<string> mandatoryHeaderNames = new List<string>();

            mandatoryHeaderNames.Add("NAME_WITH_INITIALS");
            mandatoryHeaderNames.Add("NIC");
            mandatoryHeaderNames.Add("VIRTUAL_CODE");
            mandatoryHeaderNames.Add("BANK");
            mandatoryHeaderNames.Add("BRANCH");
            mandatoryHeaderNames.Add("ACCOUNT_NO");
            mandatoryHeaderNames.Add("D_OF_JOIN");
            mandatoryHeaderNames.Add("PERMANENT_ADDRESS");
            mandatoryHeaderNames.Add("BANK_CODES");
            mandatoryHeaderNames.Add("BANK_CODE");
            mandatoryHeaderNames.Add("BRANCH_CODE");

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                mandatoryHeaderNames.Add("INITIALS");
                mandatoryHeaderNames.Add("LASTNAME");
                mandatoryHeaderNames.Add("EMPLOYEE_NUMBER");
                mandatoryHeaderNames.Add("OC_GRADE");
            }

            TcHeaderNamesChecker checker = new TcHeaderNamesChecker();
            if (!checker.CheckHeaderNames(headerIndexes, mandatoryHeaderNames))
            {
                string error = string.Format("Invalid Master Data file\n{0}", checker.Error);
                throw new Exception(error);
            }
        }

        private TcCommissionAgentsMasterRow GetEmployeeDataFromCSVRow(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcCommissionAgentsMasterRow data = new TcCommissionAgentsMasterRow();

            data.LineNumber         = row.LineNumber;
            data.Index              = TcCsvValueDecorder.GetInt(row.Fields[0].Value);
            data.NameWithInitials   = row.Fields[headerIndexes["NAME_WITH_INITIALS"]].Value;
            data.NIC                = row.Fields[headerIndexes["NIC"]].Value;
            data.VirtualNumber      = row.Fields[headerIndexes["VIRTUAL_CODE"]].Value;
            data.Bank               = row.Fields[headerIndexes["BANK"]].Value;
            data.Branch             = row.Fields[headerIndexes["BRANCH"]].Value;
            data.AccountNumber      = row.Fields[headerIndexes["ACCOUNT_NO"]].Value;
            data.DateOfJoin         = TcCsvValueDecorder.GetDate(row.Fields[headerIndexes["D_OF_JOIN"]].Value);
            data.Address            = row.Fields[headerIndexes["PERMANENT_ADDRESS"]].Value;
            data.BankAcronym        = row.Fields[headerIndexes["BANK_CODES"]].Value;
            data.BankCode           = row.Fields[headerIndexes["BANK_CODE"]].Value;
            data.BranchCode         = row.Fields[headerIndexes["BRANCH_CODE"]].Value;

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                data.Initials = row.Fields[headerIndexes["INITIALS"]].Value;
                data.LastName = row.Fields[headerIndexes["LASTNAME"]].Value;
                data.EmployeeNumber = row.Fields[headerIndexes["EMPLOYEE_NUMBER"]].Value;
                data.OCGrade    = row.Fields[headerIndexes["OC_GRADE"]].Value;
            }

            data = Clean(data);

            return data;
        }

        private TcCommissionAgentsMasterRow Clean(TcCommissionAgentsMasterRow data)
        {
            data.NameWithInitials   = TcFormatter.TrimAndUpper(data.NameWithInitials);
            data.NIC                = TcFormatter.GetFormattedNIC(data.NIC);
            data.VirtualNumber      = TcFormatter.TrimAndUpper(data.VirtualNumber);
            data.Bank               = TcFormatter.TrimAndUpper(data.Bank);
            data.Branch             = TcFormatter.TrimAndUpper(data.Branch);
            data.AccountNumber      = TcFormatter.GetFormattedBankAccountNumber(data.AccountNumber);
            data.Address            = TcFormatter.TrimAndUpper(data.Address);
            data.BankAcronym        = TcFormatter.TrimAndUpper(data.BankAcronym);
            data.BankCode           = TcFormatter.GetFormattedBankCode(data.BankCode);
            data.BranchCode         = TcFormatter.GetFormattedBranchCode(data.BranchCode);

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                data.Initials   = TcFormatter.GetFormattedInitials(data.Initials);
                data.LastName   = TcFormatter.GetFormattedLastName(data.LastName);
                data.EmployeeNumber = TcFormatter.TrimAndUpper(data.EmployeeNumber);
                data.OCGrade    = TcFormatter.TrimAndUpper(data.OCGrade);
            }

            return data;
        }
    }
}
