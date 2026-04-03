using FluentValidation;
using Identity.Domain.AggregatesModel.UserAggregate.Enumerations;
using System.Linq;

namespace Identity.Application.Features.Users.Commands.UpdateStatus;

public sealed class UpdateUserStatusCommandValidator : AbstractValidator<UpdateUserStatusCommand>
{
    public UpdateUserStatusCommandValidator()
    {
        RuleFor(x => x.Dto).NotNull();
        RuleFor(x => x.Dto.Id).GreaterThan(0);
        RuleFor(x => x.Dto.Status)
            .NotEmpty()
            .Must(name => UserStatus.GetAll().Any(s => string.Equals(s.Name, name, StringComparison.OrdinalIgnoreCase)))
            .WithMessage("Status must be one of: " + string.Join(", ", UserStatus.GetAll().Select(s => s.Name)));
    }
}
