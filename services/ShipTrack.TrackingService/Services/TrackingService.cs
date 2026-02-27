using Microsoft.EntityFrameworkCore;
using ShipTrack.Shared.Enums;
using ShipTrack.Shared.Services;
using ShipTrack.TrackingService.Data;
using ShipTrack.TrackingService.DTOs;
using ShipTrack.TrackingService.Models;

namespace ShipTrack.TrackingService.Services;

public class TrackingService : ITrackingService
{
    private readonly TrackingDbContext _context;
    private readonly ICacheService _cache;

    private const string TrackingPrefix = "tracking:";

    public TrackingService(TrackingDbContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<IEnumerable<TrackingRecordDto>> GetHistoryAsync(string trackingNumber)
    {
        var cacheKey = $"{TrackingPrefix}history:{trackingNumber}";
        var cached = await _cache.GetAsync<IEnumerable<TrackingRecordDto>>(cacheKey);
        if (cached is not null) return cached;

        var records = await _context.TrackingRecords
            .Where(r => r.TrackingNumber == trackingNumber)
            .OrderByDescending(r => r.RecordedAt)
            .Select(r => ToDto(r))
            .ToListAsync();

        await _cache.SetAsync(cacheKey, records, TimeSpan.FromMinutes(1));
        return records;
    }

    public async Task<TrackingRecordDto?> GetLatestAsync(string trackingNumber)
    {
        var cacheKey = $"{TrackingPrefix}latest:{trackingNumber}";
        var cached = await _cache.GetAsync<TrackingRecordDto>(cacheKey);
        if (cached is not null) return cached;

        var record = await _context.TrackingRecords
            .Where(r => r.TrackingNumber == trackingNumber)
            .OrderByDescending(r => r.RecordedAt)
            .FirstOrDefaultAsync();

        if (record is null) return null;

        await _cache.SetAsync(cacheKey, ToDto(record), TimeSpan.FromSeconds(30));
        return ToDto(record);
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

        // Cache'i temizle
        await _cache.RemoveAsync($"{TrackingPrefix}latest:{trackingNumber}");
        await _cache.RemoveAsync($"{TrackingPrefix}history:{trackingNumber}");

        return ToDto(record);
    }

    private static TrackingRecordDto ToDto(TrackingRecord r) => new(
        r.Id, r.ShipmentId, r.TrackingNumber,
        r.Status.ToString(), r.CurrentLocation,
        r.Notes, r.RecordedAt);
}
