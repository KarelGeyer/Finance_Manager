using Common.Enums;
using Common.Models.ProductModels.Properties;
using Common.Models.UI.Property;
using Common.Response;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebUI.Controllers
{
    public class PortfolioController : Controller
    {
        HttpClient client = new HttpClient();

        public IActionResult Index()
        {
            return View();
        }
    }
}
