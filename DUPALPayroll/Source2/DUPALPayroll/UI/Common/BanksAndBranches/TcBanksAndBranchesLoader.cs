using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.Common.BanksAndBranches
{
    public class TcBanksAndBranchesLoader
    {
        public TcBindingList<TcBanksAndBranchesRow> LoadFromCSV(string csvFilePath)
        {
            TcBindingList<TcBanksAndBranchesRow> list = new TcBindingList<TcBanksAndBranchesRow>();

            TcCsvFile csvFile = new TcCsvFile();
            csvFile.Load(csvFilePath);

            bool headerFound = false;
            Dictionary<string, int> headerIndexes = new Dictionary<string, int>();
            foreach (TcCsvDataRow row in csvFile.Rows)
            {
                if (!headerFound)
                {
                    string startHeaderFieldName = row.Fields[0].Value.Trim().Replace(" ", "_").ToUpper();

                    if (startHeaderFieldName == "BANK_CODE")
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

                TcBanksAndBranchesRow data = GetDataFromCSVRow(row, headerIndexes);
                list.Add(data);
            }

            if (!headerFound)
            {
                throw new Exception("Invalid banks and branches data file. Header row not found");
            }

            return list;
        }

        private void CheckHeaderNames(Dictionary<string, int> headerIndexes)
        {
            List<string> mandatoryHeaderNames = new List<string>();

            mandatoryHeaderNames.Add("BANK_CODE");
            mandatoryHeaderNames.Add("BRANCH_CODE");
            mandatoryHeaderNames.Add("BANK");
            mandatoryHeaderNames.Add("BANK_NAME");
            mandatoryHeaderNames.Add("BRANCH");

            TcHeaderNamesChecker checker = new TcHeaderNamesChecker();
            if (!checker.CheckHeaderNames(headerIndexes, mandatoryHeaderNames))
            {
                string error = string.Format("Invalid Banks and Branches file\n{0}", checker.Error);
                throw new Exception(error);
            }
        }

        private TcBanksAndBranchesRow GetDataFromCSVRow(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcBanksAndBranchesRow data = new TcBanksAndBranchesRow();

            data.LineNumber     = row.LineNumber;
            data.BankCode       = TcCsvValueDecorder.GetInt(row.Fields[headerIndexes["BANK_CODE"]].Value);
            data.BranchCode     = TcCsvValueDecorder.GetInt(row.Fields[headerIndexes["BRANCH_CODE"]].Value);
            data.Bank           = row.Fields[headerIndexes["BANK"]].Value;
            data.BankName       = row.Fields[headerIndexes["BANK_NAME"]].Value;
            data.Branch         = row.Fields[headerIndexes["BRANCH"]].Value;

            data = Clean(data);

            return data;
        }

        private TcBanksAndBranchesRow Clean(TcBanksAndBranchesRow data)
        {
            data.Bank       = TcFormatter.UpperAndRemoveSpaces(data.Bank);
            data.BankName   = TcFormatter.TrimAndUpper(data.BankName);
            data.Branch     = TcFormatter.TrimAndUpper(data.Branch);

            return data;
        }
    }
}
