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
        .WriteTo.File($"Logs/{DateTime.Now.Day}_{DateTime.Now.Month}_{DateTime.Now.Year}.log").CreateLogger();

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
    var guests = new GuestCRUD(Log.Logger).SetGuest(guest);

    if (guests.Result)
    {
        return Results.Ok(guests);
    }
    else
    {
        return Results.Problem();
    }
    
});

app.Run();
