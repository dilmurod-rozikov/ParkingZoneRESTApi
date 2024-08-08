using Microsoft.EntityFrameworkCore;
using ParkingZoneWebApi.Models;

namespace ParkingZoneWebApi.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<ParkingZone> ParkingZones { get; set; }
        public DbSet<ParkingSlot> ParkingSlots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParkingZone>()
                .HasMany(p => p.ParkingSlots)
                .WithOne(x => x.ParkingZone)
                .HasForeignKey(x => x.ParkingZoneId);

            modelBuilder.Entity<ParkingSlot>()
                .HasMany(r => r.Reservations)
                .WithOne(x => x.ParkingSlot)
                .HasForeignKey(x => x.ParkingSlotId);
        }
    }
}