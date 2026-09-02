using ConferenceRoomBooking.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) :
        base(options)
    {
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<AdditionalService> AdditionalServices { get; set; }
    public DbSet<ConferenceRoom> ConferenceRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RoomService>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.ConferenceRoom)
                .WithMany(x => x.RoomServices)
                .HasForeignKey(x => x.ConferenceRoomId);

            entity.HasOne(x => x.AdditionalService)
                .WithMany(x => x.RoomServices)
                .HasForeignKey(x => x.AdditionalServiceId);

            entity.HasIndex(x => new { x.ConferenceRoomId, x.AdditionalServiceId })
                .IsUnique();
        });

        modelBuilder.Entity<ConferenceRoom>()
            .Property(room => room.BasePricePerHour)
            .HasPrecision(18, 2);

        modelBuilder.Entity<AdditionalService>()
            .Property(service => service.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ConferenceRoom>().HasData(
            new ConferenceRoom
            {
                Id = 1,
                Name = "Зал А",
                Capacity = 50,
                BasePricePerHour = 2000m
            },
            new ConferenceRoom
            {
                Id = 2,
                Name = "Зал B",
                Capacity = 100,
                BasePricePerHour = 3500m,
            },
            new ConferenceRoom
            {
                Id = 3,
                Name = "Зал C",
                Capacity = 30,
                BasePricePerHour = 1500m
            }
        );
        modelBuilder.Entity<AdditionalService>().HasData(
            new AdditionalService
            {
                Id = 1,
                Name = "Проєктор",
                Price = 500m
            },
            new AdditionalService
            {
                Id = 2,
                Name = "Wi-Fi",
                Price = 300m
            },
            new AdditionalService
            {
                Id = 3,
                Name = "Звук",
                Price = 700m
            }
        );
    }
}