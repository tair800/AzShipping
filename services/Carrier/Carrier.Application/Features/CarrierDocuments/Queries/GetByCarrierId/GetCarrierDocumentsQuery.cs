using Carrier.Application.DTOs.CarrierDocument;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Queries.GetByCarrierId;

public sealed record GetCarrierDocumentsQuery(Guid CarrierId) : IRequest<IReadOnlyList<CarrierDocumentDto>>;
