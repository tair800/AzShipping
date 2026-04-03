using FluentValidation;
using Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;

namespace Identity.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.CreateUserDto).NotNull().WithMessage("User data is required");

        RuleFor(x => x.CreateUserDto.Username)
               .NotEmpty().WithMessage("Username is required")
               .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

        When(x => !string.IsNullOrWhiteSpace(x.CreateUserDto.Password), () =>
        {
            RuleFor(x => x.CreateUserDto!.Password!)
                   .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                   .MaximumLength(100).WithMessage("Password must not exceed 100 characters")
                   .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\w\\s]).+$")
                   .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character (no spaces)");
        });

        RuleFor(x => x.CreateUserDto.Email)
               .NotEmpty().WithMessage("Email is required")
               .EmailAddress().WithMessage("Invalid email format")
               .MaximumLength(100).WithMessage("Email must not exceed 100 characters");

        RuleFor(x => x.CreateUserDto.Name)
               .NotEmpty().WithMessage("Name is required")
               .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.CreateUserDto.Surname)
               .NotEmpty().WithMessage("Surname is required")
               .MaximumLength(50).WithMessage("Surname must not exceed 50 characters");

        RuleFor(x => x.CreateUserDto.Phone)
               .Matches(PhoneNumber.Regex).When(x => !string.IsNullOrWhiteSpace(x.CreateUserDto.Phone))
               .WithMessage("Invalid phone format")
               .MaximumLength(20).WithMessage("Phone must not exceed 20 characters");

        RuleFor(x => x.CreateUserDto.RoleIds)
               .NotNull().WithMessage("Roles are required")
               .NotEmpty().WithMessage("At least one role is required");

        When(x => x.CreateUserDto.AdditionalEmails != null, () =>
        {
            RuleForEach(x => x.CreateUserDto!.AdditionalEmails!)
                   .EmailAddress().WithMessage("Invalid additional email format")
                   .MaximumLength(100);
        });

        When(x => x.CreateUserDto.AdditionalPhones != null, () =>
        {
            RuleForEach(x => x.CreateUserDto!.AdditionalPhones!)
                   .MaximumLength(30);
        });

        RuleFor(x => x.CreateUserDto.Fax).MaximumLength(100);
        RuleFor(x => x.CreateUserDto.Skype).MaximumLength(100);
        RuleFor(x => x.CreateUserDto.SipNumber).MaximumLength(100);
        RuleFor(x => x.CreateUserDto.EmployeePrefix).MaximumLength(50);
    }
}
