using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.MasterBean;
using DUPALPayroll.UI.OfficeStaff.Salary;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.OfficeStaff.MasterData
{
    public class TcOfficeStaffMasterTable : TcSalaryMasterTable<TcOfficeStaffMasterRow>
    {
        public TcBindingList<TcOfficeStaffMasterRow> GetNICDuplicateRowsForEmployeesInSalaryFile(TcOfficeStaffSalaryTable salaryTable)
        {
            TcBindingList<TcOfficeStaffMasterRow> list = new TcBindingList<TcOfficeStaffMasterRow>();

            foreach (TcOfficeStaffSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcOfficeStaffMasterRow> duplicates = GetNICDuplicates(row.NIC);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcOfficeStaffMasterRow> GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(TcOfficeStaffSalaryTable salaryTable)
        {
            TcBindingList<TcOfficeStaffMasterRow> list = new TcBindingList<TcOfficeStaffMasterRow>();

            foreach (TcOfficeStaffSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcOfficeStaffMasterRow> duplicates = GetEmployeeNumberDuplicates(row.EmployeeNumber);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }
    }
}
