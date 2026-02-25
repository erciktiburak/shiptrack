namespace ShipTrack.AuthService.DTOs;

public record RegisterDto(
    string Username,
    string Email,
    string Password
);