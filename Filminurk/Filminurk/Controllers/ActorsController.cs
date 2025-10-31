using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Actors;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class ActorsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IActorsServices _actorsServices;
        
        public ActorsController
            (
            FilminurkTARpe24Context context,
            IActorsServices actorsServices
            )
        {
            _context = context;
            _actorsServices = actorsServices;
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
        [HttpGet]
        public IActionResult Create()
        {
            ActorsCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ActorsCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new ActorsDTO()
                {
                    ActorID = (Guid)vm.ActorID,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    NickName= vm.NickName,
                    MoviesActedFor = vm.MoviesActedFor,
                    PortraitID = vm.PortraitID,
                    HomeCountry = vm.HomeCountry,
                    HomeCity = vm.HomeCity,
                    HomeRegion = vm.HomeRegion,
                    EntryCreatedAt = vm.EntryCreatedAt,
                    EntryModifiedAt = vm.EntryModifiedAt

                };
                var result = await _actorsServices.Create(dto);
                if (result == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var actors = await _actorsServices.
        }
    }
}
