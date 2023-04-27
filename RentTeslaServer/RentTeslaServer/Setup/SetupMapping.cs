using DomainLayer.ProfileMappings;

namespace RentTeslaServer.Service.Setup
{
    public static class SetupMapping
    {
        public static void LoadMappingProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RentalCarMappingProfile));
        }
    }
}
