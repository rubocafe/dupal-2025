using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.MasterBean;
using DUPALPayroll.UI.CallCenterInbound.Salary;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterInbound.MasterData
{
    public class TcCallCenterInboundMasterTable : TcSalaryMasterTable<TcCallCenterInboundMasterRow>
    {
        public TcBindingList<TcCallCenterInboundMasterRow> GetNICDuplicateRowsForEmployeesInSalaryFile(TcCallCenterInboundSalaryTable salaryTable)
        {
            TcBindingList<TcCallCenterInboundMasterRow> list = new TcBindingList<TcCallCenterInboundMasterRow>();

            foreach (TcCallCenterInboundSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcCallCenterInboundMasterRow> duplicates = GetNICDuplicates(row.NIC);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcCallCenterInboundMasterRow> GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(TcCallCenterInboundSalaryTable salaryTable)
        {
            TcBindingList<TcCallCenterInboundMasterRow> list = new TcBindingList<TcCallCenterInboundMasterRow>();

            foreach (TcCallCenterInboundSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcCallCenterInboundMasterRow> duplicates = GetEmployeeNumberDuplicates(row.EmployeeNumber);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }
    }
}
