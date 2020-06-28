using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19.Monitor.Sv.Gateways.BrasilIo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Covid19.Monitor.Sv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CasesController : ControllerBase
    {
        private readonly ILogger<CasesController> _logger;

        public CasesController(ILogger<CasesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Task<List<CasesMonthResult>> Get([FromServices] IBrasilIoGateway brasilIoGateway)
        {
            return brasilIoGateway.ListByRegionCodeAsync(string.Empty);
        }
        
        [HttpGet]
        [Route("{regionCode}")]
        public Task<List<CasesMonthResult>> Get([FromServices] IBrasilIoGateway brasilIoGateway,
                                                [FromRoute] string regionCode)
        {
            return brasilIoGateway.ListByRegionCodeAsync(regionCode);
        }
        
        [HttpGet]
        [Route("month")]
        public Task<List<GroupedCasesMonthResult>> Month([FromServices] IBrasilIoGateway brasilIoGateway)
        {
            return brasilIoGateway.ListByRegionCodeGroupByMonthAsync(string.Empty);
        }

        [HttpGet]
        [Route("{regionCode}/month")]
        public Task<List<GroupedCasesMonthResult>> Month([FromServices] IBrasilIoGateway brasilIoGateway,
                                                         [FromRoute] string regionCode)
        {
            return brasilIoGateway.ListByRegionCodeGroupByMonthAsync(regionCode);
        }
    }
}