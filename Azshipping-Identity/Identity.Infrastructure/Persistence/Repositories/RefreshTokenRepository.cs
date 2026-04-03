using Identity.Domain.AggregatesModel.RefreshTokenAggregate;
using MrStyx.Infrastructure;

namespace Identity.Infrastructure.Persistence.Repositories;

public class RefreshTokenRepository(AppDbContext context) : Repository<RefreshToken, long>(context), IRefreshTokenRepository { }