using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.Commissions
{
    public class TcCommissionsLoader
    {
        private TcYearMonth workingYearMonth;

        public TcCommissionsLoader(TcYearMonth workingYearMonth)
        {
            this.workingYearMonth = workingYearMonth;
        }

        public TcBindingList<TcCommissionsRow> LoadFromCSV(string csvFilePath)
        {
            TcBindingList<TcCommissionsRow> list = new TcBindingList<TcCommissionsRow>();

            TcCsvFile csvFile = new TcCsvFile();
            csvFile.Load(csvFilePath);

            bool headerFound = false;
            Dictionary<string, int> headerIndexes = new Dictionary<string, int>();
            foreach (TcCsvDataRow row in csvFile.Rows)
            {
                if (!headerFound)
                {
                    string startHeaderFieldName = row.Fields[0].Value.Trim().Replace(" ", "_").ToUpper();

                    if (startHeaderFieldName == "FL_CODE")
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

                TcCommissionsRow data = GetDataFromCSVRow(row, headerIndexes);
                list.Add(data);
            }

            if (!headerFound)
            {
                throw new Exception("Invalid Commissions file. Header row not found\nHeader row must start with \"FL_CODE\"");
            }

            return list;
        }

        private void CheckHeaderNames(Dictionary<string, int> headerIndexes)
        {
            List<string> mandatoryHeaderNames = new List<string>();

            mandatoryHeaderNames.Add("VIRTUAL_NUMBER");
            mandatoryHeaderNames.Add("D_OF_JOIN");
            mandatoryHeaderNames.Add("BPO_NAME");
            mandatoryHeaderNames.Add("ADDRESS");
            mandatoryHeaderNames.Add("NIC");
            mandatoryHeaderNames.Add("BANK");
            mandatoryHeaderNames.Add("ACC:_NO");
            mandatoryHeaderNames.Add("BRANCH");
            mandatoryHeaderNames.Add("SALES_MANAGER");
            mandatoryHeaderNames.Add("GROSS_COMMISSION");
            mandatoryHeaderNames.Add("8%_EPF_DEDUCTION");
            mandatoryHeaderNames.Add("NET_COMMISSION");
            mandatoryHeaderNames.Add("TL/BPO");

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                mandatoryHeaderNames.Add("EMPLOYEE_NUMBER");
                mandatoryHeaderNames.Add("EPF_CONTRIBUTION");
                mandatoryHeaderNames.Add("ETF_CONTRIBUTION");
                mandatoryHeaderNames.Add("MEMBER_STATUS");
                mandatoryHeaderNames.Add("DAYS_WORKED");
            }

            if (TcVersions.IsPayeSupported(workingYearMonth))
            {
                mandatoryHeaderNames.Add("PAYE");
            }

            TcHeaderNamesChecker checker = new TcHeaderNamesChecker();
            if (!checker.CheckHeaderNames(headerIndexes, mandatoryHeaderNames))
            {
                string error = string.Format("Invalid Commissions file\n{0}", checker.Error);
                throw new Exception(error);
            }
        }

        private TcCommissionsRow GetDataFromCSVRow(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcCommissionsRow data = new TcCommissionsRow();

            data.LineNumber = row.LineNumber;
            data.VirtualNumber      = row.Fields[headerIndexes["VIRTUAL_NUMBER"]].Value;
            data.DateOfJoin         = TcCsvValueDecorder.GetDate(row.Fields[headerIndexes["D_OF_JOIN"]].Value);
            data.Name               = row.Fields[headerIndexes["BPO_NAME"]].Value;
            data.Address            = row.Fields[headerIndexes["ADDRESS"]].Value;
            data.NIC                = row.Fields[headerIndexes["NIC"]].Value;
            data.Bank               = row.Fields[headerIndexes["BANK"]].Value;
            data.AccountNumber      = row.Fields[headerIndexes["ACC:_NO"]].Value;
            data.Branch             = row.Fields[headerIndexes["BRANCH"]].Value;
            data.SalesManager       = row.Fields[headerIndexes["SALES_MANAGER"]].Value;
            data.GrossCommission    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["GROSS_COMMISSION"]].Value);
            data.EPFDeduction       = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["8%_EPF_DEDUCTION"]].Value);
            data.NetCommission      = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["NET_COMMISSION"]].Value);
            data.TLorBPO            = row.Fields[headerIndexes["TL/BPO"]].Value;

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                data.EmployeeNumber     = row.Fields[headerIndexes["EMPLOYEE_NUMBER"]].Value;
                data.EPFContribution    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["EPF_CONTRIBUTION"]].Value);
                data.ETFContribution    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["ETF_CONTRIBUTION"]].Value);
                data.MemberStatus       = row.Fields[headerIndexes["MEMBER_STATUS"]].Value;
                data.DaysWorked         = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["DAYS_WORKED"]].Value);
            }

            if (TcVersions.IsPayeSupported(workingYearMonth))
            {
                data.Paye = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["PAYE"]].Value);
            }

            data = Clean(data);

            return data;
        }

        private TcCommissionsRow Clean(TcCommissionsRow data)
        {
            data.VirtualNumber  = TcFormatter.UpperAndRemoveSpaces(data.VirtualNumber);
            data.Name           = TcFormatter.TrimAndUpper(data.Name);
            data.Address        = TcFormatter.TrimAndUpper(data.Address);
            data.NIC            = TcFormatter.GetFormattedNIC(data.NIC);
            data.Bank           = TcFormatter.TrimAndUpper(data.Bank);
            data.AccountNumber  = TcFormatter.GetFormattedBankAccountNumber(data.AccountNumber);
            data.Branch         = TcFormatter.TrimAndUpper(data.Branch);
            data.SalesManager   = TcFormatter.TrimAndUpper(data.SalesManager);
            data.TLorBPO        = TcFormatter.TrimAndUpper(data.TLorBPO);

            if (TcVersions.IsEpfEtfSupported(workingYearMonth))
            {
                data.EmployeeNumber = TcFormatter.TrimAndUpper(data.EmployeeNumber);
                data.MemberStatus   = TcFormatter.GetFormattedMemberStatus(data.MemberStatus);
            }

            return data;
        }
    }
}
