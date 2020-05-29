using Microsoft.AspNetCore.Mvc;
using Eavesdropper.Ui.Models.GoogleApis;

namespace Eavesdropper.Ui.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("~/")]
        public IActionResult Index([FromServices] IGoogleDriveRepositoryModel repository)
        {
            return View();
        }
    }
}