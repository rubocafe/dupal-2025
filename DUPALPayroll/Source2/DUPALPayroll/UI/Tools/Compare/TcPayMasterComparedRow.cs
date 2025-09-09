using DUPALPayroll.UI.Common.PayMaster;
using System.Collections.Generic;
using System.Reflection;

// Harshan Nishatha
// 2013-08-30

namespace DUPALPayroll.UI.CommissionAgents.Tools.Compare
{
    public class TcPayMasterComparedRow : TcPayMasterRow
    {
        public Dictionary<TePayMasterCompareFilter, string> Errors { get; set; }
        public TcPayMasterRow SecondaryRow { get; set; }

        private TcPayMasterComparedRow()
        {
            Errors = new Dictionary<TePayMasterCompareFilter, string>();
        }

        public bool HasErrors
        {
            get
            {
                bool hasErrors = Errors.Count > 0 ? true : false;

                return hasErrors;
            }
        }

        public static TcPayMasterComparedRow Create(TcPayMasterRow primaryRow, TcPayMasterRow secondryRow)
        {
            TcPayMasterComparedRow newRow = new TcPayMasterComparedRow();

            PropertyInfo[] properties = typeof(TcPayMasterRow).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(newRow, property.GetValue(primaryRow));
                }
            }

            newRow.SecondaryRow = secondryRow;

            newRow.Compare();

            return newRow;
        }

        private void Compare()
        {
            Errors = new Dictionary<TePayMasterCompareFilter, string>();

            if (DestinationBank != SecondaryRow.DestinationBank)
            {
                string error = string.Format("Destination Banks are not matched. Primary Destination Bank: [{0}], Secondary Destination Bank: [{1}]",
                    DestinationBank, SecondaryRow.DestinationBank);
                Errors.Add(TePayMasterCompareFilter.Destination_Bank_is_Different, error);
            }

            if (DestinationBranch != SecondaryRow.DestinationBranch)
            {
                string error = string.Format("Destination Branches are not matched. Primary Destination Branch: [{0}], Secondary Destination Branch: [{1}]",
                    DestinationBranch, SecondaryRow.DestinationBranch);
                Errors.Add(TePayMasterCompareFilter.Destination_Branch_is_Different, error);
            }

            if (DestinationAccount != SecondaryRow.DestinationAccount)
            {
                string error = string.Format("Destination Accounts are not matched. Primary Destination Account: [{0}], Secondary Destination Account: [{1}]",
                    DestinationAccount, SecondaryRow.DestinationAccount);
                Errors.Add(TePayMasterCompareFilter.Destination_Account_is_Different, error);
            }

            if (DestinationAccountName.ToUpper() != SecondaryRow.DestinationAccountName.ToUpper())
            {
                string error = string.Format("Destination Account Names are not matched. Primary Destination Account Name: [{0}], Secondary Destination Account Name: [{1}]",
                    DestinationAccountName.ToUpper(), SecondaryRow.DestinationAccountName.ToUpper());
                Errors.Add(TePayMasterCompareFilter.Destination_Account_Name_is_Different, error);
            }

            if (CreditDebitCode != SecondaryRow.CreditDebitCode)
            {
                string error = string.Format("Credit Debit Codes are not matched. Primary Credit Debit Code: [{0}], Secondary Credit Debit Code: [{1}]",
                    CreditDebitCode, SecondaryRow.CreditDebitCode);
                Errors.Add(TePayMasterCompareFilter.Credit_Debit_Code_is_Different, error);
            }

            if (Amount != SecondaryRow.Amount)
            {
                string error = string.Format("Amounts are not matched. Primary Amount: [{0}], Secondary Amount: [{1}]",
                    AmountDecimal.ToString("N2"), SecondaryRow.AmountDecimal.ToString("N2"));
                Errors.Add(TePayMasterCompareFilter.Amount_is_Different, error);
            }

            if (OriginatingBank != SecondaryRow.OriginatingBank)
            {
                string error = string.Format("Originating Banks are not matched. Primary Originating Bank: [{0}], Secondary Originating Bank: [{1}]",
                    OriginatingBank, SecondaryRow.OriginatingBank);
                Errors.Add(TePayMasterCompareFilter.Originating_Bank_is_Different, error);
            }

            if (OriginatingBranch != SecondaryRow.OriginatingBranch)
            {
                string error = string.Format("Originating Branchs are not matched. Primary Originating Branch: [{0}], Secondary Originating Branch: [{1}]",
                    OriginatingBranch, SecondaryRow.OriginatingBranch);
                Errors.Add(TePayMasterCompareFilter.Originating_Branch_is_Different, error);
            }


            if (OriginatingAccount != SecondaryRow.OriginatingAccount)
            {
                string error = string.Format("Originating Accounts are not matched. Primary Originating Account: [{0}], Secondary Originating Account: [{1}]",
                    OriginatingAccount, SecondaryRow.OriginatingAccount);
                Errors.Add(TePayMasterCompareFilter.Originating_Account_is_Different, error);
            }

            if (OriginatingAccountName != SecondaryRow.OriginatingAccountName)
            {
                string error = string.Format("Originating Account Names are not matched. Primary Originating Account Name: [{0}], Secondary Originating Account Name: [{1}]",
                    OriginatingAccountName, SecondaryRow.OriginatingAccountName);
                Errors.Add(TePayMasterCompareFilter.Originating_Account_Name_is_Different, error);
            }

            if (Particulars != SecondaryRow.Particulars)
            {
                string error = string.Format("NICs are not matched. Primary NIC: [{0}], Secondary NIC: [{1}]",
                    Particulars, SecondaryRow.Particulars);
                Errors.Add(TePayMasterCompareFilter.NIC_is_Different, error);
            }

            if (Reference != SecondaryRow.Reference)
            {
                string error = string.Format("References are not matched. Primary Reference: [{0}], Secondary Reference: [{1}]",
                    Reference, SecondaryRow.Reference);
                Errors.Add(TePayMasterCompareFilter.Reference_is_Different, error);
            }

            if (ValueDate != SecondaryRow.ValueDate)
            {
                string error = string.Format("Value Dates are not matched. Primary Value Date: [{0}], Secondary Value Date: [{1}]",
                    ValueDate, SecondaryRow.ValueDate);
                Errors.Add(TePayMasterCompareFilter.Value_Date_is_Different, error);
            }
        }

        public string GetErrors()
        {
            string errors = string.Empty;

            int i = 1;
            foreach (KeyValuePair<TePayMasterCompareFilter, string> pair in Errors)
            {
                errors += string.Format("({0}) {1}\n", i, pair.Value);
                i++;
            }

            return errors;
        }

        public bool HasError(TePayMasterCompareFilter error)
        {
            return Errors.ContainsKey(error);
        }
    }
}
