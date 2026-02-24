using MassTransit;
using Microsoft.EntityFrameworkCore;
using ShipTrack.OrderService.Data;
using ShipTrack.OrderService.DTOs;
using ShipTrack.OrderService.Models;
using ShipTrack.Shared.Enums;
using ShipTrack.Shared.Events;

namespace ShipTrack.OrderService.Services;

public class OrderService : IOrderService
{
    private readonly OrderDbContext _context;
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderService(OrderDbContext context, IPublishEndpoint publishEndpoint)
    {
        _context = context;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<IEnumerable<ShipmentResponseDto>> GetAllAsync()
    {
        return await _context.Shipments
            .OrderByDescending(s => s.CreatedAt)
            .Select(s => ToDto(s))
            .ToListAsync();
    }

    public async Task<ShipmentResponseDto?> GetByIdAsync(Guid id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        return shipment is null ? null : ToDto(shipment);
    }

    public async Task<ShipmentResponseDto?> GetByTrackingNumberAsync(string trackingNumber)
    {
        var shipment = await _context.Shipments
            .FirstOrDefaultAsync(s => s.TrackingNumber == trackingNumber);
        return shipment is null ? null : ToDto(shipment);
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

        // Event yayınla
        await _publishEndpoint.Publish(new ShipmentCreatedEvent(
            shipment.Id,
            shipment.TrackingNumber,
            shipment.SenderName,
            shipment.ReceiverName,
            shipment.OriginPort,
            shipment.DestinationPort,
            shipment.WeightKg,
            shipment.CreatedAt
        ));

        return ToDto(shipment);
    }

    public async Task<bool> CancelAsync(Guid id)
    {
        var shipment = await _context.Shipments.FindAsync(id);
        if (shipment is null || shipment.Status == ShipmentStatus.Delivered) return false;

        var oldStatus = shipment.Status.ToString();
        shipment.Status = ShipmentStatus.Cancelled;
        shipment.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        // Event yayınla
        await _publishEndpoint.Publish(new ShipmentCancelledEvent(
            shipment.Id,
            shipment.TrackingNumber,
            shipment.UpdatedAt.Value
        ));

        return true;
    }

    private static string GenerateTrackingNumber() =>
        $"SHT-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpper()}";

    private static ShipmentResponseDto ToDto(Shipment s) => new(
        s.Id, s.TrackingNumber, s.SenderName, s.ReceiverName,
        s.OriginPort, s.DestinationPort, s.WeightKg,
        s.Status.ToString(), s.CreatedAt
    );
}