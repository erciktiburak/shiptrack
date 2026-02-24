namespace ShipTrack.TrackingService.DTOs;

public record UpdateLocationDto(
    string CurrentLocation,
    string Status,
    string? Notes
);