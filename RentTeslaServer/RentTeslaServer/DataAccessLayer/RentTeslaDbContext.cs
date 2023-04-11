using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using RentTeslaServer.DataAccessLayer.Entities;
using System.Reflection.Metadata;

namespace RentTeslaServer.DataAccessLayer
{
    public class RentTeslaDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarType> CarTypes { get; set; }

        public RentTeslaDbContext(DbContextOptions<RentTeslaDbContext> options) : base(options)
        {
           
        
             
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(eb =>
            {
                eb.HasOne(x => x.PickUpLocation)
                   .WithMany(x => x.PickupReservations)
                   .HasForeignKey(x => x.PickUpLocationId)
                    .OnDelete(DeleteBehavior.Restrict);
                eb.HasOne(x => x.ReturnLocation)
                  .WithMany(x => x.DropoffReservations)
                  .HasForeignKey(x => x.ReturnLocationId)
                  .OnDelete(DeleteBehavior.Restrict);
                eb.HasIndex(x => x.Guid).IsUnique();
            });

            modelBuilder.Entity<History>(eb =>
            {
                eb.HasIndex(x => x.Guid).IsUnique();
            });



            modelBuilder.Entity<Car>(eb =>
            {
                eb.HasOne(x => x.CarType)
                  .WithMany(x => x.Cars).HasForeignKey(x => x.CarTypeId);
                eb.HasOne(x => x.CarRental)
                  .WithMany(x => x.Cars).HasForeignKey(x => x.CarRentalId);
            });

            modelBuilder.Entity<CarRental>()
                .HasIndex(x=>x.Name).IsUnique();    

        }
    }
}
