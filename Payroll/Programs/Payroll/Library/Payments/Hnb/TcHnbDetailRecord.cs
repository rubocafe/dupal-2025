using Payroll.General;
using Payroll.Library.General;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ruchira Bomiriya
// 2016/09/16

namespace Payroll.Library.Payments.Hnb
{
    public class TcHnbDetailRecord : TiSearchable
    {
        public string ReferenceNumber { get; set; }
        public string AccountName { get; set; }
        public string BankCode;
        public string BranchCode;
        public string BankBranchCode { get; set; }
        public string CreditAccountNumber { get; set; }
        public string TransactionCode { get; set; }
        public string Amount { get; set; }
        public string ValueDate { get; set; }
        public string Comments { get; set; }

        public TcHnbDetailRecord(TcBankMemberData member)
        {
            TcHnbMemberData hnbMember = new TcHnbMemberData(member);

            FillAndFormat(hnbMember);
        }

        private void FillAndFormat(TcHnbMemberData member)
        {
            ReferenceNumber = TcString.AppendZerosToFront(member.ReferenceNumber.ToString(), 8);
            AccountName = TcString.AppendSpacesToEnd(member.AccountName, 20);
            BankCode = TcString.AppendZerosToFront(member.BankCode, 4);
            BranchCode = TcString.AppendZerosToFront(member.BranchCode, 3);
            BankBranchCode = string.Format("{0}{1}", BankCode, BranchCode);
            CreditAccountNumber = TcString.AppendZerosToFront(member.CrediAccountNumber, 12);
            TransactionCode = "023";
            Amount = TcDecimal.MoneyWithoutDecimalPoint(member.Amount, 11);
            ValueDate = member.ValueDate.ToString("yyMMdd");
            Comments = TcString.AppendSpacesToEnd(member.Comments, 13);
        }

        public bool IsValid()
        {
            bool isValid = false;

            if (TcValidator.IsValidBankCode(BankCode) &&
                TcValidator.IsValidBranchCode(BranchCode) &&
                TcValidator.IsValidBankAccountNumber(CreditAccountNumber))
            {
                isValid = true;
            }

            return isValid;
        }

        public string FormattedLine()
        {
            string line = string.Format(
                "{0}{1}{2}{3}{4}{5}{6}{7}",
                ReferenceNumber,
                AccountName,
                BankBranchCode,
                CreditAccountNumber,
                TransactionCode,
                Amount,
                ValueDate,
                Comments
                );

            return line;
        }

        public string[] SearchableFields()
        {
            string[] rows = new string[] {
                AccountName,
                BankCode,
                BranchCode,
                CreditAccountNumber,
                TransactionCode,
                Comments
            };

            return rows;
        }
    }
}
