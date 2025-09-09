using DUPALPayroll.Library;
using System;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.Common.PayMaster
{
    public class TcPayMasterDestinationData
    {
        private string destinationBank { get; set; }         // 04 Numeric [Bank MICR number]
        private string destinationBranch { get; set; }       // 03 Numeric [Refer Branch Code List]
        private string destinationAccount { get; set; }      // 12 Numeric Must have leading zeros
        private string destinationAccountName { get; set; }  // 20 Alphabetic Recipients Name
        private string particulars { get; set; }             // 15 Alphabetic For example: [Employee No.]

        public int LineNumber { get; set; }
        public decimal Amount { get; set; }

        public string DestinationBank 
        {
            get
            {
                return destinationBank;
            }
            set
            {
                destinationBank = TcString.AppendZerosToFront(value, 4);
            }
        }

        public string DestinationBranch
        {
            get
            {
                return destinationBranch;
            }
            set
            {
                destinationBranch = TcString.AppendZerosToFront(value, 3);
            }
        }

        public string DestinationAccount
        {
            get
            {
                return destinationAccount;
            }
            set
            {
                destinationAccount = TcString.AppendZerosToFront(value, 12);
            }
        }

        public string DestinationAccountName
        {
            get
            {
                return destinationAccountName;
            }
            set
            {
                destinationAccountName = TcString.AppendSpacesToEnd(value, 20);
            }
        }               

        public string Particulars
        {
            get
            {
                return particulars;
            }
            set
            {
                particulars = TcString.AppendSpacesToEnd(value, 15); ;
            }
        }

        public TcPayMasterDestinationData()
        {
            LineNumber  = 0;
            Amount      = 0;

            destinationBank         = string.Empty;
            destinationBranch       = string.Empty;
            destinationAccount      = string.Empty;
            destinationAccountName  = string.Empty;
            particulars             = string.Empty;
        }

        public static TcPayMasterDestinationData CreateFromOriginData(TcPayMasterOriginData origin, decimal amount, string particulars, int line)
        {
            TcPayMasterDestinationData destination = new TcPayMasterDestinationData();

            destination.LineNumber  = line;
            destination.Amount      = amount;

            destination.DestinationBank         = origin.OriginatingBank;
            destination.DestinationBranch       = origin.OriginatingBranch;
            destination.DestinationAccount      = origin.OriginatingAccount;
            destination.DestinationAccountName  = origin.OriginatingAccountName;
            destination.Particulars             = particulars;

            return destination;
        }
    }
}
