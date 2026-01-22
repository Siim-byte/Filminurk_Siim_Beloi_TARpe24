using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class RChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
