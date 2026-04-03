using MediatR;
using MrStyx.Application.Interfaces;

namespace Identity.Application.Features.Auth.Commands.ConfirmEmail;

public sealed record ConfirmEmailCommand(string Token) : IRequest, ITransactionalRequest;