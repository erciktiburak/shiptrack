using MassTransit;
using ShipTrack.Shared.Events;

namespace ShipTrack.TrackingService.Consumers;

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
            "[TrackingService] New shipment received: {TrackingNumber} | {Origin} → {Destination}",
            msg.TrackingNumber, msg.OriginPort, msg.DestinationPort);

        // İleride: DB'ye tracking kaydı eklenecek
        await Task.CompletedTask;
    }
}