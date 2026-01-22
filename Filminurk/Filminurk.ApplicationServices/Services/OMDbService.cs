using Filminurk.Core.Dto.OMDbDTOs;
using Filminurk.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Filminurk.ApplicationServices.Services
{
    public class OMDbService : IOMDbServices
    {


        public async Task<OMDbApiResultDTO> OMDbApi(OMDbApiResultDTO dto)
        {
            string apikey = Filminurk.Data.Environment.omdbkey;

            string omdbResponse = $"http://www.omdbapi.com/?t={dto.Title}&y={dto.Year}&apikey={apikey}";

            using (var clientOMDb = new HttpClient())
            {
                var httpResponseOMDb = await clientOMDb.GetAsync(omdbResponse);
                string jsonOMDb = await httpResponseOMDb.Content.ReadAsStringAsync();

                try
                {
                    // Deserialize JSON into your DTO
                    var omdbData = JsonSerializer.Deserialize<OMDbApiResultDTO>(jsonOMDb);

                    // Return the data (or merge with input dto if needed)
                    return omdbData;
                }
                catch
                {
                    // Handle errors (return the original dto if deserialization fails)
                    return dto;
                }
            }
        }

    }
}
