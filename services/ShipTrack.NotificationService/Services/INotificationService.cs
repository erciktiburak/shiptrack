using ShipTrack.NotificationService.Models;

namespace ShipTrack.NotificationService.Services;

public interface INotificationService
{
    Task<NotificationLog> CreateLogAsync(Guid shipmentId, string trackingNumber, string recipientName, string type, string message);
    Task<IEnumerable<NotificationLog>> GetLogsAsync(string trackingNumber);
}