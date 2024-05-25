using HotelApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HotelApp.Services
{
    public class GuestService : ICRUD<GuestModel>
    {
        public Task<List<GuestModel>> GetData()
        {
            throw new NotImplementedException();
        }

        public async Task<httpResult> Register(GuestModel guest)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, @"https://localhost:7257/RegisterGhest");

            var client = new HttpClient();
            request.Content = new StringContent(JsonConvert.SerializeObject(guest), 
                                                Encoding.UTF8,
                                                "application/json");

            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
            
            var response = await client.SendAsync(request);
            var content  = await response.Content.ReadAsStringAsync();

            httpResult deserialicer = JsonConvert.DeserializeObject<httpResult>(content);

            return deserialicer??new httpResult();
        }

    }
}
