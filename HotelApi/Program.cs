using HotelApi.DAL;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using HotelApi.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

    return Results.Ok(guests);
      
});

app.MapGet("/GetRooms", () =>
{
    var rooms = new RoomsCRUD(Log.Logger).GetRooms();

    return Results.Ok(rooms);

});

app.MapPost("/GetFreeRooms", ([FromBody] FreeRoomsFilter filter)=>
{
    var freeRooms = new BookRoomCrud(Log.Logger).GetFreeRooms(filter.CheckIn, filter.CheckOut);
    return Results.Ok(freeRooms);
});

app.MapPost("/SetReservation", ([FromBody] Reservation reservation) =>
{
    var bookRoom = new BookRoomCrud(Log.Logger).SetBookRoom(reservation);

    return Results.Ok(bookRoom);
    

});

app.Run();