using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApi.Models
{
    [Table("Room")]
    public class Room
    {
        [Key]
        public int RoomId {set;get;}
        public string Name{ set; get; } = string.Empty;

        public int RoomTypeId { set; get; }
        public int Status { set; get; }
        public decimal PriceBase { set; get; }
        public RoomTypes RoomType { set; get; }
    }
}
