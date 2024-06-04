using HotelApi.Models;

namespace HotelApi.DAL
{
    public class BookRoomCRUD
    {

        public Serilog.ILogger _logger { get; }

        public BookRoomCRUD(Serilog.ILogger logger)
        {
            _logger = logger;
        }


        internal CRUDresult SetBookRoom(Reservation reservation)
        {
            using (var context = new HotelContext())
            {
                try
                {
                    context.Reservations.Add(reservation);

                    var rows = context.SaveChanges();
                    _logger.Information($"Se registro la reservacion {reservation.ReservationId}");
                    return new CRUDresult() { Message = "OK", Result = true };
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, ex.Message);
                    return new CRUDresult() { Message = "Hubo un problema al registrar la reservacion.", Result = false };
                }
            }
        }
    }
}
