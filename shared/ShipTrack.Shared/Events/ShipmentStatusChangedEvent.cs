namespace ShipTrack.Shared.Events;

public record ShipmentStatusChangedEvent(
    Guid ShipmentId,
    string TrackingNumber,
    string OldStatus,
    string NewStatus,
    DateTime ChangedAt
);