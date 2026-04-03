using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Domain.AggregatesModel.RefreshTokenAggregate;

public interface IRefreshTokenRepository : IRepository<RefreshToken, long> { }