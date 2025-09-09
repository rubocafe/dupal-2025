using DUPALPayroll.UI.Common.Epf;
using DUPALPayroll.UI.Common.Etf;

// Harshan Nishantha
// 2013-09-17

namespace DUPALPayroll.UI.Common.PayMaster
{
    public interface TiPayMasterDestination
    {
        TcPayMasterDestinationData GetPayMasterDestinationData();
        TcEpfDestinationData GetEpfDestinationData();
        TcEtfDetailDestinationData GetEtfDestinationData();
    }
}
