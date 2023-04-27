namespace RentTeslaServer.Service.Setup
{
    public static class SetupCors
    {
        public static void ConfigureCors(this IServiceCollection services, string[] allowedOrigins)
        {
            services.AddCors(policy =>
            {
                policy.AddPolicy("FrondEndClient", options =>
                                options.AllowAnyMethod().AllowAnyHeader()
                                       .AllowCredentials()
                                       .WithOrigins(allowedOrigins));
            });
        }
    }
}
