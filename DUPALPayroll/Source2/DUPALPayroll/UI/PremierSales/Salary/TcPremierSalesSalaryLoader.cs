using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.SalaryBean;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.PremierSales.Salary
{
    public class TcPremierSalesSalaryLoader : TcSalaryLoader<TcPremierSalesSalaryRow>
    {
        public TcPremierSalesSalaryLoader(TcYearMonth workingYearMonth)
            : base("Premier Sales", workingYearMonth)
        {
            BasicSalaryHeader = "BASIC_REMUNERATION";
            GrossSalaryHeader = "GROSS_REMUNERATION";
            NetSalaryHeader   = "NET_REMUNERATION";

            Init();
        }

        public override TcPremierSalesSalaryRow New()
        {
            return new TcPremierSalesSalaryRow();
        }

        protected override void AddMandatoryHeaders()
        {
            base.AddMandatoryHeaders();

            mandatoryHeaderNames.Add("SALES_COMMISSIONS");
            mandatoryHeaderNames.Add("PAYMENT");
            mandatoryHeaderNames.Add("COMMISSION_ADVANCE");
        }

        protected override TcPremierSalesSalaryRow Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcPremierSalesSalaryRow data = base.Load(row, headerIndexes);

            data.SalesCommissions   = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["SALES_COMMISSIONS"]].Value);
            data.Payment            = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["PAYMENT"]].Value);
            data.CommissionAdvance  = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["COMMISSION_ADVANCE"]].Value);

            return data;
        }
    }
}
