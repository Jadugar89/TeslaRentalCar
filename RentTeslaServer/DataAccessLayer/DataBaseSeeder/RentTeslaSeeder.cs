using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Entities;
using System.Numerics;

namespace RentTeslaServer
{
    public class RentTeslaSeeder
    {
         class CarParameter
        {
           public int Type { get; set; }
           public decimal Price { get; set; }
           public string Plates { get; set; } = null!;
        }

        private readonly RentTeslaDbContext _dbContext;

        public RentTeslaSeeder(RentTeslaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.CarRentals.Any())
                {
                    _dbContext.CarTypes.AddRange(GetTypes());
                    _dbContext.SaveChanges();
                    _dbContext.CarRentals.AddRange(GetCarRentals());
                    _dbContext.SaveChanges();
                }

            }
        }
        private static List<CarType> GetTypes()
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
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AA123"},
                       new CarParameter(){ Price=60m,Type=2,Plates="AA223"},
                       new CarParameter(){ Price=70m,Type=3,Plates="AA124"},
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AA243"},
                       new CarParameter(){ Price=120m,Type=5,Plates="AA623"},
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AA903"},
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
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AC123"},
                       new CarParameter(){ Price=60m,Type=2,Plates="AC223"},
                       new CarParameter(){ Price=70m,Type=3,Plates="AC323"},
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AC723"},
                       new CarParameter(){ Price=120m,Type=5,Plates="AC246"},
                       new CarParameter(){ Price=125m,Type=6,Plates="AC453"},
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AC523"},
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
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AD223"},
                       new CarParameter(){ Price=60m,Type=2,Plates="AD323"},
                       new CarParameter(){ Price=60m,Type=2,Plates="AD423"},
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AD523"},
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AD723"},
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
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AX123"},
                       new CarParameter(){ Price=70m,Type=3,Plates="AX143"},
                       new CarParameter(){ Price=73m,Type=4,Plates="AX124"},
                       new CarParameter(){ Price=51.40m,Type=1,Plates="AX543"},
                       new CarParameter(){ Price=51.40m,Type=1, Plates="AX746"},
                   })
                },

            };
        }
        private List<Car> GetCars(List<CarParameter> carParameter)
        {

            var cars = new List<Car>();
            foreach (var item in carParameter)
            {
                cars.Add(GetCar(item.Price,item.Type,item.Plates));
            }
            return cars;
        }


        private Car GetCar(decimal dailyPrice,int carType,string plates)
        {
            return new Car
            {
                DailyPrice = dailyPrice,
                Plates = plates,
                IsFree = true,
                IsPrepared = true,
                CarType = _dbContext.CarTypes.Single(x => x.Id == carType),

            };
        }

    }
}
