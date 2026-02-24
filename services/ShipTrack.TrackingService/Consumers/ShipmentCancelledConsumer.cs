using MassTransit;
using ShipTrack.Shared.Events;

namespace ShipTrack.TrackingService.Consumers;

public class ShipmentCancelledConsumer : IConsumer<ShipmentCancelledEvent>
{
    private readonly ILogger<ShipmentCancelledConsumer> _logger;

    public ShipmentCancelledConsumer(ILogger<ShipmentCancelledConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ShipmentCancelledEvent> context)
    {
        var msg = context.Message;
        _logger.LogInformation(
            "[TrackingService] Shipment cancelled: {TrackingNumber} at {CancelledAt}",
            msg.TrackingNumber, msg.CancelledAt);

        await Task.CompletedTask;
    }
}