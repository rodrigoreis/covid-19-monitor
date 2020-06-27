using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Covid19.Monitor.Sv.Gateways.SerpApi;
using Covid19.Monitor.Sv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Covid19.Monitor.Sv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupplyController : ControllerBase
    {
        private readonly ILogger<SupplyController> _logger;

        public SupplyController(ILogger<SupplyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Supply>> Get([FromServices] ISerpApiGateway serpApiGateway)
        {
            var result = await serpApiGateway.ListProductsAsync("alcool gel");

            return result.Select(p => new Supply
            {
                Type = SupplyType.Alcohol70,
                Name = p.Title,
                Vendor = p.Source,
                Price = p.ExtractedPrice,
                Link = p.Link
            });
        }
    }
}