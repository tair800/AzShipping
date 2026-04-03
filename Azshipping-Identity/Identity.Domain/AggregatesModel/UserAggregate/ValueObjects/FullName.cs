using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;

namespace Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;

public sealed class FullName : ValueObject
{
    public string Name { get; private set; }
    public string Surname { get; private set; }

    private FullName(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    public static FullName Create(string name, string surname)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException($"{nameof(Name)} is required");
        if (string.IsNullOrWhiteSpace(surname)) throw new DomainException($"{nameof(Surname)} is required");

        return new(name, surname);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Name;
        yield return Surname;
    }
}