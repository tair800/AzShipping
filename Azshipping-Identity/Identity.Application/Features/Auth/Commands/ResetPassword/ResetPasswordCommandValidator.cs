using FluentValidation;

namespace Identity.Application.Features.Auth.Commands.ResetPassword;

public sealed class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Token)
               .NotEmpty().WithMessage("Confirmation token is required.");

        RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage("Password is required")
               .MinimumLength(8).WithMessage("Password must be at least 8 characters")
               .MaximumLength(100).WithMessage("Password must not exceed 100 characters")
               .Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\w\\s]).+$")
               .WithMessage("Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character (no spaces)");
    }
}