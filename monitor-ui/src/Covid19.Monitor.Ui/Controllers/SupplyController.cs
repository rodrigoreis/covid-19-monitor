using Microsoft.AspNetCore.Mvc;

namespace Covid19.Monitor.Ui.Controllers
{
    [Route("supply")]
    public class SupplyController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}