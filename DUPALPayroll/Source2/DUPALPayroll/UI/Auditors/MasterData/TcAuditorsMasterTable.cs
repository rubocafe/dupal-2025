using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.MasterBean;
using DUPALPayroll.UI.Auditors.Salary;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.Auditors.MasterData
{
    public class TcAuditorsMasterTable : TcSalaryMasterTable<TcAuditorsMasterRow>
    {
        public TcBindingList<TcAuditorsMasterRow> GetNICDuplicateRowsForEmployeesInSalaryFile(TcAuditorsSalaryTable salaryTable)
        {
            TcBindingList<TcAuditorsMasterRow> list = new TcBindingList<TcAuditorsMasterRow>();

            foreach (TcAuditorsSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcAuditorsMasterRow> duplicates = GetNICDuplicates(row.NIC);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcAuditorsMasterRow> GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(TcAuditorsSalaryTable salaryTable)
        {
            TcBindingList<TcAuditorsMasterRow> list = new TcBindingList<TcAuditorsMasterRow>();

            foreach (TcAuditorsSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcAuditorsMasterRow> duplicates = GetEmployeeNumberDuplicates(row.EmployeeNumber);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }
    }
}
