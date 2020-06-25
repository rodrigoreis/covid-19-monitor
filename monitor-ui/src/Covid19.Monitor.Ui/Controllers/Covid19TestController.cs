using Microsoft.AspNetCore.Mvc;

namespace Covid19.Monitor.Ui.Controllers
{
    [Route("covid-19-test")]
    public class Covid19TestController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}