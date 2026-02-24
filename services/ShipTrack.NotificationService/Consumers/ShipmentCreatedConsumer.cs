using MassTransit;
using ShipTrack.NotificationService.Helpers;
using ShipTrack.NotificationService.Services;
using ShipTrack.Shared.Events;

namespace ShipTrack.NotificationService.Consumers;

public class ShipmentCreatedConsumer : IConsumer<ShipmentCreatedEvent>
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<ShipmentCreatedConsumer> _logger;

    public ShipmentCreatedConsumer(INotificationService notificationService, ILogger<ShipmentCreatedConsumer> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ShipmentCreatedEvent> context)
    {
        var msg = context.Message;

        var message = NotificationTemplates.ShipmentCreated(
            msg.ReceiverName, msg.TrackingNumber,
            msg.OriginPort, msg.DestinationPort);

        await _notificationService.CreateLogAsync(
            msg.ShipmentId, msg.TrackingNumber,
            msg.ReceiverName, "Created", message);
    }
}