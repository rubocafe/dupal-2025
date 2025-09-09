using Payroll.General;
using Payroll.Library.General;
using Payroll.UI.Controls;
using System;

// Harshan Nishantha
// 2013-09-17

namespace Payroll.Library.Payments.ComBank
{
    public class TcComBankPaymentRecord : TiSearchable
    {
        public int LineNumber { get; set; }

        public string TranId { get; set; }
        public string DestinationBank { get; set; }
        public string DestinationBranch { get; set; }
        public string DestinationAccount { get; set; }
        public string DestinationAccountName { get; set; }
        public string TransactionCode { get; set; }
        public string ReturnCode { get; set; }
        public string CreditDebitCode { get; set; }
        public string ReturnDate { get; set; }
        public string Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string OriginatingBank { get; set; }
        public string OriginatingBranch { get; set; }
        public string OriginatingAccount { get; set; }
        public string OriginatingAccountName { get; set; }
        public string Particulars { get; set; }
        public string Reference { get; set; }
        public string ValueDate { get; set; }
        public string SecurityField { get; set; }
        public string Filler { get; set; }

        public TcComBankPaymentRecord()
        {
        }

        public TcComBankPaymentRecord(TcEmployerData employer, TcBankMemberData member, bool isDebit = false)
        {
            TcComBankEmployerData comBankEmployer = new TcComBankEmployerData(employer);
            TcComBankMemberData comBankMember = new TcComBankMemberData(member);

            FillAndFormat(comBankEmployer, comBankMember, isDebit);
        }

        protected void FillAndFormat(TcComBankEmployerData employer, TcComBankMemberData member, bool isCredit)
        {
            LineNumber = member.LineNumber;

            TranId = "0000";
            DestinationBank = TcString.AppendZerosToFront(member.DestinationBank, 4);
            DestinationBranch = TcString.AppendZerosToFront(member.DestinationBranch, 3);
            DestinationAccount = TcString.AppendZerosToFront(member.DestinationAccount, 12);
            DestinationAccountName = TcString.AppendSpacesToEnd(member.DestinationAccountName, 20);
            TransactionCode = "23";
            ReturnCode = "00";
            CreditDebitCode = isCredit ? "0" : "1";
            ReturnDate = "000000";
            Amount = TcDecimal.MoneyWithoutDecimalPoint(member.Amount, 12);
            CurrencyCode = "SLR";
            OriginatingBank = TcString.AppendZerosToFront(employer.OriginatingBank, 4);
            OriginatingBranch = TcString.AppendZerosToFront(employer.OriginatingBranch, 3);
            OriginatingAccount = TcString.AppendZerosToFront(employer.OriginatingAccount, 12);
            OriginatingAccountName = TcString.AppendSpacesToEnd(employer.OriginatingAccountName, 20);
            Particulars = TcString.AppendSpacesToEnd(member.Particulars, 15);
            Reference = TcString.AppendSpacesToEnd(employer.Reference, 15);
            ValueDate = employer.ValueDate.ToString("yyMMdd"); // 6 characters
            SecurityField = "      ";
            Filler = "@";
        }

        public bool IsValid()
        {
            bool isValid = false;

            if (TcValidator.IsValidBankCode(DestinationBank) &&
                TcValidator.IsValidBranchCode(DestinationBranch) &&
                TcValidator.IsValidBankAccountNumber(DestinationAccount))
            {
                isValid = true;
            }

            return isValid;
        }

        public string FormattedLine()
        {
            string line = string.Format(
                "{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}", 
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
                Filler
                );

            return line;
        }

        public string[] SearchableFields()
        {
            string[] rows = new string[] {
                DestinationBank,
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
                Reference
            };

            return rows;
        }
    }
}
