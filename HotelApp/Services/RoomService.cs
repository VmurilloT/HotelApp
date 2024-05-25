using HotelApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Services
{
    class RoomService : ICRUD<RoomsModel>
    {
        public async Task<List<RoomsModel>> GetData()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, @"https://localhost:7257/GetRooms");

            var client = new HttpClient();


            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();

            List<RoomsModel> deserialicer = JsonConvert.DeserializeObject<List<RoomsModel>>(content);

            return deserialicer ?? new List<RoomsModel>();
            
        }

        public async Task<httpResult> Register(RoomsModel entity)
        {
            return new httpResult();
        }


    }
}
