using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskMaganer.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        public IActionResult Ola()
        {
            return View();
        }
    }
}
