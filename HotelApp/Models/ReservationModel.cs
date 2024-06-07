namespace HotelApp.Models;

public class ReservationModel
{
        public int ReservationId { get; set; } = 0;
        public int RoomId { get; set; } = 0;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now.Date;
        public Decimal Price { get; set; } = 0;
        public char Status { get; set; } = 'X';
        public int GuestId { get; set; } = 0;
        public int NumberOfGuests { get; set; } = 0;
}