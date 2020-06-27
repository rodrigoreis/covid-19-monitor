using System.Collections.Generic;
using System.Threading.Tasks;

namespace Covid19.Monitor.Sv.Gateways.SerpApi
{
    public interface ISerpApiGateway
    {
        Task<List<ShoppingResult>> ListProductsAsync(string keywords);
    }
}