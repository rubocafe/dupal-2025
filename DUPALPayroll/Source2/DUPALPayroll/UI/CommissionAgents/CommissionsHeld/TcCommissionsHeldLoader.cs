using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Csv;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-26

namespace DUPALPayroll.UI.CommissionAgents.CommissionsHeld
{
    public class TcCommissionsHeldLoader
    {
        public TcBindingList<TcCommissionsHeldRow> LoadFromCSV(string csvFilePath)
        {
            TcBindingList<TcCommissionsHeldRow> list = new TcBindingList<TcCommissionsHeldRow>();

            TcCsvFile csvFile = new TcCsvFile();
            csvFile.Load(csvFilePath);

            bool headerFound = false;
            Dictionary<string, int> headerIndexes = new Dictionary<string, int>();
            foreach (TcCsvDataRow row in csvFile.Rows)
            {
                if (!headerFound)
                {
                    string startHeaderFieldName = row.Fields[0].Value.Trim().Replace(" ", "_").ToUpper();

                    if (startHeaderFieldName == "REQUEST")
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

                TcCommissionsHeldRow data = GetDataFromCSVRow(row, headerIndexes);
                list.Add(data);
            }

            if (!headerFound)
            {
                throw new Exception("Invalid Commissions Held file. Header row not found\nHeader row must start with \"REQUEST\"");
            }

            return list;
        }

        private void CheckHeaderNames(Dictionary<string, int> headerIndexes)
        {
            List<string> mandatoryHeaderNames = new List<string>();

            mandatoryHeaderNames.Add("REQUEST");
            mandatoryHeaderNames.Add("VIRTUAL");
            mandatoryHeaderNames.Add("NAME");
            mandatoryHeaderNames.Add("MANAGER");
            mandatoryHeaderNames.Add("NET_COMMISSION");
            mandatoryHeaderNames.Add("HOLD");
            mandatoryHeaderNames.Add("AMT_PAYABLE");

            TcHeaderNamesChecker checker = new TcHeaderNamesChecker();
            if (!checker.CheckHeaderNames(headerIndexes, mandatoryHeaderNames))
            {
                string error = string.Format("Invalid Commissions file\n{0}", checker.Error);
                throw new Exception(error);
            }
        }

        private TcCommissionsHeldRow GetDataFromCSVRow(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcCommissionsHeldRow data = new TcCommissionsHeldRow();

            data.LineNumber     = row.LineNumber;
            data.Request        = row.Fields[headerIndexes["REQUEST"]].Value;
            data.VirtualNumber  = row.Fields[headerIndexes["VIRTUAL"]].Value;
            data.Name           = row.Fields[headerIndexes["NAME"]].Value;
            data.Manager        = row.Fields[headerIndexes["MANAGER"]].Value;
            data.NetCommission     = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["NET_COMMISSION"]].Value);
            data.Hold           = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["HOLD"]].Value);
            data.AmountPayable  = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["AMT_PAYABLE"]].Value);

            data = Clean(data);

            return data;
        }

        private TcCommissionsHeldRow Clean(TcCommissionsHeldRow data)
        {
            data.Request        = TcFormatter.TrimAndUpper(data.Request);
            data.VirtualNumber  = TcFormatter.TrimAndUpper(data.VirtualNumber);
            data.Name           = TcFormatter.TrimAndUpper(data.Name);
            data.Manager        = TcFormatter.TrimAndUpper(data.Manager);

            return data;
        }
    }
}
