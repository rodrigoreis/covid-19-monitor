using System.Threading.Tasks;

namespace Covid19.Monitor.Sv.Gateways.IpData
{
    public interface IIpDataGateway
    {
        Task<IpDataInfo> GetIpDataInfoAsync(string ip);
    }
}