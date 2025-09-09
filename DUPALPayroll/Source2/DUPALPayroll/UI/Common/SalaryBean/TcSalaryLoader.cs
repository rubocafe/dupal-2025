using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-24

namespace DUPALPayroll.UI.Common.SalaryBean
{
    public abstract class TcSalaryLoader<T> where T : TcSalaryRow
    {
        public string BasicSalaryHeader = "BASIC_SALARY";
        public string GrossSalaryHeader = "GROSS_SALARY";
        public string NetSalaryHeader   = "NET_SALARY";

        public abstract T New();

        protected List<string> mandatoryHeaderNames = new List<string>();
        protected Dictionary<string, string> currencyHeaderNames = new Dictionary<string, string>();

        public string FileIdentifier { get; set; }
        protected TcYearMonth workingYearMonth;

        private bool epfEtfSupported { get; set; }
        private bool payeSupported { get; set; }

        public TcSalaryLoader(string fileIdentifier, TcYearMonth workingYearMonth)
        {
            FileIdentifier = fileIdentifier;
            this.workingYearMonth = workingYearMonth;

            epfEtfSupported = TcVersions.IsEpfEtfSupported(workingYearMonth);
            payeSupported   = TcVersions.IsPayeSupported(workingYearMonth);
        }

        public void Init()
        {
            AddMandatoryHeaders();
            LoadCurrencyHeaderNames();
        }

        private void LoadCurrencyHeaderNames()
        {
            AddCurrencyField("BASIC_SALARY");
            AddCurrencyField("BASIC_REMUNERATION");
            AddCurrencyField("BRA");
            AddCurrencyField("OT_NORMAL");
            AddCurrencyField("OT_DOUBLE");
            AddCurrencyField("NO_PAY");
            AddCurrencyField("GROSS_SALARY");
            AddCurrencyField("GROSS_REMUNERATION");
            AddCurrencyField("EPF_DEDUCTION");
            AddCurrencyField("NET_SALARY");
            AddCurrencyField("NET_REMUNERATION");
            AddCurrencyField("TOTAL_REMUNERATION");
            AddCurrencyField("ATTENDANCE_INCENTIVE");
            AddCurrencyField("UPSELLING_INCENTIVE");
            AddCurrencyField("EBILLING_INCENTIVE");
            AddCurrencyField("TBI");
            AddCurrencyField("PBI");
            AddCurrencyField("SALES_COMMISSIONS");
            AddCurrencyField("COMMISSION_ADVANCE");
            AddCurrencyField("HOLD");
            AddCurrencyField("BANK_TRANSFER_AMOUNT");
            AddCurrencyField("EPF_CONTRIBUTION");
            AddCurrencyField("ETF_CONTRIBUTION");
            AddCurrencyField("PAYE");
        }

        private void AddCurrencyField(string feildId)
        {
            currencyHeaderNames.Add(feildId, feildId);
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
                    CheckCurrencyFields(row, headerIndexes);
                    list.Add(data);
                }
            }

            if (!headerFound)
            {
                string error = string.Format("Invalid {0} Salary file. Header row not found\nHeader row must start with \"EMPLOYEE_NUMBER\"", FileIdentifier);
                throw new Exception(error);
            }

