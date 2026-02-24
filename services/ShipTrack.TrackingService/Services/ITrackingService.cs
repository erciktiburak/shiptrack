using ShipTrack.TrackingService.DTOs;

namespace ShipTrack.TrackingService.Services;

public interface ITrackingService
{
    Task<IEnumerable<TrackingRecordDto>> GetHistoryAsync(string trackingNumber);
    Task<TrackingRecordDto?> GetLatestAsync(string trackingNumber);
    Task<TrackingRecordDto> AddRecordAsync(Guid shipmentId, string trackingNumber, UpdateLocationDto dto);
}