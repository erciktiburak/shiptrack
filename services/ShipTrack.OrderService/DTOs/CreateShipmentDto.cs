namespace ShipTrack.OrderService.DTOs;

public record CreateShipmentDto(
    string SenderName,
    string ReceiverName,
    string OriginPort,
    string DestinationPort,
    decimal WeightKg
);