            return list;
        }

        private void CheckCurrencyFields(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            bool errorFound = false;
            string error = string.Format("{0} Salary file: Currency fields must have exactly 2 decimal places\r\n", FileIdentifier);

            foreach (string field in currencyHeaderNames.Keys)
            {
                if (headerIndexes.ContainsKey(field))
                {
                    string value = row.Fields[headerIndexes[field]].Value;
                    if (!TcString.IsValidFormttedCurrencyString(value, 2))
                    {
                        error += string.Format("\r\nField: [{0}] Value: [{1}] in Line Number: [{2}]", field, value, row.LineNumber);
                        errorFound = true;
                    }
                }
            }

            if (errorFound)
            {
                throw new Exception(error);
            }
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
                string error = string.Format("Invalid {0} Salary file\n{1}", FileIdentifier, checker.Error);
                throw new Exception(error);
            }
        }

        private T GetEmployeeDataFromCSVRow(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            T data = Load(row, headerIndexes);
            data = Clean(data);

            return data;
        }

        protected virtual void AddMandatoryHeaders()
        {
            mandatoryHeaderNames.Clear();

            mandatoryHeaderNames.Add("NAME");
            mandatoryHeaderNames.Add("NIC");
            mandatoryHeaderNames.Add("EMPLOYEE_NUMBER");
            mandatoryHeaderNames.Add("DESIGNATION");
            mandatoryHeaderNames.Add("DATE_OF_JOIN");
            mandatoryHeaderNames.Add("ADDRESS_LINE_1");
            mandatoryHeaderNames.Add("ADDRESS_LINE_2");
            mandatoryHeaderNames.Add("CITY");
            mandatoryHeaderNames.Add(BasicSalaryHeader);
            mandatoryHeaderNames.Add("BRA");
            mandatoryHeaderNames.Add(GrossSalaryHeader);
            mandatoryHeaderNames.Add("EPF_DEDUCTION");
            mandatoryHeaderNames.Add(NetSalaryHeader);
            mandatoryHeaderNames.Add("TOTAL_REMUNERATION");
            mandatoryHeaderNames.Add("BANK_TRANSFER_AMOUNT");
            mandatoryHeaderNames.Add("HOLD");
            mandatoryHeaderNames.Add("BANK");
            mandatoryHeaderNames.Add("BRANCH");
            mandatoryHeaderNames.Add("ACCOUNT");
            mandatoryHeaderNames.Add("EPF_CONTRIBUTION");
            mandatoryHeaderNames.Add("ETF_CONTRIBUTION");

            if (epfEtfSupported)
            {
                mandatoryHeaderNames.Add("MEMBER_STATUS");
                mandatoryHeaderNames.Add("DAYS_WORKED");
            }

            if (payeSupported)
            {
                mandatoryHeaderNames.Add("PAYE");
            }
        }

        protected virtual T Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            T data = New();

            data.LineNumber                     = row.LineNumber;
            data.Name                           = row.Fields[headerIndexes["NAME"]].Value;
            data.NIC                            = row.Fields[headerIndexes["NIC"]].Value;
            data.EmployeeNumber                 = row.Fields[headerIndexes["EMPLOYEE_NUMBER"]].Value;
            data.Designation                    = row.Fields[headerIndexes["DESIGNATION"]].Value;
            data.DateOfJoin                     = TcCsvValueDecorder.GetDate(row.Fields[headerIndexes["DATE_OF_JOIN"]].Value);
            data.AddressLine1                   = row.Fields[headerIndexes["ADDRESS_LINE_1"]].Value;
            data.AddressLine2                   = row.Fields[headerIndexes["ADDRESS_LINE_2"]].Value;
            data.City                           = row.Fields[headerIndexes["CITY"]].Value;
            data.BasicSalary                    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes[BasicSalaryHeader]].Value);
            data.BRA                            = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["BRA"]].Value);
            data.GrossSalary                    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes[GrossSalaryHeader]].Value);
            data.EPFDeduction                   = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["EPF_DEDUCTION"]].Value);
            data.NetSalary                      = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes[NetSalaryHeader]].Value);
            data.TotalRemuneration              = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["TOTAL_REMUNERATION"]].Value);
            data.BankTransferAmount             = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["BANK_TRANSFER_AMOUNT"]].Value);
            data.Payment                        = data.TotalRemuneration;
            data.Hold                           = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["HOLD"]].Value);
            data.Bank                           = row.Fields[headerIndexes["BANK"]].Value;
            data.Branch                         = row.Fields[headerIndexes["BRANCH"]].Value;
            data.AccountNumber                  = row.Fields[headerIndexes["ACCOUNT"]].Value;
            data.EPFContribution                = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["EPF_CONTRIBUTION"]].Value);
            data.ETFContribution                = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["ETF_CONTRIBUTION"]].Value);

            if (epfEtfSupported)
            {
                data.MemberStatus   = row.Fields[headerIndexes["MEMBER_STATUS"]].Value;
                data.DaysWorked     = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["DAYS_WORKED"]].Value);
            }

            if (payeSupported)
            {
                data.Paye = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["PAYE"]].Value);
            }

            return data;
        }

        protected virtual T Clean(T data)
        {
            data.Name               = TcFormatter.TrimAndUpper(data.Name);
            data.NIC                = TcFormatter.GetFormattedNIC(data.NIC);
            data.EmployeeNumber     = TcFormatter.TrimAndUpper(data.EmployeeNumber);
            data.Designation        = TcFormatter.TrimAndUpper(data.Designation);
            data.AddressLine1       = TcFormatter.TrimAndUpper(data.AddressLine1);
            data.AddressLine2       = TcFormatter.TrimAndUpper(data.AddressLine2);
            data.City               = TcFormatter.TrimAndUpper(data.City);
            data.Bank               = TcFormatter.TrimAndUpper(data.Bank);
            data.Branch             = TcFormatter.TrimAndUpper(data.Branch);
            data.AccountNumber      = TcFormatter.GetFormattedBankAccountNumber(data.AccountNumber);

            if (epfEtfSupported)
            {
                data.MemberStatus = TcFormatter.GetFormattedMemberStatus(data.MemberStatus);
            }

            return data;
        }
    }
}
