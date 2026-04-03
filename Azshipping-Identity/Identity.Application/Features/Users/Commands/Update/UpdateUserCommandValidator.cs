using FluentValidation;
using Identity.Domain.AggregatesModel.UserAggregate.ValueObjects;

namespace Identity.Application.Features.Users.Commands.Update;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {

        RuleFor(x => x.UpdateUserDto).NotNull().WithMessage("User data is required");

        RuleFor(x => x.UpdateUserDto.Id)
               .GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.UpdateUserDto.Username)
               .Length(3, 50).WithMessage("Username must be between 3 and 50 characters");

        RuleFor(x => x.UpdateUserDto.Name)
               .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.UpdateUserDto.Surname)
               .MaximumLength(50).WithMessage("Surname must not exceed 50 characters");

        RuleFor(x => x.UpdateUserDto.Phone)
               .Matches(PhoneNumber.Regex).When(x => !string.IsNullOrWhiteSpace(x.UpdateUserDto.Phone))
               .WithMessage("Invalid phone format")
               .MaximumLength(20).WithMessage("Phone must not exceed 20 characters");

        When(x => x.UpdateUserDto.AdditionalEmails != null, () =>
        {
            RuleForEach(x => x.UpdateUserDto!.AdditionalEmails!)
                   .EmailAddress().WithMessage("Invalid additional email format")
                   .MaximumLength(100);
        });

        When(x => x.UpdateUserDto.AdditionalPhones != null, () =>
        {
            RuleForEach(x => x.UpdateUserDto!.AdditionalPhones!)
                   .MaximumLength(30);
        });

        RuleFor(x => x.UpdateUserDto.Fax).MaximumLength(100);
        RuleFor(x => x.UpdateUserDto.Skype).MaximumLength(100);
        RuleFor(x => x.UpdateUserDto.SipNumber).MaximumLength(100);
        RuleFor(x => x.UpdateUserDto.EmployeePrefix).MaximumLength(50);
    }
}