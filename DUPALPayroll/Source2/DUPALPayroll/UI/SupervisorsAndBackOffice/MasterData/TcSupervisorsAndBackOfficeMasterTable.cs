using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.MasterBean;
using DUPALPayroll.UI.SupervisorsAndBackOffice.Salary;

// Harshan Nishantha
// 2013-10-01

namespace DUPALPayroll.UI.SupervisorsAndBackOffice.MasterData
{
    public class TcSupervisorsAndBackOfficeMasterTable : TcSalaryMasterTable<TcSupervisorsAndBackOfficeMasterRow>
    {
        public TcBindingList<TcSupervisorsAndBackOfficeMasterRow> GetNICDuplicateRowsForEmployeesInSalaryFile(TcSupervisorsAndBackOfficeSalaryTable salaryTable)
        {
            TcBindingList<TcSupervisorsAndBackOfficeMasterRow> list = new TcBindingList<TcSupervisorsAndBackOfficeMasterRow>();

            foreach (TcSupervisorsAndBackOfficeSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcSupervisorsAndBackOfficeMasterRow> duplicates = GetNICDuplicates(row.NIC);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcSupervisorsAndBackOfficeMasterRow> GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(TcSupervisorsAndBackOfficeSalaryTable salaryTable)
        {
            TcBindingList<TcSupervisorsAndBackOfficeMasterRow> list = new TcBindingList<TcSupervisorsAndBackOfficeMasterRow>();

            foreach (TcSupervisorsAndBackOfficeSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcSupervisorsAndBackOfficeMasterRow> duplicates = GetEmployeeNumberDuplicates(row.EmployeeNumber);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }
    }
}
