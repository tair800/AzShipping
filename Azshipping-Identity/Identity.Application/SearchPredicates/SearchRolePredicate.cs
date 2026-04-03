using Identity.Application.DTOs.Role;
using Identity.Domain.AggregatesModel.RoleAggregate;
using System.Linq.Expressions;

namespace Identity.Application.SearchPredicates;

public static class SearchRolePredicate
{
    public static Expression<Func<Role, bool>> BuildPredicate(SearchRoleDto dto)
    {
        return role =>
        (dto.Id == null || role.Id.ToString().Contains(dto.Id)) &&
        (string.IsNullOrWhiteSpace(dto.Name) || role.Name.ToLower().StartsWith(dto.Name.ToLower()));
    }
}