using HotelApi.Models;

namespace HotelApi.DAL
{
    internal class GuestCRUD
    {
        public Serilog.ILogger _logger { get; }

        public GuestCRUD(Serilog.ILogger logger)
        {
            _logger = logger;
        }

        internal List<Guest> GetGuest()
        {
            List<Guest> result = new List<Guest>();
            using (var context = new HotelContext())
            {
                result = context.Guests.ToList();
            }
            return result;
        }

        /// <summary>
        /// Register a new Guest
        /// </summary>
        /// <param name="guest"></param>
        /// <returns></returns>
        internal CRUDresult SetGuest(Guest guest) 
        {
            try
            {
                using (var context = new HotelContext())
                {
                    context.Guests.Add(new Guest()
                    {
                        Email = guest.Email,
                        LastName = guest.LastName,
                        Name = guest.Name,
                        SecondLastName = guest.SecondLastName,
                        Phone = guest.Phone
                    });
                    context.SaveChanges();
                }
                _logger.Information($"Se registro el huesped {guest.Phone.PadRight(5)}");
                return new CRUDresult { Result = true };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new CRUDresult { Result = true, Message = "Ocurrio un error al registrar al huesped." };
            }
        }
    }
}
