using Microsoft.AspNetCore.Mvc;

namespace Covid19.Monitor.Ui.Controllers
{
    [Route("stay-home")]
    public class StayHomeController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}