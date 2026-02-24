using MassTransit;
using ShipTrack.Shared.Events;

namespace ShipTrack.NotificationService.Consumers;

public class ShipmentCreatedConsumer : IConsumer<ShipmentCreatedEvent>
{
    private readonly ILogger<ShipmentCreatedConsumer> _logger;

    public ShipmentCreatedConsumer(ILogger<ShipmentCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ShipmentCreatedEvent> context)
    {
        var msg = context.Message;
        _logger.LogInformation(
            "[NotificationService] Sending confirmation to {Receiver} for shipment {TrackingNumber}",
            msg.ReceiverName, msg.TrackingNumber);

        await Task.CompletedTask;
    }
}