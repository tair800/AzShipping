using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;

namespace Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;

public sealed class Username : ValueObject
{
    public string Value { get; private set; }

    public const int MAX_LENGTH = 50;
    public const int MIN_LENGTH = 3;

    private Username(string value) => Value = value;

    public static Username Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainException($"{nameof(Username)} is required");

        if (value.Length < MIN_LENGTH || value.Length > MAX_LENGTH)
            throw new DomainException($"{nameof(Username)} must be between {MIN_LENGTH} and {MAX_LENGTH} characters");

        return new(value);
    }
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}