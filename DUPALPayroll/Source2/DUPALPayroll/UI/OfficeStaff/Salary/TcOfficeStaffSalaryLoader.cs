using DUPALPayroll.Library.Csv;
using DUPALPayroll.Library.Date;
using DUPALPayroll.UI.Common;
using DUPALPayroll.UI.Common.SalaryBean;
using System.Collections.Generic;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.OfficeStaff.Salary
{
    public class TcOfficeStaffSalaryLoader : TcSalaryLoader<TcOfficeStaffSalaryRow>
    {
        public TcOfficeStaffSalaryLoader(TcYearMonth salaryMonth)
            : base(TcPaths.OfficeStaffId, salaryMonth)
        {
            Init();
        }

        public override TcOfficeStaffSalaryRow New()
        {
            return new TcOfficeStaffSalaryRow();
        }

        protected override void AddMandatoryHeaders()
        {
            base.AddMandatoryHeaders();
        }

        protected override TcOfficeStaffSalaryRow Load(TcCsvDataRow row, Dictionary<string, int> headerIndexes)
        {
            TcOfficeStaffSalaryRow data = base.Load(row, headerIndexes);

            return data;
        }
    }
}
