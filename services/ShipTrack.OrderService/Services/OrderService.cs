using MassTransit;
using Microsoft.EntityFrameworkCore;
using ShipTrack.OrderService.Data;
using ShipTrack.OrderService.DTOs;
using ShipTrack.OrderService.Models;
using ShipTrack.Shared.Enums;
using ShipTrack.Shared.Events;
using ShipTrack.Shared.Services;

namespace ShipTrack.OrderService.Services;

public class OrderService : IOrderService
{
    private readonly OrderDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ICacheService _cache;

    private const string AllShipmentsCacheKey = "shipments:all";
    private const string ShipmentCachePrefix = "shipments:";

    public OrderService(OrderDbContext context, IPublishEndpoint publishEndpoint, ICacheService cache)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
        _cache = cache;
    }

    public async Task<IEnumerable<ShipmentResponseDto>> GetAllAsync()
    {
        var cached = await _cache.GetAsync<IEnumerable<ShipmentResponseDto>>(AllShipmentsCacheKey);
        if (cached is not null) return cached;

        var shipments = await _context.Shipments
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => ToDto(s))
            .ToListAsync();

        await _cache.SetAsync(AllShipmentsCacheKey, shipments, TimeSpan.FromMinutes(2));
        return shipments;
    }

    public async Task<ShipmentResponseDto?> GetByIdAsync(Guid id)
    {
        var cacheKey = $"{ShipmentCachePrefix}id:{id}";
        var cached = await _cache.GetAsync<ShipmentResponseDto>(cacheKey);
        if (cached is not null) return cached;

        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment is null) return null;

        await _cache.SetAsync(cacheKey, ToDto(shipment), TimeSpan.FromMinutes(5));
        return ToDto(shipment);
    }

    public async Task<ShipmentResponseDto?> GetByTrackingNumberAsync(string trackingNumber)
    {
        var cacheKey = $"{ShipmentCachePrefix}track:{trackingNumber}";
        var cached = await _cache.GetAsync<ShipmentResponseDto>(cacheKey);
        if (cached is not null) return cached;

        var shipment = await _context.Shipments
            .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);
        if (shipment is null) return null;

        await _cache.SetAsync(cacheKey, ToDto(shipment), TimeSpan.FromMinutes(5));
        return ToDto(shipment);
    }

    public async Task<ShipmentResponseDto> CreateAsync(CreateShipmentDto dto)
    {
        var shipment = new Shipment
        {
            TrackingNumber = GenerateTrackingNumber(),
            SenderName = dto.SenderName,
            ReceiverName = dto.ReceiverName,
            OriginPort = dto.OriginPort,
            DestinationPort = dto.DestinationPort,
            WeightKg = dto.WeightKg
        };

        _context.Shipments.Add(shipment);
        await _context.SaveChangesAsync();

        // Cache'i temizle
        await _cache.RemoveAsync(AllShipmentsCacheKey);

        await _publishEndpoint.Publish(new ShipmentCreatedEvent(
            shipment.Id, shipment.TrackingNumber, shipment.SenderName,
            shipment.ReceiverName, shipment.OriginPort, shipment.DestinationPort,
            shipment.WeightKg, shipment.CreatedAt));

        return ToDto(shipment);
    }

    public async Task<bool> CancelAsync(Guid id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment is null || shipment.Status == ShipmentStatus.Delivered) return false;

        shipment.Status = ShipmentStatus.Cancelled;
        shipment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Cache'i temizle
        await _cache.RemoveAsync(AllShipmentsCacheKey);
        await _cache.RemoveAsync($"{ShipmentCachePrefix}id:{id}");
        await _cache.RemoveAsync($"{ShipmentCachePrefix}track:{shipment.TrackingNumber}");

        await _publishEndpoint.Publish(new ShipmentCancelledEvent(
            shipment.Id, shipment.TrackingNumber, shipment.UpdatedAt.Value));

        return true;
    }

    private static string GenerateTrackingNumber() =>
        $"SHT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

    private static ShipmentResponseDto ToDto(Shipment s) => new(
        s.Id, s.TrackingNumber, s.SenderName, s.ReceiverName,
        s.OriginPort, s.DestinationPort, s.WeightKg,
        s.Status.ToString(), s.CreatedAt);
}
