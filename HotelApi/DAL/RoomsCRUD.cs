using HotelApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelApi.DAL
{
    /// <summary>
    /// Clase para el manejo de los cuertos.
    /// </summary>
    internal class RoomsCRUD
    {
        public Serilog.ILogger _logger { get; }

        public RoomsCRUD(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Listado de cuertos
        /// </summary>
        /// <returns></returns>
        internal List<Room> GetRooms()
        {
            try
            {
                List<Room> result = new List<Room>();
                using (var context = new HotelContext())
                {
                    result = context.Rooms.Include(r => r.RoomType).ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new List<Room>();
            }
            
        }
    }
}
