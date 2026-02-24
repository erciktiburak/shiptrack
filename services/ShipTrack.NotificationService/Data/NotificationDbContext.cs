using Microsoft.EntityFrameworkCore;
using ShipTrack.NotificationService.Models;

namespace ShipTrack.NotificationService.Data;

public class NotificationDbContext : DbContext
{
    public NotificationDbContext(DbContextOptions<NotificationDbContext> options) : base(options) { }

    public DbSet<NotificationLog> NotificationLogs => Set<NotificationLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotificationLog>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TrackingNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.RecipientName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.NotificationType).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.TrackingNumber);
        });
    }
}