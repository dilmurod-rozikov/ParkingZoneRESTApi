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
            modelBuilder.Entity<ParkingSlot>()
                .HasOne(p => p.ParkingZone)
                .WithMany(p => p.ParkingSlots)
                .HasForeignKey(p => p.ParkingZoneId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ParkingSlot)
                .WithMany(r => r.Reservations)
                .HasForeignKey(r => r.ParkingSlotId);
        }
    }
}