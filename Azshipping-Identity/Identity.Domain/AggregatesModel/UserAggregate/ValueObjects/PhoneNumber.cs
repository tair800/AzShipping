using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;
using System.Text.RegularExpressions;

namespace Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public string Value { get; private set; }

    public static readonly Regex Regex = new("^[+]?[0-9\\s\\-()]+$", RegexOptions.Compiled);

    public const int MAX_LENGTH = 20;

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainException("Phone number can not be empty");

        if (value.Length > MAX_LENGTH) throw new DomainException($"Phone number must not exceed {MAX_LENGTH} characters");

        if (!Regex.IsMatch(value)) throw new DomainException("Invalid phone format");

        return new(value); 
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}