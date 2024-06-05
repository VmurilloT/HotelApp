using System.Runtime.InteropServices.JavaScript;
using HotelApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.DAL
{
    public class BookRoomCrud(Serilog.ILogger logger)
    {
        private string _sqlQuery =
            @"SELECT [r].[RoomId], [r].[Name], [r].[PriceBase], [r].[RoomTypeId], [r].[Status] FROM [Room] AS [r] WHERE Not Exists (SELECT 1 FROM [Reservation] AS [RS] WHERE [r].[RoomId] = [RS].[RoomId] AND [RS].[CheckIn] >= @CheckInDate AND [RS].[CheckOut] <= @CheckOutDate AND [RS].[Status] = 'A');";
        
        private Serilog.ILogger Logger { get; } = logger;

        internal List<Room> GetFreeRooms(DateTime checkIn, DateTime checkOut)
        {
            using var context = new HotelContext();
            var availableRooms = context.Rooms.FromSqlRaw(_sqlQuery, 
                    new SqlParameter("@CheckInDate", checkIn.Date),
                    new SqlParameter("@CheckOutDate", checkOut.Date)).ToList();
            
            return availableRooms;
        }
        internal CRUDresult SetBookRoom(Reservation reservation)
        {
            using var context = new HotelContext();
            try
            {
                context.Reservations.Add(reservation);

                var rows = context.SaveChanges();
                Logger.Information($"Se registro la reservacion {reservation.ReservationId}");
                return new CRUDresult() { Message = "OK", Result = true };
            }
            catch (Exception ex)
            {
                Logger.Error(ex, ex.Message);
                return new CRUDresult() { Message = "Hubo un problema al registrar la reservacion.", Result = false };
            }
        }
    }
}
