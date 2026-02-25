namespace ShipTrack.AuthService.DTOs;

public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    string Username,
    string Role
);