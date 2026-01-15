using Filminurk.Core.Dto.OMDbDTOs;
using Filminurk.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filminurk.ApplicationServices.Services
{
    public class OMDbService : IOMDbServices
    {


        public async Task<OMDbApiDTO> OMDbApi(OMDbApiDTO dto)
        {
            string apikey = Filminurk.Data.Environment.omdbkey;

            var baseUrl = "http://www.omdbapi.com/?apikey=[7dbe858b]&";
            var omdbUrl = $"http://img.omdbapi.com/?apikey=[7dbe858b]&";
        }
    }
}
