using Identity.Application.DTOs.User;
using Identity.Domain.AggregatesModel.UserAggregate;
using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using System.Linq.Expressions;

namespace Identity.Application.SearchPredicates;

public static class SearchUserPredicate
{
    public static Expression<Func<User, bool>> BuildPredicate(SearchUserDto dto)
    {
        var status = string.IsNullOrWhiteSpace(dto.Status)
            ? null
            : UserStatus.GetAll().FirstOrDefault(s => string.Equals(s.Name, dto.Status, StringComparison.OrdinalIgnoreCase));

        Guid? companyId = ParseGuid(dto.CompanyId);
        Guid? departmentId = ParseGuid(dto.DepartmentId);
        Guid? workerPostId = ParseGuid(dto.WorkerPostId);

        return user =>
            (dto.Id == null || user.Id.ToString().Contains(dto.Id)) &&

            (string.IsNullOrWhiteSpace(dto.Username) || user.Username.Value.ToLower().StartsWith(dto.Username.ToLower())) &&

            (string.IsNullOrWhiteSpace(dto.Name) || (user.FullName != null && user.FullName.Name.ToLower().StartsWith(dto.Name.ToLower()))) &&
            (string.IsNullOrWhiteSpace(dto.Surname) || (user.FullName != null && user.FullName.Surname.ToLower().StartsWith(dto.Surname.ToLower()))) &&

            (string.IsNullOrWhiteSpace(dto.Email) || user.Email.Value.ToLower().StartsWith(dto.Email.ToLower())) &&

            (string.IsNullOrWhiteSpace(dto.PhoneNumber) ||
             (user.PhoneNumber != null && user.PhoneNumber.Value.Contains(dto.PhoneNumber)) ||
             user.AdditionalPhones.Any(p => p.Contains(dto.PhoneNumber))) &&

            (dto.CreationDate == null || user.CreationDate.Date == dto.CreationDate.Value.Date) &&
            (dto.LastLoginDate == null || (user.LastLoginDate != null && user.LastLoginDate.Value.Date == dto.LastLoginDate.Value.Date)) &&

            (string.IsNullOrWhiteSpace(dto.Status) || (status != null && user.Status == status)) &&

            (companyId == null || user.CompanyId == companyId) &&
            (departmentId == null || user.DepartmentId == departmentId) &&
            (workerPostId == null || user.WorkerPostId == workerPostId);
    }

    private static Guid? ParseGuid(string? s) =>
        string.IsNullOrWhiteSpace(s) || !Guid.TryParse(s, out var g) ? null : g;
}
