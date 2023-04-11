using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;

namespace RentTeslaServer
{
    public class RentTeslaSeeder
    {
         class CarParameter
        {
           public int Type { get; set; }
           public double Price { get; set; }
        }

        private readonly RentTeslaDbContext dbContext;

        public RentTeslaSeeder(RentTeslaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Seed()
        {
            if (dbContext.Database.CanConnect())
            {
                if (!dbContext.CarRentals.Any())
                {
                    dbContext.CarTypes.AddRange(GetTypes());
                    dbContext.SaveChanges();
                    dbContext.CarRentals.AddRange(GetCarRentals());
                    dbContext.SaveChanges();
                }

            }
        }
        private List<CarType> GetTypes()
        {
           return new List<CarType>()
            {
                new CarType {
                
                 Name="Model S",
                 Motor ="1020",
                 Range=396,
                 Seats=5
                },
                new CarType {
                 
                 Name="Model 3",
                 Motor ="450",
                 Range=358,
                 Seats=5
                },
                 new CarType {
                  
                 Name="Model X",
                 Motor ="1020",
                 Range=333,
                 Seats=5
                },
                 new CarType {
                 Name="Model X s6",
                 Motor ="1020",
                 Range=333,
                 Seats=6
                },
                 new CarType {
                     
                 Name="Model Y",
                 Motor ="450",
                 Range=330,
                 Seats=5
                },
                new CarType {
                 Name="Model Y s7",
                 Motor ="450",
                 Range=330,
                 Seats=7
                },
            };
        }

        private IEnumerable<CarRental> GetCarRentals()
        {
            return new List<CarRental>()
            {
                new CarRental
                {
                   Name="Palma Airport" ,
                   PostalCode="07611",
                   Street ="Airport",
                   City="Palma",
                   IsActive=true,
                   Cars= GetCars(new List<CarParameter>()
                   {
                       new CarParameter(){ Price=51.40,Type=1},
                       new CarParameter(){ Price=60,Type=2},
                       new CarParameter(){ Price=70,Type=3},
                       new CarParameter(){ Price=51.40,Type=1},
                       new CarParameter(){ Price=120,Type=5},
                       new CarParameter(){ Price=51.40,Type=1},
                   })
                },
                new CarRental
                {
                    Name="Palma City Center",
                    PostalCode="27611",
                    Street ="Airport",
                    City="Palma",
                    IsActive=true,
                    Cars= GetCars(new List<CarParameter>()
                   {
                       new CarParameter(){ Price=51.40,Type=1},
                       new CarParameter(){ Price=60,Type=2},
                       new CarParameter(){ Price=70,Type=3},
                       new CarParameter(){ Price=51.40,Type=1},
                       new CarParameter(){ Price=120,Type=5},
                       new CarParameter(){ Price=125,Type=6},
                       new CarParameter(){ Price=51.40,Type=1},
                   })

                },
                new CarRental
                {
                    Name="Alcudia",
                    PostalCode="47611",
                    Street ="Airport",
                    City="Alcudia",
                    IsActive=true,
                    Cars= GetCars(new List<CarParameter>()
                   {
                       new CarParameter(){ Price=51.40,Type=1},
                       new CarParameter(){ Price=60,Type=2},
                       new CarParameter(){ Price=60,Type=2},
                       new CarParameter(){ Price=51.40,Type=1},
                       new CarParameter(){ Price=51.40,Type=1},
                   })
                },
                new CarRental
                {
                    Name="Manacor",
                    PostalCode="67611",
                    Street ="Airport",
                    City="Manacor",
                    IsActive=true,
                   Cars= GetCars(new List<CarParameter>()
                   {
                       new CarParameter(){ Price=51.40,Type=1},
                       new CarParameter(){ Price=70,Type=3},
                       new CarParameter(){ Price=73,Type=4},
                       new CarParameter(){ Price=51.40,Type=1},
                       new CarParameter(){ Price=51.40,Type=1},
                   })
                },

            };
        }
        private List<Car> GetCars(List<CarParameter> carParameter)
        {

            var cars = new List<Car>();
            foreach (var item in carParameter)
            {
                cars.Add(GetCar(item.Price,item.Type));
            }
            return cars;
        }


        private Car GetCar(double dailyPrice,int carType)
        {
            return new Car
            {
                DailyPrice = dailyPrice,
                IsFree = true,
                IsPrepared = true,
                CarType = dbContext.CarTypes.Single(x => x.Id == carType),

            };
        }

    }
}
