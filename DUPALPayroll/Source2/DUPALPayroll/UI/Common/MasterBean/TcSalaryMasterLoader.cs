using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-24

namespace DUPALPayroll.UI.Common.MasterBean
{
    public abstract class TcSalaryMasterLoader<T> where T:TcSalaryMasterRow
    {
        public abstract T New();
        private TcYearMonth workingYearMonth;

        private List<string> mandatoryHeaderNames = new List<string>();

        public TcSalaryMasterLoader(TcYearMonth workingYearMonth)
        {
            this.workingYearMonth = workingYearMonth;
        }

        public void Init()
        {
            AddMandatoryHeaders();
        }

        public TcBindingList<T> LoadFromCSV(string csvFilePath)
        {
            TcBindingList<T> list = new TcBindingList<T>();

            TcCsvFile csvFile = new TcCsvFile();
            csvFile.Load(csvFilePath);

            bool headerFound = false;
            Dictionary<string, int> headerIndexes = new Dictionary<string, int>();
            foreach (TcCsvDataRow row in csvFile.Rows)
            {
                if (!headerFound)
                {
                    string startHeaderFieldName = row.Fields[0].Value.Trim().Replace(" ", "_").ToUpper();

                    if (startHeaderFieldName == "EMPLOYEE_NUMBER")
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

                T data = GetEmployeeDataFromCSVRow(row, headerIndexes);
                if (IsValidRow(data)) // remove empty rows
                {
                    list.Add(data);
                }
            }

            if (!headerFound)
            {
                throw new Exception("Invalid master data file. Header row not found. Header must start with [EMPLOYEE_NUMBER]");
            }

            return list;
        }

        protected virtual bool IsValidRow(T data)
        {
            bool isValid = data.EmployeeNumber.Length > 0 ||
                            data.NIC.Length > 0 ||
                            data.Name.Length > 0;

            return isValid;
        }

        private void CheckHeaderNames(Dictionary<string, int> headerIndexes)
        {
            TcHeaderNamesChecker checker = new TcHeaderNamesChecker();
            if (!checker.CheckHeaderNames(headerIndexes, mandatoryHeaderNames))
            {
                string error = string.Format("Invalid Master Data file\n{0}", checker.Error);
                throw new Exception(error);
            }
        }

        protected virtual void AddMandatoryHeaders()
        {
            mandatoryHeaderNames.Add("EMPLOYEE_NUMBER");
            mandatoryHeaderNames.Add("NIC");
            mandatoryHeaderNames.Add("NAME");
            mandatoryHeaderNames.Add("DATE_OF_JOIN");
            mandatoryHeaderNames.Add("ADDRESS_LINE_1");
            mandatoryHeaderNames.Add("ADDRESS_LINE_2");
            mandatoryHeaderNames.Add("CITY");
            mandatoryHeaderNames.Add("DESIGNATION");
            mandatoryHeaderNames.Add("BASIC_SALARY");
            mandatoryHeaderNames.Add("BANK");
            mandatoryHeaderNames.Add("BANK_CODE");
            mandatoryHeaderNames.Add("BRANCH");
            mandatoryHeaderNames.Add("BRANCH_CODE");
            mandatoryHeaderNames.Add("ACCOUNT");

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                mandatoryHeaderNames.Add("INITIALS");
                mandatoryHeaderNames.Add("LASTNAME");
                mandatoryHeaderNames.Add("OC_GRADE");
            }
        }

        private T GetEmployeeDataFromCSVRow(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            T data = Load(row, headerIndexes);
            data = Clean(data);

            return data;
        }

        protected virtual T Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            T data = New();

            data.LineNumber         = row.LineNumber;
            data.EmployeeNumber     = row.Fields[headerIndexes["EMPLOYEE_NUMBER"]].Value;
            data.NIC                = row.Fields[headerIndexes["NIC"]].Value;
            data.Name               = row.Fields[headerIndexes["NAME"]].Value;
            data.DateOfJoin         = TcCsvValueDecorder.GetDate(row.Fields[headerIndexes["DATE_OF_JOIN"]].Value);
            data.Designation        = row.Fields[headerIndexes["DESIGNATION"]].Value;
            data.AddressLine1       = row.Fields[headerIndexes["ADDRESS_LINE_1"]].Value;
            data.AddressLine2       = row.Fields[headerIndexes["ADDRESS_LINE_2"]].Value;
            data.City               = row.Fields[headerIndexes["CITY"]].Value;
            data.BasicSalary        = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["BASIC_SALARY"]].Value);
            data.Bank               = row.Fields[headerIndexes["BANK"]].Value;
            data.BankCode           = row.Fields[headerIndexes["BANK_CODE"]].Value;
            data.Branch             = row.Fields[headerIndexes["BRANCH"]].Value;
            data.BranchCode         = row.Fields[headerIndexes["BRANCH_CODE"]].Value;
            data.AccountNumber      = row.Fields[headerIndexes["ACCOUNT"]].Value;

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                data.Initials = row.Fields[headerIndexes["INITIALS"]].Value;
                data.LastName = row.Fields[headerIndexes["LASTNAME"]].Value;
                data.OCGrade  = row.Fields[headerIndexes["OC_GRADE"]].Value;
            }

            return data;
        }

        protected virtual T Clean(T data)
        {
            data.EmployeeNumber = TcFormatter.TrimAndUpper(data.EmployeeNumber);
            data.NIC            = TcFormatter.GetFormattedNIC(data.NIC);
            data.Name           = TcFormatter.TrimAndUpper(data.Name);
            data.AddressLine1   = TcFormatter.TrimAndUpper(data.AddressLine1);
            data.AddressLine2   = TcFormatter.TrimAndUpper(data.AddressLine2);
            data.City           = TcFormatter.TrimAndUpper(data.City);
            data.Designation    = TcFormatter.TrimAndUpper(data.Designation);
            data.Bank           = TcFormatter.TrimAndUpper(data.Bank);
            data.Branch         = TcFormatter.TrimAndUpper(data.Branch);
            data.BankCode       = TcFormatter.GetFormattedBankCode(data.BankCode);
            data.BranchCode     = TcFormatter.GetFormattedBranchCode(data.BranchCode);
            data.AccountNumber  = TcFormatter.GetFormattedBankAccountNumber(data.AccountNumber);

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                data.Initials = TcFormatter.GetFormattedInitials(data.Initials);
                data.LastName = TcFormatter.GetFormattedLastName(data.LastName);
                data.OCGrade  = TcFormatter.TrimAndUpper(data.OCGrade);
            }

            return data;
        }
    }
}
