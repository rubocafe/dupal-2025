using DUPALPayroll.Controls;
using DUPALPayroll.Library;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.CareTakers.MasterData;
using DUPALPayroll.UI.Common.PayMaster;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-10-31

namespace DUPALPayroll.UI.CareTakers.Analyze
{
    public class TcCareTakersAnalyzedRow : TiSearchable, TiPayMasterDestination
    {
        public int      LineNumber { get; set; }
        public string   SiteName { get; set; }
        public string   SiteCode { get; set; }
        public string   SiteEngineer { get; set; }
        public string   Bank { get; set; }
        public string   Branch { get; set; }
        public string   DestinationAccount { get; set; }
        public string   DestinationAccountName { get; set; }
        public string   NIC { get; set; }
        public decimal  Payment { get; set; }
        public decimal  Hold { get; set; }

        public string   BankCode { get; set; }
        public string   BranchCode { get; set; }

        public Dictionary<TeCareTakersAnalyzeFilter, string> Errors { get; set; }

        public DateTime DOBBoundryDate { get; set; }
        public TcBindingList<TcCareTakersMasterRow> DuplicateMasterRows { get; set; }

        public string TLorBPO { get; set; }

        public TcCareTakersAnalyzedRow()
        {
            Errors = new Dictionary<TeCareTakersAnalyzeFilter, string>();
            DuplicateMasterRows = new TcBindingList<TcCareTakersMasterRow>();
        }

        public decimal Amount
        {
            get { return Payment - Hold; }
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

        public bool HasError(TeCareTakersAnalyzeFilter error)
        {
            return Errors.ContainsKey(error);
        }

        public string GetErrors()
        {
            string errors = string.Empty;

            int i = 1;
            foreach (KeyValuePair<TeCareTakersAnalyzeFilter, string> pair in Errors)
            {
                errors += string.Format("({0}) {1}\n", i, pair.Value);
                i++;
            }

            return errors;
        }

        public string[] GetSearchableFields()
        {
            string[] fields = { NIC, DestinationAccount, DestinationAccountName,
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


        public Common.Epf.TcEpfDestinationData GetEpfDestinationData()
        {
            throw new NotImplementedException();
        }

        public Common.Etf.TcEtfDetailDestinationData GetEtfDestinationData()
        {
            throw new NotImplementedException();
        }
    }
}
