using Filminurk.Core.Domain;
using Filminurk.Core.Dto;
using Filminurk.Core.ServiceInterface;
using Filminurk.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.ApplicationServices.Services
{
    public class FavouriteListsServices : IFavouriteListsServices
    {
        private readonly FilminurkTARpe24Context _context;
        private readonly IFilesServices _filesServices;
        public FavouriteListsServices(FilminurkTARpe24Context context, IFilesServices filesServices)
        {
            _context=context;
            _filesServices=filesServices;
        }

        public async Task<FavouriteList> DetailsAsync(Guid id)
        {
            var result = await _context.FavouriteLists
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.FavouriteListID == id);
            return result;
        }
        public async Task<FavouriteList> Create(FavouriteListDTO dto/*, List<Movie>selectedMovies*/)
        {
            FavouriteList newList = new();
            newList.FavouriteListID = Guid.NewGuid();
            newList.ListName = dto.ListName;
            newList.ListDescription = dto.ListDescription;
            newList.ListCreatedAt = dto.ListCreatedAt;
            newList.ListModifiedAt = dto.ListModifiedAt;
            newList.ListDeletedAt = dto.ListDeletedAt;
            newList.ListOfMovies = dto.ListOfMovies;
            newList.ListBelongToUser = dto.ListBelongToUser;
            await _context.FavouriteLists.AddAsync(newList);
            await _context.SaveChangesAsync();

            //foreach (var movieid in selectedMovies)
            //{
              
            //}
            return newList;
            



        }
        public async Task<FavouriteList> Update(FavouriteListDTO updatedList)
        {
            FavouriteList updatedListInDB = new();

            updatedListInDB.FavouriteListID = updatedList.FavouriteListID;
            updatedListInDB.ListBelongToUser = updatedList.ListBelongToUser;
            updatedListInDB.IsMovieOrActor = updatedList.IsMovieOrActor;
            updatedListInDB.ListName = updatedList.ListName;
            updatedListInDB.ListDescription = updatedList.ListDescription;
            updatedListInDB.IsPrivate = updatedList.IsPrivate;
            updatedListInDB.ListOfMovies = updatedList.ListOfMovies;
            updatedListInDB.ListCreatedAt = updatedList.ListCreatedAt;
            updatedListInDB.ListDeletedAt = updatedList.ListDeletedAt;
            updatedListInDB.ListModifiedAt = updatedList.ListModifiedAt;
            _context.FavouriteLists.Attach(updatedListInDB);
            _context.Entry(updatedListInDB).Property(l => l.IsPrivate).IsModified = true;
            await _context.SaveChangesAsync();
            return updatedListInDB;
            
        }
    }
}
