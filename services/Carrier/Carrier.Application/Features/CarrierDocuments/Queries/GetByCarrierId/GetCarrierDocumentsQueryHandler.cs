using Carrier.Application.DTOs.CarrierDocument;
using Carrier.Application.Features.CarrierDocuments;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Queries.GetByCarrierId;

public sealed class GetCarrierDocumentsQueryHandler(ICarrierDocumentRepository repository)
    : IRequestHandler<GetCarrierDocumentsQuery, IReadOnlyList<CarrierDocumentDto>>
{
    public async Task<IReadOnlyList<CarrierDocumentDto>> Handle(GetCarrierDocumentsQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetByCarrierIdAsync(request.CarrierId, cancellationToken);
        return list.Select(CarrierDocumentMapper.MapToDto).ToList();
    }
}
