using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.SalaryBean;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.Salary
{
    public class TcAuditorsSalaryLoader : TcSalaryLoader<TcAuditorsSalaryRow>
    {
        public TcAuditorsSalaryLoader(TcYearMonth salaryMonth)
            : base(TcPaths.AuditorsId, salaryMonth)
        {
            Init();
        }

        public override TcAuditorsSalaryRow New()
        {
            return new TcAuditorsSalaryRow();
        }

        protected override void AddMandatoryHeaders()
        {
            base.AddMandatoryHeaders();

            mandatoryHeaderNames.Add("TRAVEL_ALLOWANCE");
            mandatoryHeaderNames.Add("TRAVEL_REIMBURSEMENT");
            mandatoryHeaderNames.Add("TRAVEL_INCENTIVE");
        }

        protected override TcAuditorsSalaryRow Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcAuditorsSalaryRow data = base.Load(row, headerIndexes);

            data.TravelAllowance       = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["TRAVEL_ALLOWANCE"]].Value);
            data.TravelReimbursement   = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["TRAVEL_REIMBURSEMENT"]].Value);
            data.TravelIncentive       = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["TRAVEL_INCENTIVE"]].Value);

            return data;
        }
    }
}
