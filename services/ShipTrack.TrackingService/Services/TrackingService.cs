using Microsoft.EntityFrameworkCore;
using ShipTrack.Shared.Enums;
using ShipTrack.TrackingService.Data;
using ShipTrack.TrackingService.DTOs;
using ShipTrack.TrackingService.Models;

namespace ShipTrack.TrackingService.Services;

public class TrackingService : ITrackingService
{
    private readonly TrackingDbContext _context;

    public TrackingService(TrackingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TrackingRecordDto>> GetHistoryAsync(string trackingNumber)
    {
        return await _context.TrackingRecords
            .Where(r => r.TrackingNumber == trackingNumber)
            .OrderByDescending(r => r.RecordedAt)
            .Select(r => ToDto(r))
            .ToListAsync();
    }

    public async Task<TrackingRecordDto?> GetLatestAsync(string trackingNumber)
    {
        var record = await _context.TrackingRecords
            .Where(r => r.TrackingNumber == trackingNumber)
            .OrderByDescending(r => r.RecordedAt)
            .FirstOrDefaultAsync();

        return record is null ? null : ToDto(record);
    }

    public async Task<TrackingRecordDto> AddRecordAsync(Guid shipmentId, string trackingNumber, UpdateLocationDto dto)
    {
        if (!Enum.TryParse<ShipmentStatus>(dto.Status, out var status))
            status = ShipmentStatus.InTransit;

        var record = new TrackingRecord
        {
            ShipmentId = shipmentId,
            TrackingNumber = trackingNumber,
            Status = status,
            CurrentLocation = dto.CurrentLocation,
            Notes = dto.Notes
        };

        _context.TrackingRecords.Add(record);
        await _context.SaveChangesAsync();
        return ToDto(record);
    }

    private static TrackingRecordDto ToDto(TrackingRecord r) => new(
        r.Id, r.ShipmentId, r.TrackingNumber,
        r.Status.ToString(), r.CurrentLocation,
        r.Notes, r.RecordedAt
    );
}