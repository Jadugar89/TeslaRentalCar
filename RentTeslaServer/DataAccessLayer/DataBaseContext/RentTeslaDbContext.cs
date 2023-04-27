using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer.Entities;

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
                eb.Property(x => x.Cost).HasPrecision(10, 2);
            });

            modelBuilder.Entity<History>(eb =>
            {
                eb.HasIndex(x => x.Guid).IsUnique();
                eb.Property(x => x.Cost).HasPrecision(10, 2);
            });

            modelBuilder.Entity<Car>(eb =>
            {
                eb.HasOne(x => x.CarType)
                  .WithMany(x => x.Cars).HasForeignKey(x => x.CarTypeId);
                eb.HasOne(x => x.CarRental)
                  .WithMany(x => x.Cars).HasForeignKey(x => x.CarRentalId);
                eb.HasIndex(x => x.Plates).IsUnique();
                eb.Property(x=>x.DailyPrice).HasPrecision(10,2);
            });

            modelBuilder.Entity<CarRental>()
                .HasIndex(x=>x.Name).IsUnique();    
        }
    }
}
