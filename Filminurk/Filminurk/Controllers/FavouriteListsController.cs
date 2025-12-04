using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Filminurk.Models.FavouriteLists;
using Filminurk.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<IActionResult>Create(FavouriteListUserCreateViewModel model)
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
            newListDto.ListBelongToUser = Guid.NewGuid().ToString();
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
                var thismovie = (Movie)_context.Movies.Where(tm => tm.ID == movieId).ToList().First();
                listofmoviestoadd.Add((Movie)thismovie);
            }
            newListDto.ListOfMovies = listofmoviestoadd;

            var newList = await _favouriteListsServices.Create(newListDto/*, convertedIDs*/);
            if (newList == null)
            {
                return BadRequest();
            }
            return RedirectToAction("Index", vm);


        }
        [HttpGet]
        public async Task<IActionResult> UserDetails(Guid id, Guid thisuserid)
        {
            if (id == Guid.Empty || thisuserid == Guid.Empty)
                return BadRequest();

            var thisList = await _context.FavouriteLists
                .Where(tl => tl.FavouriteListID == id &&
                             tl.ListBelongToUser == thisuserid.ToString())
                .Select(stl => new FavouriteListUserDetailsViewModel
                {
                    FavouriteListID = stl.FavouriteListID,
                    ListBelongToUser = stl.ListBelongToUser,
                    IsMovieOrActor = stl.IsMovieOrActor,
                    ListName = stl.ListName,
                    ListDescription = stl.ListDescription,
                    IsPrivate = stl.IsPrivate,
                    ListOfMovies = stl.ListOfMovies,
                    IsReported = stl.IsReported,
                    ListCreatedAt = stl.ListCreatedAt,
                    ListModifiedAt = stl.ListModifiedAt,
                    ListDeletedAt = stl.ListDeletedAt,

                    //Image = _context.FilesToDatabase
                    //    .Where(i => i.ListID == stl.FavouriteListID)
                    //    .Select(si => new FavouriteListIndexImageViewModel
                    //    {
                    //        ImageID = si.ImageID,
                    //        ListID = si.ListID,
                    //        ImageData = si.ImageData,
                    //        ImageTitle = si.ImageTitle,
                    //        Image = "data:image/gif;base64," + Convert.ToBase64String(si.ImageData)/*      Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(si.ImageData))*/
                    //    }).FirstOrDefault()
                }).FirstOrDefaultAsync();

            if (thisList == null)
                return NotFound();

            return View("Details", thisList);
        }
        //[HttpGet]
        //public async Task<IActionResult> UserTogglePrivacy(Guid id, Guid thisuserid)
        //{
        //    if (id == Guid.Empty || thisuserid == Guid.Empty)
        //        return BadRequest();

        //    var thisList = await _context.FavouriteLists
        //        .Where(tl => tl.FavouriteListID == id &&
        //                     tl.ListBelongToUser == thisuserid.ToString())
        //        .Select(stl => new FavouriteListUserDetailsViewModel
        //        {
        //            FavouriteListID = stl.FavouriteListID,
        //            ListBelongToUser = stl.ListBelongToUser,
        //            IsMovieOrActor = stl.IsMovieOrActor,
        //            ListName = stl.ListName,
        //            ListDescription = stl.ListDescription,
        //            IsPrivate = stl.IsPrivate,
        //            ListOfMovies = stl.ListOfMovies,
        //            IsReported = stl.IsReported,
        //            ListCreatedAt = stl.ListCreatedAt,
        //            ListModifiedAt = stl.ListModifiedAt,
        //            ListDeletedAt = stl.ListDeletedAt,

        //            //Image = _context.FilesToDatabase
        //            //    .Where(i => i.ListID == stl.FavouriteListID)
        //            //    .Select(si => new FavouriteListIndexImageViewModel
        //            //    {
        //            //        ImageID = si.ImageID,
        //            //        ListID = si.ListID,
        //            //        ImageData = si.ImageData,
        //            //        ImageTitle = si.ImageTitle,
        //            //        Image = "data:image/gif;base64," + Convert.ToBase64String(si.ImageData)/*      Image = string.Format("data:image/gif;base64,{0}", Convert.ToBase64String(si.ImageData))*/
        //            //    }).FirstOrDefault()
        //        }).FirstOrDefaultAsync();

        //    if (thisList == null)
        //        return NotFound();

        //    return View("UserTogglePrivacy", thisList);
        //}
        [HttpPost]
        public async Task<IActionResult> UserTogglePrivacy(Guid id)
        {
            FavouriteList thisList = await _favouriteListsServices.DetailsAsync(id);

            FavouriteListDTO updatedList = new FavouriteListDTO();
            updatedList.FavouriteListID = thisList.FavouriteListID;
            updatedList.ListBelongToUser = thisList.ListBelongToUser;
            updatedList.IsMovieOrActor = thisList.IsMovieOrActor;
            updatedList.ListName = thisList.ListName;
            updatedList.ListDescription = thisList.ListDescription;
            updatedList.IsPrivate = !updatedList.IsPrivate;
            updatedList.ListOfMovies = thisList.ListOfMovies;
            updatedList.IsReported = thisList.IsReported;
            updatedList.ListCreatedAt = thisList.ListCreatedAt;
            updatedList.ListModifiedAt = DateTime.Now;
            updatedList.ListDeletedAt = updatedList.ListDeletedAt;

            //var result = await _favouriteListsServices.Update(updatedList);
            //if (result == null || result.IsPrivate != !result.IsPrivate)
            //{
            //    return BadRequest();
            //}
            await _context.SaveChangesAsync();
            return RedirectToAction("UserDetails",result.FavouriteListID);
            
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
