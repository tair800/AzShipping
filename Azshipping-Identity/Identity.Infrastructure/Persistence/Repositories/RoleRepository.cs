using Identity.Domain.AggregatesModel.RoleAggregate;
using Microsoft.EntityFrameworkCore;
using MrStyx.Infrastructure;

namespace Identity.Infrastructure.Persistence.Repositories;

public class RoleRepository(AppDbContext context) : Repository<Role, long>(context), IRoleRepository
{
    protected override IQueryable<Role> ExtendQuery(IQueryable<Role> query) => query.Include(r => r.RolePermissions);
}