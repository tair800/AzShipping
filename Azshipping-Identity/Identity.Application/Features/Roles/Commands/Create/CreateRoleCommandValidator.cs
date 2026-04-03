using FluentValidation;

namespace Identity.Application.Features.Roles.Commands.Create;

public sealed class UpdateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.CreateRoleDto).NotNull().WithMessage("Role data is required");

        RuleFor(x => x.CreateRoleDto.Name)
               .NotNull().WithMessage("Name is required")
               .Length(2, 50).WithMessage("Name must be between 2 and 50 characters");

        RuleFor(x => x.CreateRoleDto.PermissionIds)
               .NotNull().WithMessage("Permissions are required")
               .NotEmpty().WithMessage("At least one permission is required");
    }
}