using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;

namespace Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;

public sealed class PasswordHash : ValueObject
{
    public string Value { get; private set; }

    private PasswordHash(string value) => Value = value;

    public static PasswordHash Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Password hash is required");

        return new(value); 
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}