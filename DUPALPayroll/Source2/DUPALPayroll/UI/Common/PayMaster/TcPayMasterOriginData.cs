using DUPALPayroll.Library;
using System;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.Common.PayMaster
{
    public class TcPayMasterOriginData
    {
        public string OriginatingBank { get; private set; } // 04 Numeric Must be 7056
        private string originatingBranch;                   // 03 Numeric Must be Your Branch Code [ex: 036,003,016]       
        private string originatingAccount;                  // 12 Numeric Must have leading zeros.[Your Company A/C No.]
        private string originatingAccountName;              // 20 Alphabetic [Your Company A/C Name]
        private string reference;                           // 15 Alphabetic For example: [SALARY JANUARY]
        private string valueDate;                           // 06 Numeric YYMMDD [ex : 2002JAN31 as 020131]

        public string OriginatingBranch 
        {
            get
            {
                return originatingBranch;
            }
            set
            {
                originatingBranch = TcString.AppendZerosToFront(value, 3);
            }
        }

        public string OriginatingAccount
        {
            get
            {
                return originatingAccount;
            }
            set
            {
                originatingAccount = TcString.AppendZerosToFront(value, 12);
            }
        }

        public string OriginatingAccountName
        {
            get
            {
                return originatingAccountName;
            }
            set
            {
                originatingAccountName = TcString.AppendSpacesToEnd(value, 20);
            }
        }

        public string Reference
        {
            get
            {
                return reference;
            }
            set
            {
                reference = TcString.AppendSpacesToEnd(value, 15);
            }
        }               

        public string ValueDate
        {
            get
            {
                return valueDate;
            }
            set
            {
                valueDate = TcString.AppendSpacesToEnd(value, 6);
            }
        }               

        public TcPayMasterOriginData()
        {
            OriginatingBank         = "7056";
            OriginatingAccount      = "";
            OriginatingAccountName  = "";
            Reference               = "";
            SetValueDate(DateTime.Now);
        }

        public void SetValueDate(DateTime dateTime)
        {
            ValueDate = dateTime.ToString("yyMMdd");
        }
    }
}
