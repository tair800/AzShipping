using MrStyx.Domain.SeedWork.Abstracts;
using MrStyx.Domain.SeedWork.Exceptions;
using System.Net.Mail;

namespace Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; private set; }

    public const int MAX_LENGTH = 100;

    private Email(string value) => Value = value;

    public static Email Create(string value)
    {
        Validate(value);

        return new(value); 
    }

    private static void Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new DomainException($"{nameof(Email)} is required");

        if (value.Length > MAX_LENGTH) throw new DomainException($"{nameof(Email)} must not exceed {MAX_LENGTH} characters");

        try
        {
            var email = new MailAddress(value);
        }
        catch
        {
            throw new DomainException($"Invalid {nameof(Email).ToLower()} format");
        }
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}