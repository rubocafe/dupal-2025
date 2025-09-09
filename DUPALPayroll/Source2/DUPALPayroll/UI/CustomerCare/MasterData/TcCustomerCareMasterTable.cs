using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.MasterBean;
using DUPALPayroll.UI.CustomerCare.Salary;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.CustomerCare.MasterData
{
    public class TcCustomerCareMasterTable : TcSalaryMasterTable<TcCustomerCareMasterRow>
    {
        public TcBindingList<TcCustomerCareMasterRow> GetNICDuplicateRowsForEmployeesInSalaryFile(TcCustomerCareSalaryTable salaryTable)
        {
            TcBindingList<TcCustomerCareMasterRow> list = new TcBindingList<TcCustomerCareMasterRow>();

            foreach (TcCustomerCareSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcCustomerCareMasterRow> duplicates = GetNICDuplicates(row.NIC);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcCustomerCareMasterRow> GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(TcCustomerCareSalaryTable salaryTable)
        {
            TcBindingList<TcCustomerCareMasterRow> list = new TcBindingList<TcCustomerCareMasterRow>();

            foreach (TcCustomerCareSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcCustomerCareMasterRow> duplicates = GetEmployeeNumberDuplicates(row.EmployeeNumber);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }
    }
}
