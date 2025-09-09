using Payroll.Library;
using Payroll.Library.General;
using Payroll.UI.Controls;
using System;
using System.IO;

// Harshan Nishantha
// 2013-09-17

namespace Payroll.Library.Payments.Hnb
{
    public class TcHnbPaymentsGenerator<T> where T : TiMemberData
    {
        public TcEmployerData Employer { get; set; }
        public TcBindingList<T> Members { get; set; }
        public TcBindingList<T> ValidMembers { get; set; }
        public TcBindingList<T> InvalidMembers { get; set; }

        public TcHnbPaymentsGenerator(TcEmployerData employer, TcBindingList<T> members)
        {
            Employer = employer;
            Members = members;

            Reset();
        }

        private void Reset()
        {
            InvalidMembers = new TcBindingList<T>();
            ValidMembers = new TcBindingList<T>();
        }

        public void GeneratePaymentsFile(string filePath)
        {
            InvalidMembers.Clear();
            ValidMembers.Clear();

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                decimal totalAmount = 0;
                int validCount = 1;
                long hashTotal = 0;

                foreach (T row in Members)
                {
                    TcBankMemberData member = row.BankMemberData();
                    TcHnbDetailRecord detail = new TcHnbDetailRecord(member);

                    if (detail.IsValid())
                    {
                        totalAmount += member.Amount;
                        hashTotal += Convert.ToInt64(detail.CreditAccountNumber);
                        validCount++;
                    }
                }

                TcHnbControlRecord control = new TcHnbControlRecord(Employer, totalAmount, hashTotal, validCount - 1);
                string recordLine = control.FormattedLine();
                writer.WriteLine(recordLine);

                foreach (T row in Members)
                {
                    TcBankMemberData member = row.BankMemberData();
                    TcHnbDetailRecord detail = new TcHnbDetailRecord(member);

                    if (detail.IsValid())
                    {
                        string line = detail.FormattedLine();
                        writer.WriteLine(line);

                        ValidMembers.Add(row);
                    }
                    else
                    {
                        InvalidMembers.Add(row);
                    }
                }

                writer.WriteLine(TcString.AppendZerosToFront("0", 80));
            }
        }
    }
}
