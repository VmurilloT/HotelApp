using HotelApi.DAL;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using HotelApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSingleton<IConfiguration>(builder.Configuration)
    .AddEndpointsApiExplorer()
    .AddSerilog()
    .AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
        .WriteTo.File($"Logs/{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}").CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/RegisterGhest", ([FromBody] Guest guest) =>
{
    try
    {
        var guests = new GuestCRUD(Log.Logger).SetGuest(guest);

        return Results.Ok(guests);
    }
    catch
    {
        return Results.Problem();
    }
});

app.Run();
