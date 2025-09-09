 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Harshan Nishantha
// 2013-09-05

namespace DUPALPayroll.UI.CommissionAgents.MasterData
{
    public enum TeCommissionAgentsMasterFilter
    {
        All,
        NIC_Duplicates,
        Virtual_Number_Duplicates,
        NIC_Empty,
        Virtual_Number_Empty,
        Duplicate_Virtual_Numbers_for_Agents_in_Commissions_File,
        Duplicate_NICs_for_Agents_in_Commissions_File,
        Duplicate_NICs_with_Empty_Virtual_Numbers_for_Agents_in_Commissions_File
    }
}
