using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19.Monitor.Sv.Gateways.BrasilIo
{
    public interface IBrasilIoGateway
    {
        Task<List<CasesMonthResult>> ListByRegionCodeAsync(string regionCode);
        Task<List<GroupedCasesMonthResult>> ListByRegionCodeGroupByMonthAsync(string regionCode);
    }
}