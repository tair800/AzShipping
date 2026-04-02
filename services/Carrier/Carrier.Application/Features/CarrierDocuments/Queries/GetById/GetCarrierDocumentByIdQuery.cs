using Carrier.Application.DTOs.CarrierDocument;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Queries.GetById;

public sealed record GetCarrierDocumentByIdQuery(Guid Id) : IRequest<CarrierDocumentDto?>;
