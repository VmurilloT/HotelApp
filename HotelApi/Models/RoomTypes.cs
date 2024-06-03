using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApi.Models
{
    [Table("RoomTypes")]
    public class RoomTypes
    {
        [Key]
        public int RoomTypeId { get; set; }
    
        public string? Name { get; set; }

        public int PeopleMax { get; set; }
    }
}
