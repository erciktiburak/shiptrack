using Microsoft.EntityFrameworkCore;
using ShipTrack.NotificationService.Data;
using ShipTrack.NotificationService.Models;

namespace ShipTrack.NotificationService.Services;

public class NotificationService : INotificationService
{
    private readonly NotificationDbContext _context;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(NotificationDbContext context, ILogger<NotificationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NotificationLog> CreateLogAsync(
        Guid shipmentId, string trackingNumber,
        string recipientName, string type, string message)
    {
        var log = new NotificationLog
        {
            ShipmentId = shipmentId,
            TrackingNumber = trackingNumber,
            RecipientName = recipientName,
            NotificationType = type,
            Message = message,
            IsSent = true,
            SentAt = DateTime.UtcNow
        };

        _context.NotificationLogs.Add(log);
        await _context.SaveChangesAsync();

        _logger.LogInformation(
            "[NotificationService] {Type} notification sent to {Recipient} for {TrackingNumber}",
            type, recipientName, trackingNumber);

        return log;
    }

    public async Task<IEnumerable<NotificationLog>> GetLogsAsync(string trackingNumber)
    {
        return await _context.NotificationLogs
            .Where(n => n.TrackingNumber == trackingNumber)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync();
    }
}