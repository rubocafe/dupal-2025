using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common.SalaryBean;
using System;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-10-01

namespace DUPALPayroll.UI.SupervisorsAndBackOffice.Salary
{
    public class TcSupervisorsAndBackOfficeSalaryLoader : TcSalaryLoader<TcSupervisorsAndBackOfficeSalaryRow>
    {
        public TcSupervisorsAndBackOfficeSalaryLoader(TcYearMonth workingYearMonth)
            : base("Supervisors And Back Office", workingYearMonth)
        {
            Init();
        }

        public override TcSupervisorsAndBackOfficeSalaryRow New()
        {
            return new TcSupervisorsAndBackOfficeSalaryRow();
        }

        protected override void AddMandatoryHeaders()
        {
            base.AddMandatoryHeaders();

            mandatoryHeaderNames.Add("OT_NORMAL");
            mandatoryHeaderNames.Add("OT_DOUBLE");
            mandatoryHeaderNames.Add("PBI");
            mandatoryHeaderNames.Add("WORK_TRAVEL_ALLOWANCE");
        }

        protected override TcSupervisorsAndBackOfficeSalaryRow Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcSupervisorsAndBackOfficeSalaryRow data = base.Load(row, headerIndexes);

            data.OTNormal               = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["OT_NORMAL"]].Value);
            data.OTDouble               = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["OT_DOUBLE"]].Value);
            data.PBI                    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["PBI"]].Value);
            data.WorkTravelAllowance    = TcCsvValueDecorder.GetDecimal(row.Fields[headerIndexes["WORK_TRAVEL_ALLOWANCE"]].Value);

            return data;
        }
    }
}
