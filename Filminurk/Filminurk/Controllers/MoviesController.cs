using Filminurk.Data;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        public IActionResult Index()
        {
            return View();
        }
    }
}
