using Carrier.Application.DTOs.CarrierDocument;
using Carrier.Application.Features.CarrierDocuments;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Queries.GetById;

public sealed class GetCarrierDocumentByIdQueryHandler(ICarrierDocumentRepository repository)
    : IRequestHandler<GetCarrierDocumentByIdQuery, CarrierDocumentDto?>
{
    public async Task<CarrierDocumentDto?> Handle(GetCarrierDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        return entity == null ? null : CarrierDocumentMapper.MapToDto(entity);
    }
}
