using Identity.Application.DTOs.User;
using Identity.Application.Interfaces.Services;
using Identity.Domain.AggregatesModel.RoleAggregate;

namespace Identity.Infrastructure.Services;

public sealed class UserDtoEnrichmentService(
    SettingsCatalogHttpReader settingsCatalog,
    IRoleRepository roleRepository) : IUserDtoEnrichmentService
{
    public async Task<UserDto> EnrichAsync(UserDto dto, CancellationToken cancellationToken)
    {
        var list = await EnrichAsync(new List<UserDto> { dto }, cancellationToken);
        return list[0];
    }

    public async Task<IReadOnlyList<UserDto>> EnrichAsync(IReadOnlyCollection<UserDto> users, CancellationToken cancellationToken)
    {
        if (users.Count == 0)
            return users is IReadOnlyList<UserDto> existing ? existing : users.ToList();

        try
        {
            var companies = await settingsCatalog.LoadCompanyNamesAsync(cancellationToken);
            var departments = await settingsCatalog.LoadDepartmentNamesAsync(cancellationToken);
            var posts = await settingsCatalog.LoadWorkerPostNamesAsync(cancellationToken);

            var roleIds = users.SelectMany(u => u.RoleIds).Distinct().ToList();
            var roles = await roleRepository.GetByIdsAsync(roleIds, cancellationToken);
            var roleMap = roles.ToDictionary(r => r.Id, r => r.Name);

            return users.Select(u =>
            {
                var groupLabels = u.RoleIds.Select(id => roleMap.TryGetValue(id, out var n) ? n : $"#{id}").ToList();
                return u with
                {
                    CompanyName = Resolve(u.CompanyId, companies),
                    DepartmentName = Resolve(u.DepartmentId, departments),
                    WorkerPostName = Resolve(u.WorkerPostId, posts),
                    GroupsDisplay = groupLabels.Count == 0 ? null : string.Join(", ", groupLabels)
                };
            }).ToList();
        }
        catch
        {
            return users is IReadOnlyList<UserDto> list ? list : users.ToList();
        }
    }

    private static string? Resolve(Guid? id, Dictionary<Guid, string> map)
    {
        if (!id.HasValue) return null;
        return map.TryGetValue(id.Value, out var n) ? n : null;
    }
}
