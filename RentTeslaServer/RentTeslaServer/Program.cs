using DomainLayer.HostedServices;
using DomainLayer.ProfileMappings;
using DomainLayer.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using RentTeslaServer;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.Middleware;
using RentTeslaServer.Service.Setup;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.LoadServices();
builder.Services.LoadHostServices();
builder.Services.LoadRepository();
builder.Services.ConfigureCors(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>());
builder.Services.ConfigureDatabase(builder.Configuration.GetConnectionString("RentTeslaDbConnectionString") ?? string.Empty);
builder.Services.LoadMappingProfiles();
builder.Services.AddValidatorsFromAssemblyContaining<ReservationValidator>();

builder.Host.UseNLog();

var app = builder.Build();

app.UseCors("FrontEndClient");
app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var RentTeslaDbContext = scope.ServiceProvider.GetRequiredService<RentTeslaDbContext>();
        RentTeslaDbContext.Database.Migrate();
    new RentTeslaSeeder(RentTeslaDbContext).Seed();
}

app.Run();
