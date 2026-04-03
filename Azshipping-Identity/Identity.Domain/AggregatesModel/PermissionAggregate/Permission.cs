using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;
using System.Text.RegularExpressions;

namespace Identity.Domain.AggregatesModel.PermissionAggregate;

public sealed class Permission : Entity<long>, IAggregateRoot
{
    public string Name { get; private set; } = null!;
    public string Module { get; private set; } = null!;

    public const int MAX_NAME_LENGTH = 100;
    public const int MAX_MODULE_LENGTH = 100;

    private static readonly Regex ModuleRegex = new("^[A-Za-z][A-Za-z0-9]*$", RegexOptions.Compiled);
    private static readonly Regex NameRegex = new("^[A-Za-z][A-Za-z0-9]*(\\.[A-Za-z][A-Za-z0-9]*)*$", RegexOptions.Compiled);

    private Permission() { }
    private Permission(string name, string module)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException($"{nameof(Name)} is required");
        if (string.IsNullOrWhiteSpace(module)) throw new DomainException($"{nameof(Module)} is required");

        if (name.Length > MAX_NAME_LENGTH) throw new DomainException($"{nameof(Name)} must not exceed {MAX_NAME_LENGTH} characters");
        if (module.Length > MAX_MODULE_LENGTH) throw new DomainException($"{nameof(Module)} must not exceed {MAX_MODULE_LENGTH} characters");

        if (!NameRegex.IsMatch(name)) throw new DomainException("Invalid name format");
        if (!ModuleRegex.IsMatch(module)) throw new DomainException("Invalid module format");

        Name = name;
        Module = module;
    }

    public static Permission Create(string name, string module) => new(name, module);
}