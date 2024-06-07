using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Documents;
using HotelApp.Models;
using Newtonsoft.Json;

namespace HotelApp.Services;

public class BookRoomService : ICRUD<ReservationModel>
{
    bool isbusy = false;
    public Task<httpResult> Register(ReservationModel entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReservationModel>> GetData()
    {
        throw new NotImplementedException();
    }
    /// <summary>
    /// Obtener cuertos disponibles
    /// </summary>
    /// <param name="parameters">Lista de parametros para el filtrado de la informacion</param>
    /// <value>Item 1 {Fecha_CheckIn}, </value>
    /// <value>Item 2 {Fecha_CheckCheckOut}</value>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<object> GetData(List<object> parameters)
    {
        if (isbusy) return new List<RoomsModel>();
        isbusy = true;
        ReservationModel reservation = new ReservationModel()
        {
            CheckIn = (DateTime)parameters[0],
            CheckOut = (DateTime)parameters[1]
        };
        var request = new HttpRequestMessage(HttpMethod.Get, @"https://localhost:7257/GetFreeRooms");
        request.Content = new StringContent(JsonConvert.SerializeObject(reservation), 
            Encoding.UTF8,
            "application/json");
        
        var client = new HttpClient();
        client.DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));


        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        var deserializer = JsonConvert.DeserializeObject<List<RoomsModel>>(content);
        isbusy = false;
        return deserializer ?? [];
    }
}