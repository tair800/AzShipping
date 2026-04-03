using Identity.Application.DTOs.User;
using Identity.Domain.AggregatesModel.UserAggregate;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Extensions.MapsterExtensions;

public static class UserMappingConfigurationExtension
{
    public static IServiceCollection AddUserMappingConfiguration(this IServiceCollection services)
    {
        TypeAdapterConfig<User, UserDto>.NewConfig()
        .Map(dest => dest.RoleIds, src => src.UserRoles.Select(ur => ur.RoleId).ToList())
        .Map(dest => dest.Username, src => src.Username.Value)
        .Map(dest => dest.Email, src => src.Email.Value)
        .Map(dest => dest.Name, src => src.FullName != null ? src.FullName.Name : null)
        .Map(dest => dest.Surname, src => src.FullName != null ? src.FullName.Surname : null)
        .Map(dest => dest.Phone, src => src.PhoneNumber != null ? src.PhoneNumber.Value : null)
        .Map(dest => dest.Status, src => src.Status.Name)
        .Map(dest => dest.EmployeeGroupIds, src => (IReadOnlyList<Guid>)src.EmployeeGroupIds)
        .Map(dest => dest.AdditionalEmails, src => (IReadOnlyList<string>)src.AdditionalEmails)
        .Map(dest => dest.AdditionalPhones, src => (IReadOnlyList<string>)src.AdditionalPhones)
        .Map(dest => dest.CompanyName, _ => (string?)null)
        .Map(dest => dest.DepartmentName, _ => (string?)null)
        .Map(dest => dest.WorkerPostName, _ => (string?)null)
        .Map(dest => dest.GroupsDisplay, _ => (string?)null);

        TypeAdapterConfig<IEnumerable<User>, UserList>.NewConfig()
        .Map(dest => dest.Users, src => src.Adapt<IReadOnlyCollection<UserDto>>());

        return services;
    }
}
