using Microsoft.AspNetCore.Mvc;

namespace ChecklistAPI.Controllers
{
    public class TorreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
