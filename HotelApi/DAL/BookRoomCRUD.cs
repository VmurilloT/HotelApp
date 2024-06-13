using System.Diagnostics.CodeAnalysis;
using HotelApi.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.DAL
{
    [SuppressMessage("ReSharper", "FormatStringProblem")]
    public class BookRoomCrud(Serilog.ILogger logger)
    {
        private const string SqlQuery =
            @"Select [RoomId], [Name], [PriceBase], [RoomTypeId], [Status] From (Select [r].[RoomId], [r].[Name], [r].[PriceBase], [r].[RoomTypeId], [r].[Status]   From Room R 
            Where Not Exists (Select 1
                              From Reservation RS
                              Where R.RoomId = RS.RoomId
                                and @CheckInDate Between CheckIn and CheckOut
                                and RS.Status = 'A')) R
            Where Not Exists (Select 1
                              From Reservation RS
                              Where R.RoomId = RS.RoomId
                                and @CheckOutDate Between CheckIn and CheckOut
                                and RS.Status = 'A')and
        Not Exists (Select 1
                    From Reservation RS
                    Where R.RoomId = RS.RoomId
                      and (CheckIn Between @CheckInDate and @CheckOutDate
                          or CheckOut Between @CheckInDate and @CheckOutDate) 
                      and RS.Status = 'A')";
          

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
