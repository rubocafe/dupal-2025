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
    public class TcHnbControlRecord : TiSearchable
    {
        public string AccountName;
        public string TotalAmount;
        public string DebitAccountNumber;
        public string DateOfCrediting;
        public string HashTotal;
        public string NumberOfTransactions;
        public string BankCode;
        public string BranchCode;
        public string BankBranchCode;
        public string TransactionCode;
        public string ControlInformation;

        public TcHnbControlRecord(TcEmployerData employer, decimal totalAmount, long hashTotal, int numTransactions)
        {
            TcHnbEmployerData hnbEmployer = new TcHnbEmployerData(employer);

            FillAndFormat(hnbEmployer, totalAmount, hashTotal, numTransactions);
        }

        private void FillAndFormat(TcHnbEmployerData employer, decimal totalAmount, long hashTotal, int numTransactions)
        {
            AccountName = TcString.AppendSpacesToEnd(employer.AccountName, 20);
            TotalAmount = TcDecimal.MoneyWithoutDecimalPoint(totalAmount, 11);
            DebitAccountNumber = TcString.AppendZerosToFront(employer.DebitAccountNumber, 12);
            DateOfCrediting = employer.DateOfCrediting.ToString("yyMMdd");
            HashTotal = TcString.AppendZerosToFront(hashTotal.ToString(), 14);
            NumberOfTransactions = TcString.AppendZerosToFront(numTransactions.ToString(), 5);
            BankCode = TcString.AppendZerosToFront(employer.BankCode, 4);
            BranchCode = TcString.AppendZerosToFront(employer.BranchCode, 3);
            BankBranchCode = string.Format("{0}{1}", BankCode, BranchCode);
            TransactionCode = "223";
            ControlInformation = "  ";
        }

        public bool IsValid()
        {
            bool isValid = false;

            if (TcValidator.IsValidBankCode(BankCode) &&
                TcValidator.IsValidBranchCode(BranchCode) &&
                TcValidator.IsValidBankAccountNumber(DebitAccountNumber))
            {
                isValid = true;
            }

            return isValid;
        }

        
        public string FormattedLine()
        {
            string line = string.Format(
                "{0}{1}{2}{3}{4}{5}{6}{7}",
                AccountName,
                TotalAmount,
                DebitAccountNumber,
                DateOfCrediting,
                HashTotal,
                NumberOfTransactions,
                BankBranchCode,
                TransactionCode,
                ControlInformation
                );

            return line;
        }

        public string[] SearchableFields()
        {
            string[] fields = new string[] {
                AccountName,
                DebitAccountNumber,
                BankBranchCode,
            };

            return fields;
        }
    }
}
