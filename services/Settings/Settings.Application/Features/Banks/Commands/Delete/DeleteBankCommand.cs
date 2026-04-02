using MediatR;

namespace Settings.Application.Features.Banks.Commands.Delete;

public sealed record DeleteBankCommand(Guid Id) : IRequest<bool>;
