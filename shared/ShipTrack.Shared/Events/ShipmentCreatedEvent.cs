namespace ShipTrack.Shared.Events;

public record ShipmentCreatedEvent(
    Guid ShipmentId,
    string TrackingNumber,
    string SenderName,
    string ReceiverName,
    string OriginPort,
    string DestinationPort,
    decimal WeightKg,
    DateTime CreatedAt
);