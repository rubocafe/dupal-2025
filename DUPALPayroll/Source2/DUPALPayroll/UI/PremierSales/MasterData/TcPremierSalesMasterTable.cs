using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.MasterBean;
using DUPALPayroll.UI.PremierSales.Salary;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.PremierSales.MasterData
{
    public class TcPremierSalesMasterTable : TcSalaryMasterTable<TcPremierSalesMasterRow>
    {
        public TcBindingList<TcPremierSalesMasterRow> GetNICDuplicateRowsForEmployeesInSalaryFile(TcPremierSalesSalaryTable salaryTable)
        {
            TcBindingList<TcPremierSalesMasterRow> list = new TcBindingList<TcPremierSalesMasterRow>();

            foreach (TcPremierSalesSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcPremierSalesMasterRow> duplicates = GetNICDuplicates(row.NIC);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcPremierSalesMasterRow> GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(TcPremierSalesSalaryTable salaryTable)
        {
            TcBindingList<TcPremierSalesMasterRow> list = new TcBindingList<TcPremierSalesMasterRow>();

            foreach (TcPremierSalesSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcPremierSalesMasterRow> duplicates = GetEmployeeNumberDuplicates(row.EmployeeNumber);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }
    }
}
