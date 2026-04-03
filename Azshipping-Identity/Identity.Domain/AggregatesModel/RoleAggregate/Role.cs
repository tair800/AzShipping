using Identity.Domain.AggregatesModel.RoleAggregate.Events;
using Identity.Domain.JoinEntities;
using Identity.Domain.SeedData;
using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;

namespace Identity.Domain.AggregatesModel.RoleAggregate;

public sealed class Role : Entity<long>, IAggregateRoot
{
    public string Name { get; private set; } = null!;

    private readonly List<RolePermission> _rolePermissions = [];
    public IReadOnlyCollection<RolePermission> RolePermissions => _rolePermissions.AsReadOnly();

    public const int MAX_LENGTH = 50;
    public const int MIN_LENGTH = 2;

    private Role() { }

    private Role(string name, IReadOnlyCollection<long> permissionIds)
    {
        Validate(name);

        Name = name;

        SetPermissions(permissionIds);
    }

    public static Role Create(string name, IReadOnlyCollection<long> permissionIds)
    {
        var role = new Role(name, permissionIds);
        role.AddDomainEvent(new RoleCreatedDomainEvent(role.Name));
        
        return role;
    }

    public void Update(string name, IReadOnlyCollection<long> permissionIds)
    {
        Validate(name);

        Name = name;

        SetPermissions(permissionIds);
    }

    public void IsSystemRole()
    {
        if (RoleCatalog.All.Any(r => string.Equals(r.Name, Name, StringComparison.OrdinalIgnoreCase)))
            throw new DomainException("You can't delete system roles");
    }

    private void SetPermissions(IReadOnlyCollection<long> permissionIds)
    {
        _rolePermissions.Clear();

        foreach (var id in permissionIds.Distinct()) 
            _rolePermissions.Add(new RolePermission(id));
    }

    private static void Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException($"{nameof(Name)} is required");

        if (name.Length < MIN_LENGTH || name.Length > MAX_LENGTH)
            throw new DomainException($"{nameof(Name)} must be between {MIN_LENGTH} and {MAX_LENGTH} characters");
    }
}