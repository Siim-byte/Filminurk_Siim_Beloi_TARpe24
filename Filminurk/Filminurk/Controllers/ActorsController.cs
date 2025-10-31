using Filminurk.Data;
using Filminurk.Models.Actors;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        
        public ActorsController(FilminurkTARpe24Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Actors.Select(x => new ActorsIndexViewModel()
            {
                ActorID = x.ActorID,
                NickName = x.NickName,
                HomeCity = x.HomeCity,
                HomeRegion = x.HomeRegion,


            });
            return View(result);
        }
    }
}
