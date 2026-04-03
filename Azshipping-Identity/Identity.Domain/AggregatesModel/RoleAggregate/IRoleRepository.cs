using MrStyx.Domain.SeedWork.Persistence;

namespace Identity.Domain.AggregatesModel.RoleAggregate;

public interface IRoleRepository : IRepository<Role, long> { }