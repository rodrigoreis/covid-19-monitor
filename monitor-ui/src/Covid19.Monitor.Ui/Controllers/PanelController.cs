using Microsoft.AspNetCore.Mvc;

namespace Covid19.Monitor.Ui.Controllers
{
    [Route("panel")]
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}