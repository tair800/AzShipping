using FluentValidation;

namespace Identity.Application.Features.Roles.Commands.Update;

public sealed class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.UpdateRoleDto).NotNull().WithMessage("Role data is required");

        RuleFor(x => x.UpdateRoleDto.Id).GreaterThan(0).WithMessage("Id must be greater than zero.");

        RuleFor(x => x.UpdateRoleDto.Name)
               .Length(2, 50).WithMessage("Name must be between 2 and 50 characters");
    }
}