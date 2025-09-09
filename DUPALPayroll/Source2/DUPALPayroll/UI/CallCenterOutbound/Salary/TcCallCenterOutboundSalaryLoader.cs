using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.SalaryBean;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterOutbound.Salary
{
    public class TcCallCenterOutboundSalaryLoader : TcSalaryLoader<TcCallCenterOutboundSalaryRow>
    {
        public TcCallCenterOutboundSalaryLoader(TcYearMonth workingYearMonth)
            : base("Call Center Outbound", workingYearMonth)
        {
            Init();
        }

        public override TcCallCenterOutboundSalaryRow New()
        {
            return new TcCallCenterOutboundSalaryRow();
        }

        protected override void AddMandatoryHeaders()
        {
            base.AddMandatoryHeaders();

            mandatoryHeaderNames.Add("OT_NORMAL");
            mandatoryHeaderNames.Add("OT_DOUBLE");
            mandatoryHeaderNames.Add("NO_PAY");
            mandatoryHeaderNames.Add("ATTENDANCE_INCENTIVE");
            mandatoryHeaderNames.Add("PBI");
            mandatoryHeaderNames.Add("UPSELLING_AND_EBILLING_INCENTIVE");
        }

        protected override TcCallCenterOutboundSalaryRow Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcCallCenterOutboundSalaryRow data = base.Load(row, headerIndexes);

            data.OTNormal               = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["OT_NORMAL"]].Value);
            data.OTDouble               = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["OT_DOUBLE"]].Value);
            data.NoPay                  = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["NO_PAY"]].Value);
            data.AttendanceIncentive    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["ATTENDANCE_INCENTIVE"]].Value);
            data.PBI                    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["PBI"]].Value);
            data.UpsellingAndEBillingIncentive = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["UPSELLING_AND_EBILLING_INCENTIVE"]].Value);

            return data;
        }
    }
}
