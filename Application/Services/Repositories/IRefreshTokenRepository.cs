using Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IRefreshTokenRepository : IAsyncRepository<Core.Security.Entities.RefreshToken, int>
{
    Task<List<Core.Security.Entities.RefreshToken>> GetOldRefreshTokenAsync(int userId, int refreshTokenTTL);
}