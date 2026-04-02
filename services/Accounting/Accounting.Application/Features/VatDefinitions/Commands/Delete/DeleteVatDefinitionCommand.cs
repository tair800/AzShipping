using MediatR;

namespace Accounting.Application.Features.VatDefinitions.Commands.Delete;

public record DeleteVatDefinitionCommand(Guid Id) : IRequest<bool>;
