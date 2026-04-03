using Identity.Application.Interfaces;
using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Auth.Commands.ResetPassword;

public sealed record ResetPasswordCommand(string Token, string NewPassword) : IRequest<Unit>, ITransactionalRequest;