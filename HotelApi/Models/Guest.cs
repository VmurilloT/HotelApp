using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelApi.Models
{
    [Table("Guest")]
    public class Guest
    {
        [Key]
        public int GuestId {get; set;}

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
        
        public string? SecondLastName { get; set; }
        public string? Email { get; set;}
        public string Phone { get; set; } = string.Empty;
	        
    }
}
