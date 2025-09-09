using Payroll.Library;
using Payroll.Library.General;
using Payroll.Library.Payments.ComBank;
using Payroll.Library.Payments.Hnb;
using Payroll.UI.Controls;
using System;
using System.IO;

// Ruchira Bomiriya
// 2016/10/02

namespace Payroll.Library.Payments
{   
    public class TcBankPaymentsGenerator<T> where T : TiMemberData
    {
        public TcEmployerData Employer { get; set; }
        public TcBindingList<T> Members { get; set; }
        public TcBindingList<T> InvalidMembers { get; set; }
        public TcBindingList<T> ValidMembers { get; set; }

        public TcBankPaymentsGenerator(TcEmployerData employer, TcBindingList<T> allRows)
        {
            Employer = employer;
            Members = allRows;

            Reset();
        }

        private void Reset()
        {
            InvalidMembers = new TcBindingList<T>();
            ValidMembers   = new TcBindingList<T>();
        }

        public void GeneratePaymentsFile(string filePath)
        {
            ValidMembers.Clear();
            InvalidMembers.Clear();

            switch (Employer.Bank)
            {
                case "COM":
                    TcComBankPaymentsGenerator<T> com = new TcComBankPaymentsGenerator<T>(Employer, Members);
                    com.GeneratePaymentsFile(filePath);
                    ValidMembers = com.ValidMembers;
                    InvalidMembers = com.InvalidMembers;
                    break;

                case "HNB":
                    TcHnbPaymentsGenerator<T> hnb = new TcHnbPaymentsGenerator<T>(Employer, Members);
                    hnb.GeneratePaymentsFile(filePath);
                    ValidMembers = hnb.ValidMembers;
                    InvalidMembers = hnb.InvalidMembers;
                    break;

                default:
                    throw new Exception(String.Format("Bank [{0}] is not currently supported as an employer bank", Employer.Bank));
            }
        }
    }
}
