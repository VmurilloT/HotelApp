using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class RoomTypesModel
    {
        public int RoomTypeId { get; set; }

        public string? Name { get; set; }

        public int PeopleMax { get; set; }

    }
}
