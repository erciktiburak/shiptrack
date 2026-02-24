namespace ShipTrack.NotificationService.Models;

public class NotificationLog
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ShipmentId { get; set; }
    public string TrackingNumber { get; set; } = string.Empty;
    public string RecipientName { get; set; } = string.Empty;
    public string NotificationType { get; set; } = string.Empty; // Created, Cancelled, StatusChanged
    public string Message { get; set; } = string.Empty;
    public bool IsSent { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; set; }
}