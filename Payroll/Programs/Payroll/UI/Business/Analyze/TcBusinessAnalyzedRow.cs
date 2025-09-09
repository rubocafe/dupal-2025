using Payroll.Library.Date;
using Payroll.Library.Epf;
using Payroll.Library.Etf;
using Payroll.Library.General;
using Payroll.Library.Payments;
using Payroll.Library.Payments.ComBank;
using Payroll.UI.Business.MasterData;
using Payroll.UI.Business.Salary;
using Payroll.UI.Controls;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2015-11-05

namespace Payroll.UI.Business.Analyze
{
    public class TcBusinessAnalyzedRow : TcBusinessSalaryRow, TiSearchable, TiMemberData
    {
        public DateTime DOBBoundaryDate { get; set; }

        public string OCGrade { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }

        public List<TcBusinessMasterRow> DuplicateMasterRows { get; set; }
        public Dictionary<TeBusinessAnalyzeFilter, string> Errors { get; set; }

        public TcBusinessAnalyzedRow()
        {
            Errors = new Dictionary<TeBusinessAnalyzeFilter, string>();
            DuplicateMasterRows = new List<TcBusinessMasterRow>();
        }

        public bool HasErrors
        {
            get
            {
                bool hasErrors = Errors.Count > 0 ? true : false;

                return hasErrors;
            }
        }

        public Nullable<DateTime> DateOfBirth
        {
            get
            {
                Nullable<DateTime> dateOfBirth = TcNIC.GetDateOfBirthFromNIC(NIC);

                return dateOfBirth;
            }
        }

        public int Age
        {
            get
            {
                TcDateDiff dateDiff = new TcDateDiff(DOBBoundaryDate, DOBBoundaryDate);
                if (DateOfBirth.HasValue)
                {
                    dateDiff = new TcDateDiff(DateOfBirth.Value, DOBBoundaryDate);
                }

                return dateDiff.Years;
            }
        }

        public string AgeString
        {
            get
            {
                TcDateDiff dateDiff = new TcDateDiff(DOBBoundaryDate, DOBBoundaryDate);
                if (DateOfBirth.HasValue)
                {
                    dateDiff = new TcDateDiff(DateOfBirth.Value, DOBBoundaryDate);
                }

                return dateDiff.ToString();
            }
        }

        public bool HasError(TeBusinessAnalyzeFilter error)
        {
            return Errors.ContainsKey(error);
        }

        public string GetErrors()
        {
            string errors = string.Empty;

            int i = 1;
            foreach (KeyValuePair<TeBusinessAnalyzeFilter, string> pair in Errors)
            {
                errors += string.Format("({0}) {1}\n", i, pair.Value);
                i++;
            }

            return errors;
        }

        public override string[] SearchableFields()
        {
            string[] fields = { NIC, AccountNumber, NameWithInitials,
                                  Bank, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }

        public TcBankMemberData BankMemberData()
        {
            TcBankMemberData member = new TcBankMemberData(this);

            return member;
        }


        public virtual TcEpfMemberData EpfMemberData()
        {
            TcEpfMemberData member = new TcEpfMemberData(this);

            return member;
        }

        public virtual TcEtfMemberData EtfMemberData()
        {
            TcEtfMemberData member = new TcEtfMemberData(this);

            return member;
        }

        public decimal EpfTotal
        {
            get
            {
                var total = EpfDeduction + EpfContribution;
                return total;
            }
        }
    }
}
