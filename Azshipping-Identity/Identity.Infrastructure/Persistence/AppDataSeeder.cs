using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.PermissionAggregate;
using Identity.Domain.AggregatesModel.RoleAggregate;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;
using Identity.Domain.SeedData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Persistence;

public static class AppDataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider, IPasswordService passwordService)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await PermissionSeeder(context);

        await RoleSeeder(context);

        await AdminSeeder(context, passwordService);

        await ErpTestRequestViewerSeeder(context, passwordService);

        await context.SaveChangesAsync();
    }

    private static async Task PermissionSeeder(AppDbContext context)
    {
        var existingPermissions = await context.Permissions
                                  .AsNoTracking()
                                  .Select(x => new { x.Module, x.Name })
                                  .ToListAsync();

        var set = existingPermissions.Select(x => (x.Module.Trim().ToUpperInvariant(), x.Name.Trim().ToUpperInvariant())).ToHashSet();

        foreach (var permission in PermissionCatalog.All)
        {
            if (!set.Contains((permission.Module.Trim().ToUpperInvariant(), permission.Name.Trim().ToUpperInvariant())))
            {
                context.Permissions.Add(Permission.Create(permission.Name.Trim(), permission.Module.Trim()));
            }
        }
    }

    private static async Task RoleSeeder(AppDbContext context)
    {
        var existingRoles = await context.Roles
                            .AsNoTracking()
                            .Select(x => new { x.Name })
                            .ToListAsync();

        var set = existingRoles.Select(x => x.Name.Trim()).ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var role in RoleCatalog.All)
        {
            if (!set.Contains(role.Name.Trim()))
            {
                context.Roles.Add(Role.Create(role.Name.Trim(), []));
            }
        }
    }

    private static async Task AdminSeeder(AppDbContext context, IPasswordService passwordService)
    {

        var existingAdmin = await context.Users
                           .AsNoTracking()
                           .FirstOrDefaultAsync(u => u.Username.Value == Admin.Username);


        if (existingAdmin is null)
        {
            var passwordVO = PasswordHash.Create(passwordService.HashPassword(Admin.Password));
            var usernameVO = Username.Create(Admin.Username);
            var emailVO = Email.Create(Admin.Email);

            var list = new List<long>() { 1 };

            var admin = User.Create(usernameVO, passwordVO, null, emailVO, null, list);

            context.Users.Add(admin);
        }
    }

    private static async Task ErpTestRequestViewerSeeder(AppDbContext context, IPasswordService passwordService)
    {
        if (await context.Users.AsNoTracking().AnyAsync(u => u.Username.Value == ErpTestRequestViewer.Username))
            return;

        var passwordVO = PasswordHash.Create(passwordService.HashPassword(ErpTestRequestViewer.Password));
        var usernameVO = Username.Create(ErpTestRequestViewer.Username);
        var emailVO = Email.Create(ErpTestRequestViewer.Email);

        var user = User.Create(usernameVO, passwordVO, null, emailVO, null, new List<long> { 1 });
        user.ApplyExtendedProfile(
            companyId: null,
            departmentId: null,
            workerPostId: null,
            employeeGroupIds: [ErpTestRequestViewer.EmployeeGroupId],
            employeePrefix: null,
            unlimitedAccess: false,
            isEmployee: true,
            accessSince: DateTime.UtcNow,
            additionalEmails: [],
            additionalPhones: [],
            fax: null,
            skype: null,
            sipNumber: null);
        user.Activate();

        context.Users.Add(user);
    }
}