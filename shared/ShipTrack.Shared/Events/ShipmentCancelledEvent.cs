namespace ShipTrack.Shared.Events;

public record ShipmentCancelledEvent(
    Guid ShipmentId,
    string TrackingNumber,
    DateTime CancelledAt
);