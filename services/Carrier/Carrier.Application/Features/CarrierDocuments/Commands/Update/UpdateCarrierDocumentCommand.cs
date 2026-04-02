using Carrier.Application.DTOs.CarrierDocument;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Commands.Update;

public record UpdateCarrierDocumentCommand(Guid Id, UpdateCarrierDocumentDto Dto) : IRequest<CarrierDocumentDto?>;
