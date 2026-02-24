namespace ShipTrack.TrackingService.DTOs;

public record TrackingRecordDto(
    Guid Id,
    Guid ShipmentId,
    string TrackingNumber,
    string Status,
    string CurrentLocation,
    string? Notes,
    DateTime RecordedAt
);