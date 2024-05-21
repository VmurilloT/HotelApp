using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace HotelApi.Models
{
    public class HotelContext : DbContext
    {
        
        public DbSet<Guest> Guests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("cnn"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
