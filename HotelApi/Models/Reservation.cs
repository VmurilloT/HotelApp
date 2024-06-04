using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace HotelApi.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public DateTime ReservationDate { get; set; }
        public Decimal Price { get; set; }
        public char Status { get; set; }
        public int GuestId { get; set; }
        public int NumberOfGuests { get; set; }
    }
}
