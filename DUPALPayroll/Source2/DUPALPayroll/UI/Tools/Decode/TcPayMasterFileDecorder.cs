using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.PayMaster;
using System.IO;

// Harshan Nishantha
// 2013-08-30

namespace DUPALPayroll.UI.CommissionAgents.Tools.Decode
{
    public class TcPayMasterFileDecorder
    {
        private string filePath;

        public TcBindingList<TcPayMasterRow> PaymasterRowsList { get; set; }

        public TcPayMasterFileDecorder(string filePath)
        {
            this.filePath = filePath;

            PaymasterRowsList = new TcBindingList<TcPayMasterRow>();
        }

        public TcBindingList<TcPayMasterRow> Decode()
        {
            PaymasterRowsList = new TcBindingList<TcPayMasterRow>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                int lineNumber = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    TcPayMasterRow data = GetPaymasterData(lineNumber, line);
                    PaymasterRowsList.Add(data);

                    lineNumber++;
                }
            }

            return PaymasterRowsList;
        }

        private TcPayMasterRow GetPaymasterData(int lineNumber, string line)
        {
            TcPayMasterRow data = new TcPayMasterRow();

            data.LineNumber = lineNumber;

            data.TranId                     = line.Substring(0, 4);     // 04 Numeric Must be “Zeros”
            data.DestinationBank            = line.Substring(4, 4);     // 04 Numeric [Bank MICR number]
            data.DestinationBranch          = line.Substring(8, 3);     // 03 Numeric [Refer Branch Code List]
            data.DestinationAccount         = line.Substring(11, 12);   // 12 Numeric Must have leading zeros
            data.DestinationAccountName     = line.Substring(23, 20);   // 20 Alphabetic Recipients Name
            data.TransactionCode            = line.Substring(43, 2);    // 02 Numeric 23 for Salaries
            data.ReturnCode                 = line.Substring(45, 2);    // 02 Numeric Must be “Zeros”
            data.CreditDebitCode            = line.Substring(47, 1);    // 01 Numeric Credit = “O” Debit =”1”
            data.ReturnDate                 = line.Substring(48, 6);    // 06 Numeric Must have “Zeros”
            data.Amount                     = line.Substring(54, 12);   // 12 Numeric Must not have decimal pointer.Must have leading zeros.For example: 1208.50 as 120850
            data.CurrencyCode               = line.Substring(66, 3);    // 03 Alphabetic Must be SLR
            data.OriginatingBank            = line.Substring(69, 4);    // 04 Numeric Must be 7056
            data.OriginatingBranch          = line.Substring(73, 3);    // 03 Numeric Must be Your Branch Code [ex: 036,003,016]
            data.OriginatingAccount         = line.Substring(76, 12);   // 12 Numeric Must have leading zeros.[Your Company A/C No.]
            data.OriginatingAccountName     = line.Substring(88, 20);   // 20 Alphabetic [Your Company A/C Name]
            data.Particulars                = line.Substring(108, 15);  // 15 Alphabetic For example: [Employee No.]
            data.Reference                  = line.Substring(123, 15);  // 15 Alphabetic For example: [SALARY JANUARY]
            data.ValueDate                  = line.Substring(138, 6);   // 06 Numeric YYMMDD [ex : 2002JAN31 as 020131]
            data.SecurityField              = line.Substring(144, 6);   // 06 Must be “Blank”
            data.Filler                     = line.Substring(150, 1);   // 01 Must be [@] sign

            return data;
        }
    }
}
