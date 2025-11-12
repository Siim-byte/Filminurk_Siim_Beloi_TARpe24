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
            var result = _context.Actors
                .OrderByDescending(x => x.EntryCreatedAt) 
                .Select(x => new ActorsIndexViewModel()
                {
                    ActorID = x.ActorID,
                    NickName = x.NickName,
                    HomeCity = x.HomeCity,
                    HomeRegion = x.HomeRegion,
                }).ToList(); 

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
                    ActorID = Guid.NewGuid(),
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    NickName = vm.NickName,
                    MoviesActedFor = vm.MoviesActedFor,
                    PortraitID = vm.PortraitID,
                    HomeCountry = vm.HomeCountry,
                    HomeCity = vm.HomeCity,
                    HomeRegion = vm.HomeRegion,
                    EntryCreatedAt = DateTime.Now,
                    EntryModifiedAt = DateTime.Now
                };

                var result = await _actorsServices.Create(dto);

                if (result == null)
                { 
                    return View("CreateUpdate", vm);
                }

                return RedirectToAction(nameof(Index));
            }
            return View("CreateUpdate", vm);
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var actors = await _actorsServices.DetailsAsync(id);
            if (actors == null)
            {
                return NotFound();
            }
            var vm = new ActorsDetailsViewModel();
            vm.ActorID = actors.ActorID;
            vm.FirstName = actors.FirstName;
            vm.LastName = actors.LastName;
            vm.NickName = actors.NickName;
            vm.MoviesActedFor = actors.MoviesActedFor;
            vm.PortraitID = actors.PortraitID;
            vm.HomeCountry = actors.HomeCountry;
            vm.HomeCity = actors.HomeCity;
            vm.HomeRegion = actors.HomeRegion;
            vm.EntryCreatedAt = actors.EntryCreatedAt;
            vm.EntryModifiedAt = actors.EntryModifiedAt;
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var actors = await _actorsServices.DetailsAsync(id);

            if (actors == null)
            {
                return NotFound();
            }
            var vm = new ActorsCreateUpdateViewModel();
            vm.ActorID = actors.ActorID;
            vm.FirstName = actors.FirstName;
            vm.LastName = actors.LastName;
            vm.NickName = actors.NickName;
            vm.MoviesActedFor = actors.MoviesActedFor;
            vm.PortraitID = actors.PortraitID;
            vm.HomeCountry = actors.HomeCountry;
            vm.HomeCity = actors.HomeCity;
            vm.HomeRegion = actors.HomeRegion;
            vm.EntryCreatedAt = actors.EntryCreatedAt;
            vm.EntryModifiedAt = actors.EntryModifiedAt;
            return View("CreateUpdate", vm);
        }
    }
}
