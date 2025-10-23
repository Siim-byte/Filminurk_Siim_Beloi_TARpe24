using System.IO;
using System.Reflection.Metadata.Ecma335;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Filminurk.Controllers
{
    public class MoviesController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IMovieServices _movieServices;
        private readonly IFilesServices _fileServices;
        public MoviesController
            (
                FilminurkTARpe24Context context,
                IMovieServices movieServices,
                IFilesServices filesServices
            )
        {
            _context = context;
            _movieServices = movieServices;
            _fileServices = filesServices;
        }
        public IActionResult Index()
        {
            var result = _context.Movies.Select(x => new MoviesIndexViewModel
            {
                ID = x.ID,
                Title = x.Title,
                FirstPublished = x.FirstPublished,
                CurrentRating = x.CurrentRating,
                DeadCounter = x.DeadCounter,
            });
            return View(result);
        }
        [HttpGet]
        public IActionResult Create()
        {
            MoviesCreateUpdateViewModel result = new();
            return View("CreateUpdate", result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(MoviesCreateUpdateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var dto = new MoviesDTO()
                {
                    ID = vm.ID,
                    Title = vm.Title,
                    Description = vm.Description,
                    FirstPublished = vm.FirstPublished,
                    Director = vm.Director,
                    Actors = vm.Actors,
                    CurrentRating = vm.CurrentRating,
                    DeadCounter = vm.DeadCounter,
                    AliveCounter = vm.AliveCounter,
                    ActorCounter = vm.ActorCounter,
                    EntryCreatedAt = vm.EntryCreatedAt,
                    EntryModifiedAt = vm.EntryModifiedAt,
                    Files = vm.Files,
                    FileToApiDTOs = vm.Images
                    .Select(x => new FileToApiDTO()
                    {
                        ImageID = x.ImageID,
                        ExistingFilePath = x.FilePath,
                        MovieID = x.MovieID,
                        IsPoster = x.IsPoster,
                    }).ToArray()
                };
                var result = await _movieServices.Create(dto);
                if (result == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            var vm = new MoviesDetailsViewModel();

            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.CurrentRating = movie.CurrentRating;
            vm.AliveCounter = movie.AliveCounter;
            vm.DeadCounter = movie.DeadCounter;
            vm.ActorCounter = movie.ActorCounter;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                    FilePath = y.ExistingFilePath,
                    ImageID = id
                }).ToArrayAsync();
            var vm = new MoviesCreateUpdateViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.CurrentRating = movie.CurrentRating;
            vm.AliveCounter = movie.AliveCounter;
            vm.DeadCounter = movie.DeadCounter;
            vm.ActorCounter = movie.ActorCounter;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.Images.AddRange(images);

            return View("CreateUpdate", vm);
        }
                
        [HttpPost]
        public async Task<IActionResult> Update(MoviesCreateUpdateViewModel vm)
        {
            var dto = new MoviesDTO()
            {
                ID = vm.ID,
                Title = vm.Title,
                Description = vm.Description,
                FirstPublished = vm.FirstPublished,
                Director = vm.Director,
                Actors = vm.Actors,
                CurrentRating = vm.CurrentRating,
                DeadCounter = vm.DeadCounter,
                AliveCounter = vm.AliveCounter,
                ActorCounter = vm.ActorCounter,
                EntryCreatedAt = vm.EntryCreatedAt,
                EntryModifiedAt = vm.EntryModifiedAt,
                Files = vm.Files,
                FileToApiDTOs = vm.Images
                .Select(x => new FileToApiDTO
                 {
                    ImageID = x.ImageID,
                    MovieID = x.MovieID,
                    ExistingFilePath = x.FilePath,
                 }).ToArray()
            };
            var result = await _movieServices.Update(dto);
            if (result == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var movie = await _movieServices.DetailsAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            var images = await _context.FilesToApi
                .Where(x => x.MovieID == id)
                .Select(y => new ImageViewModel
                {
                     FilePath = y.ExistingFilePath,
                     ImageID = y.ImageID,
                }).ToArrayAsync();


            var vm = new MoviesDeleteViewModel();
            vm.ID = movie.ID;
            vm.Title = movie.Title;
            vm.Description = movie.Description;
            vm.FirstPublished = movie.FirstPublished;
            vm.CurrentRating = movie.CurrentRating;
            vm.AliveCounter = movie.AliveCounter;
            vm.DeadCounter = movie.DeadCounter;
            vm.ActorCounter = movie.ActorCounter;
            vm.EntryCreatedAt = movie.EntryCreatedAt;
            vm.EntryModifiedAt = movie.EntryModifiedAt;
            vm.Director = movie.Director;
            vm.Actors = movie.Actors;
            vm.Images.AddRange(images);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var movie = await _movieServices.Delete(id);
            if (movie == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
