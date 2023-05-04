using DomainLayer.ProfileMappings;
using RentTeslaServer.DomainLayer.ProfileMappings;

namespace RentTeslaServer.Service.Setup
{
    public static class SetupMapping
    {
        public static void LoadMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<CarMappingProfile>();
                cfg.AddProfile<CarRentalMappingProfile>();
                cfg.AddProfile<CarTypeMappingProfile>();
                cfg.AddProfile<ReservationMappingProfile>();
            });
        }
    }
}
