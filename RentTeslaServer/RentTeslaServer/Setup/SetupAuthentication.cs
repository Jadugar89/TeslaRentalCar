using Microsoft.IdentityModel.Tokens;
using RentTeslaServer.Service.Settings;
using System.Text;

namespace RentTeslaServer.Service.Setup
{
    public static class SetupAuthentication
    {
        public static void LoadAuth(this IServiceCollection services,IConfiguration configuration)
        {
            var authenticationSettings = new AuthenticationSettings();
            services.AddSingleton(authenticationSettings);
            configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),

                };
            });
        }
    }
}
