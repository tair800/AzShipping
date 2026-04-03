using Identity.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using MrStyx.Infrastructure;

namespace Identity.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : Repository<User, long>(context), IUserRepository
{
    protected override IQueryable<User> ExtendQuery(IQueryable<User> query) => query.Include(u => u.UserRoles);
}