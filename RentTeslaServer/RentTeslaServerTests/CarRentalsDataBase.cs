using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RentTeslaServer;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentTeslaServerTests
{
    public class CarRentalsDataBase : IDisposable
    {
      
        public RentTeslaDbContext Context { get; private set; }

        public CarRentalsDataBase()
        {
         var options = new DbContextOptionsBuilder<RentTeslaDbContext>()
                .UseInMemoryDatabase(databaseName: "RentTeslaDb")
                .Options;

            Context = new RentTeslaDbContext(options);
            new RentTeslaSeeder(Context).Seed();
            AddResevation();
        }

        private void AddResevation()
        {

            var reservations = new List<Reservation>{
                new Reservation()
                {
                     CarId = 1,
                     Cost = 500,
                     Email = "First@wp.pl",
                     PickUpDate=new DateTime(2023, 4, 1, 0, 0, 0),
                     ReturnDate = new DateTime(2023, 4, 7, 0, 0, 0),
                     PickUpLocationId = 1,
                     ReturnLocationId = 1,
                },

                new Reservation()
                {
                     CarId = 1,
                     Cost = 500,
                     Email = "Jasiu@wp.pl",
                     PickUpDate=new DateTime(2023, 4, 9, 0, 0, 0),
                     ReturnDate = new DateTime(2023, 4, 15, 0, 0, 0),
                     PickUpLocationId = 1,
                     ReturnLocationId = 1,
                },

                new Reservation()
                {
                     CarId = 14,
                     Cost = 500,
                     Email = "sdsa@wp.pl",
                     PickUpDate=new DateTime(2023, 4, 9, 0, 0, 0),
                     ReturnDate = new DateTime(2023, 4, 15, 0, 0, 0),
                     PickUpLocationId = 3,
                     ReturnLocationId = 1,
                }
           };


            Context.Reservations.AddRange(reservations);
            Context.SaveChanges();  
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }

    }

}

