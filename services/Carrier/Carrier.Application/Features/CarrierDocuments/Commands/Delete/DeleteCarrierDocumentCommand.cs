using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Commands.Delete;

public record DeleteCarrierDocumentCommand(Guid Id) : IRequest<bool>;
