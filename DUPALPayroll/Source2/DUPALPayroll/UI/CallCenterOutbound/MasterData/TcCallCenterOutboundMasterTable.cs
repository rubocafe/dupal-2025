using DUPALPayroll.Library;
using DUPALPayroll.UI.Common.MasterBean;
using DUPALPayroll.UI.CallCenterOutbound.Salary;

// Harshan Nishantha
// 2013-09-25

namespace DUPALPayroll.UI.CallCenterOutbound.MasterData
{
    public class TcCallCenterOutboundMasterTable : TcSalaryMasterTable<TcCallCenterOutboundMasterRow>
    {
        public TcBindingList<TcCallCenterOutboundMasterRow> GetNICDuplicateRowsForEmployeesInSalaryFile(TcCallCenterOutboundSalaryTable salaryTable)
        {
            TcBindingList<TcCallCenterOutboundMasterRow> list = new TcBindingList<TcCallCenterOutboundMasterRow>();

            foreach (TcCallCenterOutboundSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcCallCenterOutboundMasterRow> duplicates = GetNICDuplicates(row.NIC);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }

        public TcBindingList<TcCallCenterOutboundMasterRow> GetEmployeeNumberDuplicateRowsForEmployeesInSalaryFile(TcCallCenterOutboundSalaryTable salaryTable)
        {
            TcBindingList<TcCallCenterOutboundMasterRow> list = new TcBindingList<TcCallCenterOutboundMasterRow>();

            foreach (TcCallCenterOutboundSalaryRow row in salaryTable.All)
            {
                TcBindingList<TcCallCenterOutboundMasterRow> duplicates = GetEmployeeNumberDuplicates(row.EmployeeNumber);
                if (duplicates.Count > 0)
                {
                    list.Add(duplicates[0]);
                }
            }

            return list;
        }
    }
}
