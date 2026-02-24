using MassTransit;
using ShipTrack.Shared.Events;
using ShipTrack.TrackingService.DTOs;
using ShipTrack.TrackingService.Services;

namespace ShipTrack.TrackingService.Consumers;

public class ShipmentCreatedConsumer : IConsumer<ShipmentCreatedEvent>
{
    private readonly ITrackingService _trackingService;
    private readonly ILogger<ShipmentCreatedConsumer> _logger;

    public ShipmentCreatedConsumer(ITrackingService trackingService, ILogger<ShipmentCreatedConsumer> logger)
    {
        _trackingService = trackingService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ShipmentCreatedEvent> context)
    {
        var msg = context.Message;

        await _trackingService.AddRecordAsync(
            msg.ShipmentId,
            msg.TrackingNumber,
            new UpdateLocationDto(msg.OriginPort, "Pending", "Shipment created, awaiting pickup")
        );

        _logger.LogInformation(
            "[TrackingService] Tracking record created for {TrackingNumber}",
            msg.TrackingNumber);
    }
}