using MassTransit;
using ShipTrack.NotificationService.Helpers;
using ShipTrack.NotificationService.Services;
using ShipTrack.Shared.Events;

namespace ShipTrack.NotificationService.Consumers;

public class ShipmentCancelledConsumer : IConsumer<ShipmentCancelledEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<ShipmentCancelledConsumer> _logger;

    public ShipmentCancelledConsumer(INotificationService notificationService, ILogger<ShipmentCancelledConsumer> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ShipmentCancelledEvent> context)
    {
        var msg = context.Message;

        var message = NotificationTemplates.ShipmentCancelled(msg.TrackingNumber);

        await _notificationService.CreateLogAsync(
            msg.ShipmentId, msg.TrackingNumber,
            "System", "Cancelled", message);
    }
}