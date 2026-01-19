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

            var baseUrl = "http://www.omdbapi.com/?apikey=[7dbe858b]&";
            var omdbUrl = $"http://img.omdbapi.com/?apikey=[7dbe858b]&";

           /* using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(omdbUrl);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
                var response = httpClient.GetAsync($"?q={dto.Title}&apikey={apikey}&details=true").GetAwaiter().GetResult();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    List<OMDbApiRootDTO> omdbData = JsonSerializer.Deserialize<List<OMDbApiRootDTO>>(jsonResponse);
                    //dto.Title = omdbData[0].
                    //dto.Year = omdbData[0].
                }
            }
            string omdbResponse = baseUrl + $"{dto.Year}?apikey={apikey}&metric=true";
            using (var clientOMDb = new HttpClient())
            {
                var httpResponseOMDb = clientOMDb.GetAsync(omdbResponse).GetAwaiter().GetResult();
                string jsonOMDb = await httpResponseOMDb.Content.ReadAsStringAsync();


            }*/
        }
    }
}
