using Microsoft.EntityFrameworkCore;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.DataAccessLayer.Contracts;
using RentTeslaServer.DataAccessLayer.Repository;

namespace RentTeslaServer.Service.Setup
{
    public static class SetupDB
    {
        public static void ConfigureDatabase(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<RentTeslaDbContext>(options =>
                        options.UseSqlServer(connectionString));
        }

        public static void LoadRepository(this IServiceCollection services)
        {
            services.AddScoped<ICarRepository,CarRepository>();
            services.AddScoped<ICarTypeRepository,CarTypeRepository>(); 
            services.AddScoped<ICarRentalRepository,CarRentalRepository>(); 
            services.AddScoped<IReservationRepository,ReservationRepository>(); 
        }   
    }
}
