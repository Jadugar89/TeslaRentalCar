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

namespace RentTeslaServerTests.ServicesTests.Fixture
{
    public class DataBaseFixture : IDisposable
    {

        public RentTeslaDbContext Context { get; private set; }

        public DataBaseFixture()
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
                     PickUpDate= DateTime.Now.Subtract(TimeSpan.FromDays(10)),
                     ReturnDate = DateTime.Now.Subtract(TimeSpan.FromDays(5)),
                     PickUpLocationId = 1,
                     ReturnLocationId = 1,
                },

                new Reservation()
                {
                     CarId = 1,
                     Cost = 500,
                     Email = "Jasiu@wp.pl",
                     PickUpDate=DateTime.Now.AddDays(9),
                     ReturnDate = DateTime.Now.AddDays(15),
                     PickUpLocationId = 1,
                     ReturnLocationId = 1,
                },

                new Reservation()
                {
                     CarId = 14,
                     Cost = 500,
                     Email = "sdsa@wp.pl",
                     PickUpDate=DateTime.Now.AddDays(9),
                     ReturnDate =DateTime.Now.AddDays(15),
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

