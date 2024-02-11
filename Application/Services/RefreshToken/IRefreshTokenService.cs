namespace Application.Services.RefreshToken;

public interface IRefreshTokenService
{
    Task AddRefreshTokenAsync(string token, int userId, DateTime exp, string remoteIpAddress);
}