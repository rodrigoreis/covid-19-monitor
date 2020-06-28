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
        private const string Alcohol70 = "alcool gel";
        private const string ProtectiveMask = "mascaras protetoras";
        private const string LatexGlove = "luvas de latex";

        private readonly ILogger<SupplyController> _logger;

        public SupplyController(ILogger<SupplyController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Supply>> Get([FromServices] ISerpApiGateway serpApiGateway)
        {
            var alcoholResult = await serpApiGateway.ListProductsAsync(Alcohol70);
            var protectiveMaskResult = await serpApiGateway.ListProductsAsync(ProtectiveMask);
            var latexGloveResult = await serpApiGateway.ListProductsAsync(LatexGlove);

            var mergedResult = new List<Supply>();

            static Supply CreateSupplyObject(SupplyType type, ShoppingResult result)
            {
                return new Supply
                {
                    SupplyId = result.ProductId,
                    Type = type,
                    Name = result.Title,
                    Vendor = result.Source,
                    Price = result.ExtractedPrice,
                    Link = result.Link,
                    Thumbnail = result.Thumbnail
                };
            }

            mergedResult.AddRange(alcoholResult.Select(p =>
                CreateSupplyObject(SupplyType.Alcohol70, p)));

            mergedResult.AddRange(protectiveMaskResult.Select(p =>
                CreateSupplyObject(SupplyType.ProtectiveMask, p)));

            mergedResult.AddRange(latexGloveResult.Select(p =>
                CreateSupplyObject(SupplyType.LatexGlove, p)));

            return mergedResult;
        }
    }
}