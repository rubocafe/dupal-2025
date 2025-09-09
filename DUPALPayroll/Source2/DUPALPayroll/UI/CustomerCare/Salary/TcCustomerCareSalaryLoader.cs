using DUPALPayroll.General;
using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.SalaryBean;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.CustomerCare.Salary
{
    public class TcCustomerCareSalaryLoader : TcSalaryLoader<TcCustomerCareSalaryRow>
    {
        public TcCustomerCareSalaryLoader(TcYearMonth workingYearMonth)
            : base("Customer Care", workingYearMonth)
        {
            Init();
        }

        public override TcCustomerCareSalaryRow New()
        {
            return new TcCustomerCareSalaryRow();
        }

        protected override void AddMandatoryHeaders()
        {
            base.AddMandatoryHeaders();

            mandatoryHeaderNames.Add("NO_PAY");
            mandatoryHeaderNames.Add("TBI");
            mandatoryHeaderNames.Add("PBI");

            if (TcVersions.IsCustomerCareFR001Supported(workingYearMonth))
            {
                mandatoryHeaderNames.Add("SALES_COMMISSION");
                mandatoryHeaderNames.Add("UPSELLING_AND_EBILLING_INCENTIVE");
            }
        }

        protected override TcCustomerCareSalaryRow Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcCustomerCareSalaryRow data = base.Load(row, headerIndexes);

            data.NoPay  = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["NO_PAY"]].Value);
            data.TBI    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["TBI"]].Value);
            data.PBI    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["PBI"]].Value);

            if (TcVersions.IsCustomerCareFR001Supported(workingYearMonth))
            {
                data.SalesCommission  = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["SALES_COMMISSION"]].Value);
                data.UpsellingAndEBillingIncentive  = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["UPSELLING_AND_EBILLING_INCENTIVE"]].Value);
            }

            return data;
        }
    }
}
