using System.Text.Json.Serialization;
using ConferenceRoomBooking.Data;
using ConferenceRoomBooking.Data.Interfaces;
using ConferenceRoomBooking.Data.Repositories;
using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddSwaggerGen();
// register controller
// and add 400 Bad Request error handling
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.UnmappedMemberHandling =
            JsonUnmappedMemberHandling.Disallow;
    });;

// register database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Server=localhost\\SQLEXPRESS;Database=conferenceRoomBookingDb;Trusted_Connection=True;TrustServerCertificate=True;"));

// configure DI

builder.Services.AddScoped<IAdditionalServiceRepository, AdditionalServiceRepository>();
builder.Services.AddScoped<IAdditionalServiceService, AdditionalServiceService>();

builder.Services.AddScoped<IRoomServiceRepository, RoomServiceRepository>();

builder.Services.AddScoped<IConferenceRoomRepository, ConferenceRoomRepository>();
builder.Services.AddScoped<IConferenceRoomService, ConferenceRoomService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();