namespace ShipTrack.AuthService.DTOs;

public record LoginDto(
    string Email,
    string Password
);