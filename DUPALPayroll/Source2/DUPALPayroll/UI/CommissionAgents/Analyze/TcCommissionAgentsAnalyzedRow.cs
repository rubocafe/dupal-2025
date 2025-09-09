using DUPALPayroll.Controls;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.CommissionAgents.MasterData;
using DUPALPayroll.UI.Common.Epf;
using DUPALPayroll.UI.Common.Etf;
using DUPALPayroll.UI.Common.PayMaster;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-08-27

namespace DUPALPayroll.UI.CommissionAgents.Analyze
{
    public class TcCommissionAgentsAnalyzedRow : TiSearchable, TiPayMasterDestination
    {
        public int      LineNumber { get; set; }
        public string   Bank { get; set; }
        public string   Branch { get; set; }
        public string   DestinationAccount { get; set; }
        public string   DestinationAccountName { get; set; }
        public string   NIC { get; set; }
        public string   VirtualNumber { get; set; }
        public decimal  NetCommission { get; set; }
        public decimal  Hold { get; set; }
        public Nullable<DateTime> DateOfJoin { get; set; }

        public string   BankCode { get; set; }
        public string   BranchCode { get; set; }

        public Dictionary<TeCommissionAgentsAnalyzeFilter, string> Errors { get; set; }

        public DateTime DOBBoundryDate { get; set; }
        public TcBindingList<TcCommissionAgentsMasterRow> DuplicateMasterRows { get; set; }

        public string   TLorBPO { get; set; }

        // Added for EPF, ETF suppport
        public string   Initials { get; set; }
        public string   LastName { get; set; }
        public string   EmployeeNumber { get; set; }
        public decimal  EPFContribution { get; set; }
        public decimal  ETFContribution { get; set; }
        public string   MemberStatus { get; set; }
        public decimal  DaysWorked { get; set; }
        public decimal  EPFDeduction { get; set; }
        public decimal  GrossCommission { get; set; }
        public string   OCGrade { get; set; }

        //Added for PAYE
        public decimal Paye { get; set; }

        public TcCommissionAgentsAnalyzedRow()
        {
            Errors = new Dictionary<TeCommissionAgentsAnalyzeFilter, string>();
            DuplicateMasterRows = new TcBindingList<TcCommissionAgentsMasterRow>();
        }

        public decimal Amount
        {
            get { return NetCommission - Paye - Hold; }
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

        public bool HasError(TeCommissionAgentsAnalyzeFilter error)
        {
            return Errors.ContainsKey(error);
        }

        public string GetErrors()
        {
            string errors = string.Empty;

            int i = 1;
            foreach (KeyValuePair<TeCommissionAgentsAnalyzeFilter, string> pair in Errors)
            {
                errors += string.Format("({0}) {1}\n", i, pair.Value);
                i++;
            }

            return errors;
        }

        public string[] GetSearchableFields()
        {
            string[] fields = { VirtualNumber, NIC, DestinationAccount, DestinationAccountName,
                                  Bank, Branch, BankCode.ToString(), BranchCode.ToString() };

            return fields;
        }

        public TcPayMasterDestinationData GetPayMasterDestinationData()
        {
            TcPayMasterDestinationData destination = new TcPayMasterDestinationData();

            destination.LineNumber  = LineNumber;
            destination.Amount      = Amount;

            destination.DestinationBank         = BankCode;
            destination.DestinationBranch       = BranchCode;
            destination.DestinationAccount      = DestinationAccount;
            destination.DestinationAccountName  = DestinationAccountName;
            destination.Particulars             = NIC;

            return destination;
        }


        public virtual TcEpfDestinationData GetEpfDestinationData()
        {
            TcEpfDestinationData row = new TcEpfDestinationData();

            row.NICNumber               = NIC;
            row.Initials                = Initials;
            row.LastName                = LastName;
            row.EmployersContribution   = EPFContribution;
            row.MembersContribution     = EPFDeduction;
            row.TotalContribution       = row.EmployersContribution + row.MembersContribution;
            row.TotalEarnings           = GrossCommission;
            row.MemberStatus            = MemberStatus;
            row.MemberNumber            = EmployeeNumber;
            row.DaysOfWork              = DaysWorked;
            row.OccupationClassificationGrade = OCGrade;

            return row;
        }

        public virtual TcEtfDetailDestinationData GetEtfDestinationData()
        {
            TcEtfDetailDestinationData row = new TcEtfDetailDestinationData();

            row.MemberNumber    = EmployeeNumber;
            row.Initials        = Initials;
            row.Surname         = LastName;
            row.NICNumber       = NIC;
            row.TotalContribution = ETFContribution;

            return row;
        }
    }
}
