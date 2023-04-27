using DomainLayer.Services;
using RentTeslaServer.DomainLayer.Contracts;
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

            services.AddScoped<ErrorHandlingMiddleware>();
        }
    }
}
