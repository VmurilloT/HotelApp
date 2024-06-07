using System.Diagnostics.CodeAnalysis;
using HotelApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.DAL
{
    [SuppressMessage("ReSharper", "FormatStringProblem")]
    public class BookRoomCrud(Serilog.ILogger logger)
    {
        private const string SqlQuery = @"SELECT [r].[RoomId], [r].[Name], [r].[PriceBase], [r].[RoomTypeId], [r].[Status] FROM [Room] AS [r] WHERE Not Exists (SELECT 1 FROM [Reservation] AS [RS] WHERE [r].[RoomId] = [RS].[RoomId] AND [RS].[CheckIn] >= @CheckInDate AND ([RS].[CheckOut] <= @CheckOutDate AND [RS].[CheckOut] >= @CheckOutDate) AND [RS].[Status] = 'A');";

        private Serilog.ILogger Logger { get; } = logger;

        internal List<Room> GetFreeRooms(DateTime checkIn, DateTime checkOut)
        {
            if (checkIn.Year < 2024) checkIn = DateTime.Now;
            using var context = new HotelContext();
            var availableRooms = context.Rooms.FromSqlRaw(SqlQuery, 
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

                context.SaveChanges();
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
