using Microsoft.AspNetCore.Mvc;

namespace Covid19.Monitor.Ui.Controllers
{
    [Route("medical-consultation")]
    public class MedicalConsultationController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}