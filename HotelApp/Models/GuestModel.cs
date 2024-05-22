using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class GuestModel
    {
        public int GuestId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? SecondLastName { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; } = string.Empty;
    }
}
