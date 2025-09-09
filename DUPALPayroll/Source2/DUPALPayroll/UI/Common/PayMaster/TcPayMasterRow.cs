using DUPALPayroll.Controls;
using DUPALPayroll.General;
using DUPALPayroll.Library;
using System;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.Common.PayMaster
{
    public class TcPayMasterRow : TiSearchable
    {
        public int LineNumber { get; set; }
        public string TranId { get; set; }                  // 04 Numeric Must be “Zeros”
        public string DestinationBank { get; set; }         // 04 Numeric [Bank MICR number]
        public string DestinationBranch { get; set; }       // 03 Numeric [Refer Branch Code List]
        public string DestinationAccount { get; set; }      // 12 Numeric Must have leading zeros
        public string DestinationAccountName { get; set; }  // 20 Alphabetic Recipients Name
        public string TransactionCode { get; set; }         // 02 Numeric 23 for Salaries
        public string ReturnCode { get; set; }              // 02 Numeric Must be “Zeros”
        public string CreditDebitCode { get; set; }         // 01 Numeric Credit = “O” Debit =”1”
        public string ReturnDate { get; set; }              // 06 Numeric Must have “Zeros”
        public string Amount { get; set; }                  // 12 Numeric Must not have decimal pointer.Must have leading zeros.For example: 1208.50 as 120850
        public decimal AmountDecimal
        {
            get
            {
                decimal decimalAmount = 0;

                if (Amount.Length > 2)
                {
                    decimalAmount = TcDecimal.GetDecimalFromText(Amount, 2);
                }

                return decimalAmount;
            }
        }
        public string CurrencyCode { get; set; }            // 03 Alphabetic Must be SLR
        public string OriginatingBank { get; set; }         // 04 Numeric Must be 7056
        public string OriginatingBranch { get; set; }       // 03 Numeric Must be Your Branch Code [ex: 036,003,016]
        public string OriginatingAccount { get; set; }      // 12 Numeric Must have leading zeros.[Your Company A/C No.]
        public string OriginatingAccountName { get; set; }  // 20 Alphabetic [Your Company A/C Name]
        public string Particulars { get; set; }             // 15 Alphabetic For example: [Employee No.]
        public string Reference { get; set; }               // 15 Alphabetic For example: [SALARY JANUARY]
        public string ValueDate { get; set; }               // 06 Numeric YYMMDD [ex : 2002JAN31 as 020131]
        public string SecurityField { get; set; }           // 06 Must be “Blank”
        public string Filler { get; set; }                  // 01 Must be [@] sign

        public TcPayMasterRow()
        {
            TranId          = "0000";
            TransactionCode = "23";
            ReturnCode      = "00";
            ReturnDate      = "000000";
            CurrencyCode    = "SLR";
            OriginatingBank = "7056";
            SecurityField   = "      ";
            Filler          = "@";
        }

        public static TcPayMasterRow GetCreditRow(TcPayMasterOriginData origin, TcPayMasterDestinationData destination)
        {
            TcPayMasterRow row = new TcPayMasterRow();

            row.LineNumber              = destination.LineNumber;

            row.DestinationBank         = destination.DestinationBank;
            row.DestinationBranch       = destination.DestinationBranch;
            row.DestinationAccount      = destination.DestinationAccount;
            row.DestinationAccountName  = destination.DestinationAccountName;
            row.CreditDebitCode         = "0";         // 01 Numeric Credit = “O” Debit =”1”
            row.OriginatingBank         = origin.OriginatingBank;
            row.OriginatingBranch       = origin.OriginatingBranch;
            row.OriginatingAccount      = origin.OriginatingAccount;
            row.OriginatingAccountName  = origin.OriginatingAccountName;
            row.Particulars             = destination.Particulars;
            row.Reference               = origin.Reference;
            row.ValueDate               = origin.ValueDate;

            decimal netAmount = destination.Amount > 0 ? destination.Amount : 0;
            netAmount   = Math.Round(netAmount, 2);
            row.Amount  = TcString.AppendZerosToFront(netAmount.ToString("N2").Replace(",", "").Replace(".", ""), 12);

            return row;
        }

        public bool IsValid()
        {
            if (TcValidator.IsValidBankCode(DestinationBank) &&
                TcValidator.IsValidBranchCode(DestinationBranch) &&
                TcValidator.IsValidBankAccountNumber(DestinationAccount))
            {
                return true;
            }

            return false;
        }

        public static TcPayMasterRow GetDebitRow(TcPayMasterOriginData origin, decimal total)
        {
            TcPayMasterDestinationData destination = TcPayMasterDestinationData.CreateFromOriginData(origin, total, TcString.AppendSpacesToFront("0", 15), 0);

            TcPayMasterRow row  = GetCreditRow(origin, destination);
            row.CreditDebitCode = "1";         // 01 Numeric Credit = “O” Debit =”1”

            return row;
        }

        public string GetPayMasterLine()
        {
            string line = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", 
                                            TranId,
                                            DestinationBank,
                                            DestinationBranch,
                                            DestinationAccount,
                                            DestinationAccountName,
                                            TransactionCode,
                                            ReturnCode,
                                            CreditDebitCode,
                                            ReturnDate,
                                            Amount,
                                            CurrencyCode,
                                            OriginatingBank,
                                            OriginatingBranch,
                                            OriginatingAccount,
                                            OriginatingAccountName,
                                            Particulars,
                                            Reference,
                                            ValueDate,
                                            SecurityField,
                                            Filler);

            return line;
        }

        public string[] GetSearchableFields()
        {
            string[] rows = new string[] {  DestinationBank,
                                            DestinationBranch,
                                            DestinationAccount,
                                            DestinationAccountName,
                                            CreditDebitCode,
                                            ReturnDate,
                                            OriginatingBank,
                                            OriginatingBranch,
                                            OriginatingAccount,
                                            OriginatingAccountName,
                                            Particulars,
                                            Reference};

            return rows;
        }
    }
}
