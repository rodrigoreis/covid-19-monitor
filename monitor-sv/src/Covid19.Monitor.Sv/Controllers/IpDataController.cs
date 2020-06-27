using System.Threading.Tasks;
using Covid19.Monitor.Sv.Gateways.IpData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Covid19.Monitor.Sv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IpDataController : ControllerBase
    {
        private readonly ILogger<IpDataController> _logger;

        public IpDataController(ILogger<IpDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ip/{ip}")]
        public Task<IpDataInfo> Get([FromServices] IIpDataGateway ipDataGateway, [FromRoute] string ip)
        {
            return ipDataGateway.GetIpDataInfoAsync(ip);
        }
    }
}