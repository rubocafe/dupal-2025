using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.Epf;
using DUPALPayroll.UI.Common.Etf;
using DUPALPayroll.UI.Common.PayMaster;
using DUPALPayroll.UI.Common.SalaryBean;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-24

namespace DUPALPayroll.UI.Common.AnalyzeBean
{
    public class TcSalaryAnalyzedRow : TcSalaryRow, TiPayMasterDestination
    {
        public Dictionary<TeEmployeeAnalyzeFilter, string> Errors { get; set; }

        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string Initials { get; set; }
        public string LastName { get; set; }
        public string OCGrade { get; set; }

        public DateTime DOBBoundryDate { get; set; }

        public TcSalaryAnalyzedRow()
        {
            Errors = new Dictionary<TeEmployeeAnalyzeFilter, string>();
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
                TcDateDiff dateDiff = new TcDateDiff(DOBBoundryDate, DOBBoundryDate);
                if (DateOfBirth.HasValue)
                {
                    dateDiff = new TcDateDiff(DateOfBirth.Value, DOBBoundryDate);
                }

                return dateDiff.Years;
            }
        }

        public string AgeString
        {
            get
            {
                TcDateDiff dateDiff = new TcDateDiff(DOBBoundryDate, DOBBoundryDate);
                if (DateOfBirth.HasValue)
                {
                    dateDiff = new TcDateDiff(DateOfBirth.Value, DOBBoundryDate);
                }

                return dateDiff.ToString();
            }
        }

        public bool HasErrors
        {
            get
            {
                bool hasErrors = Errors.Count > 0 ? true : false;

                return hasErrors;
            }
        }

        public bool HasError(TeEmployeeAnalyzeFilter error)
        {
            return Errors.ContainsKey(error);
        }

        public string GetErrors()
        {
            string errors = string.Empty;

            int i = 1;
            foreach (KeyValuePair<TeEmployeeAnalyzeFilter, string> pair in Errors)
            {
                errors += string.Format("({0}) {1}\n", i, pair.Value);
                i++;
            }

            return errors;
        }

        public override string[] GetSearchableFields()
        {
            string[] fields = { NIC, AccountNumber, Name, Bank, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }

        public virtual TcPayMasterDestinationData GetPayMasterDestinationData()
        {
            TcPayMasterDestinationData destination = new TcPayMasterDestinationData();

            destination.LineNumber  = LineNumber;
            destination.Amount      = BankTransferAmount;

            destination.DestinationBank         = BankCode;
            destination.DestinationBranch       = BranchCode;
            destination.DestinationAccount      = AccountNumber;
            destination.DestinationAccountName  = Name;
            destination.Particulars             = NIC;

            return destination;
        }

        public virtual TcEpfDestinationData GetEpfDestinationData()
        {
            TcEpfDestinationData row = new TcEpfDestinationData();

            row.NICNumber                       = NIC;
            row.Initials                        = Initials;
            row.LastName                        = LastName;
            row.EmployersContribution           = EPFContribution;
            row.MembersContribution             = EPFDeduction;
            row.TotalContribution               = row.EmployersContribution + row.MembersContribution;
            row.TotalEarnings                   = GrossSalary;
            row.MemberStatus                    = MemberStatus;
            row.MemberNumber                    = EmployeeNumber;
            row.DaysOfWork                      = DaysWorked;
            row.OccupationClassificationGrade   = OCGrade;

            return row;
        }

        public virtual TcEtfDetailDestinationData GetEtfDestinationData()
        {
            TcEtfDetailDestinationData row = new TcEtfDetailDestinationData();

            row.MemberNumber        = EmployeeNumber;
            row.Initials            = Initials;
            row.Surname             = LastName;
            row.NICNumber           = NIC;
            row.TotalContribution   = ETFContribution;

            return row;
        }
    }
}
