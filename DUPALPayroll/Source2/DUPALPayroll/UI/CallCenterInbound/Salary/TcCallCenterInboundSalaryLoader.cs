using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.SalaryBean;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterInbound.Salary
{
    public class TcCallCenterInboundSalaryLoader : TcSalaryLoader<TcCallCenterInboundSalaryRow>
    {
        public TcCallCenterInboundSalaryLoader(TcYearMonth salaryMonth)
            : base("Call Center Inbound", salaryMonth)
        {
            Init();
        }

        public override TcCallCenterInboundSalaryRow New()
        {
            return new TcCallCenterInboundSalaryRow();
        }

        protected override void AddMandatoryHeaders()
        {
            base.AddMandatoryHeaders();

            mandatoryHeaderNames.Add("NO_PAY");
            mandatoryHeaderNames.Add("ATTENDANCE_INCENTIVE");
            mandatoryHeaderNames.Add("UPSELLING_INCENTIVE");
            mandatoryHeaderNames.Add("EBILLING_INCENTIVE");
        }

        protected override TcCallCenterInboundSalaryRow Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcCallCenterInboundSalaryRow data = base.Load(row, headerIndexes);

            data.NoPay                  = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["NO_PAY"]].Value);
            data.AttendanceIncentive    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["ATTENDANCE_INCENTIVE"]].Value);
            data.UpsellingIncentive     = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["UPSELLING_INCENTIVE"]].Value);
            data.EBillingIncentive      = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["EBILLING_INCENTIVE"]].Value);

            return data;
        }
    }
}
