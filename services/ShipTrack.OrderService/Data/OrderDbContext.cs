using Microsoft.EntityFrameworkCore;
using ShipTrack.OrderService.Models;

namespace ShipTrack.OrderService.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Shipment> Shipments => Set<Shipment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TrackingNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.SenderName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.ReceiverName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.OriginPort).IsRequired().HasMaxLength(100);
            entity.Property(e => e.DestinationPort).IsRequired().HasMaxLength(100);
            entity.Property(e => e.WeightKg).HasPrecision(10, 2);
            entity.HasIndex(e => e.TrackingNumber).IsUnique();
        });
    }
}