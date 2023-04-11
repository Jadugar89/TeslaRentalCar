using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using RentTeslaServer;
using RentTeslaServer.DataAccessLayer;
using RentTeslaServer.Domain_Layer.HostedServices;
using RentTeslaServer.Domain_Layer.ModelDtos;
using RentTeslaServer.Domain_Layer.ProfileMappings;
using RentTeslaServer.Domain_Layer.Services;
using RentTeslaServer.Domain_Layer.Validators;
using RentTeslaServer.Middleware;
using RentTeslaServer.Services;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<PurgeReservationHostedService>();

builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IRentalCarService, RentalCarService>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IPurgeReservationService,PurgeReservationService>();
builder.Services.AddScoped<IValidator<ReservationCreateDto>, ReservationValidator>();

builder.Services.AddAutoMapper(typeof(RentalCarMappingProfile));
builder.Services.AddDbContext<RentTeslaDbContext>(options =>
                        options.UseSqlServer(
                        builder.Configuration.GetConnectionString("RentTeslaDbConnectionString")));

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("FrondEndClient", options =>
    options.AllowAnyMethod()
    .AllowAnyHeader()
    .AllowAnyOrigin());

});
builder.Host.UseNLog();

var app = builder.Build();

app.UseCors("FrondEndClient");

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
