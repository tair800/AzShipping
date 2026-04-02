using Carrier.Application.DTOs.CarrierDocument;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Commands.Create;

public record CreateCarrierDocumentCommand(Guid CarrierId, CreateCarrierDocumentDto Dto) : IRequest<CarrierDocumentDto>;
