using ShipTrack.OrderService.DTOs;

namespace ShipTrack.OrderService.Services;

public interface IOrderService
{
    Task<IEnumerable<ShipmentResponseDto>> GetAllAsync();
    Task<ShipmentResponseDto?> GetByIdAsync(Guid id);
    Task<ShipmentResponseDto?> GetByTrackingNumberAsync(string trackingNumber);
    Task<ShipmentResponseDto> CreateAsync(CreateShipmentDto dto);
    Task<bool> CancelAsync(Guid id);
}