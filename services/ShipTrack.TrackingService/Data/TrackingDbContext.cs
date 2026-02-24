using Microsoft.EntityFrameworkCore;
using ShipTrack.TrackingService.Models;

namespace ShipTrack.TrackingService.Data;

public class TrackingDbContext : DbContext
{
    public TrackingDbContext(DbContextOptions<TrackingDbContext> options) : base(options) { }

    public DbSet<TrackingRecord> TrackingRecords => Set<TrackingRecord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrackingRecord>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TrackingNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CurrentLocation).IsRequired().HasMaxLength(200);
            entity.HasIndex(e => e.TrackingNumber);
            entity.HasIndex(e => e.ShipmentId);
        });
    }
}
