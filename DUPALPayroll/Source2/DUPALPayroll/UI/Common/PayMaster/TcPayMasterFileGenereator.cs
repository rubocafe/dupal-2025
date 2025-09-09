using DUPALPayroll.Library;
using System.IO;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.Common.PayMaster
{   
    public class TcPayMasterFileGenereator<T> where T : TiPayMasterDestination
    {
        public TcPayMasterOriginData Origin { get; set; }
        public TcBindingList<T> InvalidRows { get; set; }
        public TcBindingList<T> ValidRows { get; set; }

        public TcPayMasterFileGenereator(TcPayMasterOriginData originData)
        {
            Origin = originData;
            Reset();
        }

        private void Reset()
        {
            InvalidRows = new TcBindingList<T>();
            ValidRows   = new TcBindingList<T>();
        }

        public void GeneratePaymaster(TcBindingList<T> paymasterDataList, string targetFilePath)
        {
            InvalidRows.Clear();
            ValidRows.Clear();

            using (StreamWriter writer = new StreamWriter(targetFilePath))
            {
                decimal total = 0;

                foreach (T data in paymasterDataList)
                {
                    TcPayMasterDestinationData destination = data.GetPayMasterDestinationData();
                    TcPayMasterRow row = TcPayMasterRow.GetCreditRow(Origin, destination);

                    if (row.IsValid())
                    {
                        total += TcDecimal.GetDecimalFromText(row.Amount, 2);

                        string line = row.GetPayMasterLine();
                        writer.WriteLine(line);

                        ValidRows.Add(data);
                    }
                    else
                    {
                        InvalidRows.Add(data);
                    }
                }

                string dupalLine = GetDebitRow(total);
                writer.WriteLine(dupalLine);
            }
        }

        private string GetDebitRow(decimal total)
        {
            TcPayMasterRow row = TcPayMasterRow.GetDebitRow(Origin, total);
            string line = row.GetPayMasterLine();

            return line;
        }
    }
}
