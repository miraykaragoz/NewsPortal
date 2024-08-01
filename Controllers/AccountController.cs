using Microsoft.AspNetCore.Mvc;

namespace NewsPortal.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
