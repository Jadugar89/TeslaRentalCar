using DomainLayer.HostedServices;

namespace RentTeslaServer.Service.Setup
{
    public static class SetupHostService
    {
        public static void LoadHostServices(this IServiceCollection services)
        {
            services.AddHostedService<PurgeReservationHostedService>();
        }
    }
}
