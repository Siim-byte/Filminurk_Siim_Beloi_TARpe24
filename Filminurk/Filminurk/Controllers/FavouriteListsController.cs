using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.FavouriteLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;

namespace Filminurk.Controllers
{
    public class FavouriteListsController : Controller
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFavouriteListsServices _favouriteListsServices;
        //filservice add later
        public FavouriteListsController(FilminurkTARpe24Context context,
            IFavouriteListsServices favouriteListsServices)
        {
            _context = context;
            _favouriteListsServices = favouriteListsServices;
        }
        public IActionResult Index()
        {
            var resultingLists = _context.FavouriteLists
                .OrderByDescending(y => y.ListCreatedAt) //sorteeri nimekiri langevas järjekorras kuupäeva-kellaaja järgi
                .Select(x => new FavouriteListsIndexViewModel()
                {
                    FavouriteListID = x.FavouriteListID,
                    ListBelongToUser = x.ListBelongToUser,
                    IsMovieOrActor = x.IsMovieOrActor,
                    ListName = x.ListName,
                    ListDescription = x.ListDescription,
                    ListCreatedAt = x.ListCreatedAt,
                    Image = (List<FavouriteListIndexImageViewModel>)_context.FilesToDatabase
                    .Where(ml => ml.ListID == x.FavouriteListID)
                    .Select(li => new FavouriteListIndexImageViewModel
                    {
                        ListID = li.ListID,
                        ImageID = li.ImageID,
                        ImageData = li.ImageData,
                        ImageTitle = li.ImageTitle,
                        Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(li.ImageData)),
                    })
                });
            return View(resultingLists);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var movies = _context.Movies
                .OrderBy(m => m.Title)
                .Select(mo => new MoviesIndexViewModel
                {
                    ID = mo.ID,
                    Title = mo.Title,
                    FirstPublished = mo.FirstPublished,
                    DeadCounter = mo.DeadCounter,
                })
                .ToList();
            ViewData["allmovies"] = movies;
            ViewData["userHasSelected"] = new List<string>();
            FavouriteListUserCreateViewModel vm = new();
            return View("UserCreate", vm);
        }
        [HttpPost]
        public async Task<IActionResult> UserCreate(FavouriteListUserCreateViewModel vm, List<string> userHasSelected,
            List<MoviesIndexViewModel> movies)
        {
            List<Guid> tempParse = new();
            foreach (var stringID in userHasSelected)
            {
                tempParse.Add(Guid.Parse(stringID));
            }
            var newListDto = new FavouriteListDTO() { };
            newListDto.ListName = vm.ListName;
            newListDto.ListDescription = vm.ListDescription;
            newListDto.IsMovieOrActor = vm.IsMovieOrActor;
            newListDto.IsPrivate = (bool)vm.IsPrivate;
            newListDto.ListCreatedAt = DateTime.UtcNow;
            newListDto.ListBelongToUser = "00000000-0000-0000-0000-000000000001";
            newListDto.ListModifiedAt = DateTime.UtcNow;
            newListDto.ListDeletedAt = vm.ListDeletedAt;
            //List<Guid> convertedIDs = new List<Guid>();
            //if (newListDto.ListOfMovies != null)
            //{
            //    convertedIDs = MovieToID(newListDto.ListOfMovies);

            //}
            var listofmoviestoadd = new List<Movie>();
            foreach (var movieId in tempParse)
            {
                var thismovie = _context.Movies.Where(tm => tm.ID == movieId).ToArray().Take(1);
                listofmoviestoadd.Add((Movie)thismovie);
            }
            newListDto.ListOfMovies = listofmoviestoadd;

            var newList = await _favouriteListsServices.Create(newListDto/*, convertedIDs*/);
            if (newList != null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);


        }

        public List<Guid> MovieToID(List<Movie> listOfMovies)
        {
            var result = new List<Guid>();
            foreach (var movie in listOfMovies)
            {
                result.Add((Guid)movie.ID);
            }
            return result;
        }
    }
}
