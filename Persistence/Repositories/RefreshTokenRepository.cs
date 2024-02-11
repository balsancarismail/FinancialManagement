using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class RefreshTokenRepository(BaseDbContext context)
    : EfRepositoryBase<RefreshToken, int, BaseDbContext>(context), IRefreshTokenRepository
{
    public Task<List<RefreshToken>> GetOldRefreshTokenAsync(int userId, int refreshTokenTTL)
    {
        return Query()
            .AsNoTracking()
            .Where(rt => rt.UserId == userId && rt.Revoked == null && rt.Expiration >= DateTime.UtcNow &&
                         rt.CreatedDate.AddDays(refreshTokenTTL) <= DateTime.UtcNow)
            .ToListAsync();
    }
}