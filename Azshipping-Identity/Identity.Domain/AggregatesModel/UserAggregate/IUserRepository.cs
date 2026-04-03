using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Domain.AggregatesModel.UserAggregate;

public interface IUserRepository : IRepository<User, long> { }