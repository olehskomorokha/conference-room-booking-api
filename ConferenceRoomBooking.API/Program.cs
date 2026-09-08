using System.Text.Json.Serialization;
using System.Reflection;
using ConferenceRoomBooking.Data;
using ConferenceRoomBooking.Data.Interfaces;
using ConferenceRoomBooking.Data.Repositories;
using ConferenceRoomBooking.Service.Intefraces;
using ConferenceRoomBooking.Service.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SystemException = ConferenceRoomBooking.Service.Exceptions.SystemException;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddSwaggerGen(options =>
{
    var apiXmlPath = Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    var serviceXmlPath = Path.Combine(AppContext.BaseDirectory, "ConferenceRoomBooking.Service.xml");

    options.IncludeXmlComments(apiXmlPath);
    options.IncludeXmlComments(serviceXmlPath);
});
// register controller
// and add 400 Bad Request error handling
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.UnmappedMemberHandling =
            JsonUnmappedMemberHandling.Disallow;
    });
;

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

builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

builder.Services.AddScoped<IReportService, ReportService>();

var app = builder.Build();

// configure exception handling
app.UseExceptionHandler(exceptionApp =>
{
    exceptionApp.Run(async context =>
    {
        var exception = context.Features
            .Get<IExceptionHandlerFeature>()?
            .Error;

        if (exception is SystemException systemException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            await context.Response.WriteAsJsonAsync(new
            {
                code = systemException.Code,
                message = systemException.Message
            });

            return;
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsJsonAsync(new
        {
            message = "Internal server error."
        });
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // configure swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();
