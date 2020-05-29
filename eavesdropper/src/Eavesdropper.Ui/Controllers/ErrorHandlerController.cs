using System.Diagnostics;
using Eavesdropper.Ui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eavesdropper.Ui.Controllers
{
    public class ErrorHandlerController : Controller
    {
        [HttpGet("/bad-server-no-donut-for-you")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}