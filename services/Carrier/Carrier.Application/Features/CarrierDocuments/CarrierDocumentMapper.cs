using Carrier.Application.DTOs.CarrierDocument;
using Carrier.Domain.AggregatesModel.CarrierAggregate;

namespace Carrier.Application.Features.CarrierDocuments;

public static class CarrierDocumentMapper
{
    public static CarrierDocumentDto MapToDto(CarrierDocument? entity)
    {
        if (entity == null) return default!;
        return new CarrierDocumentDto
        {
            Id = entity.Id,
            CarrierId = entity.CarrierId,
            DocumentNumber = entity.DocumentNumber,
            DocumentDate = entity.DocumentDate,
            DocumentName = entity.DocumentName,
            CreatedAt = entity.CreatedAt,
            ExpirationDate = entity.ExpirationDate,
            Comments = entity.Comments,
            AvailableForClient = entity.AvailableForClient,
            IsSent = entity.IsSent,
            FilePath = entity.FilePath
        };
    }
}
