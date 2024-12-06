using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
