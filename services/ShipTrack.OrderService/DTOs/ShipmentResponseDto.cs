using ShipTrack.Shared.Enums;

namespace ShipTrack.OrderService.DTOs;

public record ShipmentResponseDto(
    Guid Id,
    string TrackingNumber,
    string SenderName,
    string ReceiverName,
    string OriginPort,
    string DestinationPort,
    decimal WeightKg,
    string Status,
    DateTime CreatedAt
);