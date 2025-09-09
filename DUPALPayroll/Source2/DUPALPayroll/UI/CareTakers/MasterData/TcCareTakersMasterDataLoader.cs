using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.MasterData
{
    public class TcCareTakersMasterDataLoader
    {
        public TcBindingList<TcCareTakersMasterRow> LoadFromCSV(string csvFilePath)
        {
            TcBindingList<TcCareTakersMasterRow> list = new TcBindingList<TcCareTakersMasterRow>();

            TcCsvFile csvFile = new TcCsvFile();
            csvFile.Load(csvFilePath);

            bool headerFound = false;
            Dictionary<string, int> headerIndexes = new Dictionary<string, int>();
            foreach (TcCsvDataRow row in csvFile.Rows)
            {
                if (!headerFound)
                {
                    string startHeaderFieldName = row.Fields[0].Value.Trim().Replace(" ", "_").ToUpper();

                    if (startHeaderFieldName == "SITE_NAME")
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

                TcCareTakersMasterRow data = GetEmployeeDataFromCSVRow(row, headerIndexes);
                if (IsValidRow(data)) // remove empty rows
                {
                    list.Add(data);
                }
            }

            if (!headerFound)
            {
                throw new Exception("Invalid master data file. Header row not found. Header row must starts with \"SITE_NAME\"");
            }

            return list;
        }

        private bool IsValidRow(TcCareTakersMasterRow data)
        {
            bool isValid = data.NIC.Length > 0 ||
                            data.Name.Length > 0;

            return isValid;
        }

        private void CheckHeaderNames(Dictionary<string, int> headerIndexes)
        {
            List<string> mandatoryHeaderNames = new List<string>();

            mandatoryHeaderNames.Add("SITE_NAME");
            mandatoryHeaderNames.Add("SITE_CODE");
            mandatoryHeaderNames.Add("SITE_ENGINEER");
            mandatoryHeaderNames.Add("NAME");
            mandatoryHeaderNames.Add("NIC");
            mandatoryHeaderNames.Add("ADDRESS");
            mandatoryHeaderNames.Add("BANK");
            mandatoryHeaderNames.Add("BRANCH");
            mandatoryHeaderNames.Add("ACCOUNT");
            mandatoryHeaderNames.Add("BANK_CODE");
            mandatoryHeaderNames.Add("BRANCH_CODE");

            TcHeaderNamesChecker checker = new TcHeaderNamesChecker();
            if (!checker.CheckHeaderNames(headerIndexes, mandatoryHeaderNames))
            {
                string error = string.Format("Invalid Master Data file\n{0}", checker.Error);
                throw new Exception(error);
            }
        }

        private TcCareTakersMasterRow GetEmployeeDataFromCSVRow(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcCareTakersMasterRow data = new TcCareTakersMasterRow();

            data.LineNumber         = row.LineNumber;
            data.SiteName           = row.Fields[headerIndexes["SITE_NAME"]].Value;
            data.SiteCode           = row.Fields[headerIndexes["SITE_CODE"]].Value;
            data.SiteEngineer       = row.Fields[headerIndexes["SITE_ENGINEER"]].Value;
            data.Name               = row.Fields[headerIndexes["NAME"]].Value;
            data.NIC                = row.Fields[headerIndexes["NIC"]].Value;
            data.Address            = row.Fields[headerIndexes["ADDRESS"]].Value;
            data.Bank               = row.Fields[headerIndexes["BANK"]].Value;
            data.Branch             = row.Fields[headerIndexes["BRANCH"]].Value;
            data.AccountNumber      = row.Fields[headerIndexes["ACCOUNT"]].Value;
            data.BankCode           = row.Fields[headerIndexes["BANK_CODE"]].Value;
            data.BranchCode         = row.Fields[headerIndexes["BRANCH_CODE"]].Value;

            data = Clean(data);

            return data;
        }

        private TcCareTakersMasterRow Clean(TcCareTakersMasterRow data)
        {
            data.SiteName           = TcFormatter.TrimAndUpper(data.SiteName);
            data.SiteCode           = TcFormatter.TrimAndUpper(data.SiteCode);
            data.SiteEngineer       = TcFormatter.TrimAndUpper(data.SiteEngineer);
            data.Name               = TcFormatter.TrimAndUpper(data.Name);
            data.NIC                = TcFormatter.GetFormattedNIC(data.NIC);
            data.Address            = TcFormatter.TrimAndUpper(data.Address);
            data.Bank               = TcFormatter.TrimAndUpper(data.Bank);
            data.Branch             = TcFormatter.TrimAndUpper(data.Branch);
            data.AccountNumber      = TcFormatter.GetFormattedBankAccountNumber(data.AccountNumber);
            data.BankCode           = TcFormatter.GetFormattedBankCode(data.BankCode);
            data.BranchCode         = TcFormatter.GetFormattedBranchCode(data.BranchCode);

            return data;
        }
    }
}
