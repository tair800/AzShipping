using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Auth.Commands.ForgotPassword;

public sealed record ForgotPasswordCommand(string Email) : IRequest<Unit>, ITransactionalRequest;