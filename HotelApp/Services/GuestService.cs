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
    internal class GuestService
    {
        internal httpResult RegisterGuest(GuestModel guest)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, @"https://localhost:7257/RegisterGhest");

            var client = new HttpClient();
            request.Content = new StringContent(JsonConvert.SerializeObject(guest), 
                                                Encoding.UTF8,
                                                "application/json");

            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
            
            var response = client.SendAsync(request).Result;
            var content  = response.Content.ReadAsStringAsync().Result;

            httpResult deserialicer = JsonConvert.DeserializeObject<httpResult>(content);

            return deserialicer??new httpResult();
        }       

    }
}
