using Identity.Domain.AggregatesModel.PermissionAggregate;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.JoinEntities;
using Microsoft.EntityFrameworkCore;
using MrStyx.Infrastructure;

namespace Identity.Infrastructure.Persistence.Repositories;

public class PermissionRepository(AppDbContext context) : Repository<Permission, long>(context), IPermissionRepository
{
    public async Task<IReadOnlyCollection<string>> GetUserPermissionsAsync(long userId)
    {
        var roleIds = _context.Set<UserRole>()
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId);

        var permissionIds = _context.Set<RolePermission>()
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.PermissionId);

        var query = _context.Set<Permission>()
            .Where(p => permissionIds.Contains(p.Id))
            .Select(p => p.Module + "." + p.Name);

        return await query.Distinct().ToListAsync();
    }

    public async Task<IReadOnlyCollection<string>> GetUserRolesAsync(long userId)
    {
        var roleIds = _context.Set<UserRole>()
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId);

        return await _context.Set<Role>()
            .Where(r => roleIds.Contains(r.Id))
            .Select(r => r.Name)
            .Distinct()
            .ToListAsync();
    }

    public async Task<bool> UserHasPermissionAsync(long userId, string permission)
    {
        var roleIds = _context.Set<UserRole>()
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId);

        var permissionIds = _context.Set<RolePermission>()
            .Where(rp => roleIds.Contains(rp.RoleId))
            .Select(rp => rp.PermissionId);

        return await _context.Set<Permission>()
            .AnyAsync(p => permissionIds.Contains(p.Id) && (p.Module + "." + p.Name) == permission);
    }
}