using ConferenceRoomBooking.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// register database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        "Server=localhost\\SQLEXPRESS;Database=conferenceRoomBookingDb;Trusted_Connection=True;TrustServerCertificate=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();