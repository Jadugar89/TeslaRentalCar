using DomainLayer.Services;
using Microsoft.AspNetCore.Identity;
using RentTeslaServer.DataAccessLayer.Entities;
using RentTeslaServer.DomainLayer.Contracts;
using RentTeslaServer.DomainLayer.Services;
using RentTeslaServer.Middleware;

namespace RentTeslaServer.Service.Setup
{
    public static class SetupServices
    {
        public static void LoadServices(this IServiceCollection services)
        {
            services.AddScoped<IRentalCarService, RentalCarService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<ICarTypeService, CarTypeService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IPurgeReservationService, PurgeReservationService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IAuthService,AuthService>();

            services.AddScoped<ErrorHandlingMiddleware>();
        }
    }
}
