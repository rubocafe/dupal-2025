using Payroll.Library;
using Payroll.Library.General;
using Payroll.UI.Controls;
using System;
using System.IO;

// Harshan Nishantha
// 2013-09-17

namespace Payroll.Library.Payments.ComBank
{   
    public class TcComBankPaymentsGenerator<T> where T : TiMemberData
    {
        public TcEmployerData Employer { get; set; }
        public TcBindingList<T> Members { get; set; }
        public TcBindingList<T> ValidMembers { get; set; }
        public TcBindingList<T> InvalidMembers { get; set; }

        public TcComBankPaymentsGenerator(TcEmployerData employer, TcBindingList<T> members)
        {
            Employer = employer;
            Members = members;

            Reset();
        }

        private void Reset()
        {
            InvalidMembers = new TcBindingList<T>();
            ValidMembers   = new TcBindingList<T>();
        }

        public void GeneratePaymentsFile(string filePath)
        {
            InvalidMembers.Clear();
            ValidMembers.Clear();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                decimal total = 0;

                foreach (T row in Members)
                {
                    TcBankMemberData member = row.BankMemberData();
                    TcComBankCreditRecord credit = new TcComBankCreditRecord(Employer, member);

                    if (credit.IsValid())
                    {
                        total += TcDecimal.GetDecimalFromText(credit.Amount, 2);

                        string creditLine = credit.FormattedLine();
                        writer.WriteLine(creditLine);

                        ValidMembers.Add(row);
                    }
                    else
                    {
                        InvalidMembers.Add(row);
                    }
                }

                TcComBankDebitRecord debit = new TcComBankDebitRecord(Employer, total);
                string debitLine = debit.FormattedLine();
                writer.WriteLine(debitLine);
            }
        }
    }
}
