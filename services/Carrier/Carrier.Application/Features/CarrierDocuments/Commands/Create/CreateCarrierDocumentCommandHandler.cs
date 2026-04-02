using Carrier.Application.DTOs.CarrierDocument;
using Carrier.Application.Features.CarrierDocuments;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Commands.Create;

public class CreateCarrierDocumentCommandHandler(ICarrierDocumentRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<CreateCarrierDocumentCommand, CarrierDocumentDto>
{
    public async Task<CarrierDocumentDto> Handle(CreateCarrierDocumentCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var entity = new CarrierDocument
        {
            Id = Guid.NewGuid(),
            CarrierId = request.CarrierId,
            DocumentNumber = dto.DocumentNumber,
            DocumentDate = dto.DocumentDate,
            DocumentName = dto.DocumentName,
            CreatedAt = DateTime.UtcNow,
            ExpirationDate = dto.ExpirationDate,
            Comments = dto.Comments,
            AvailableForClient = dto.AvailableForClient,
            IsSent = dto.IsSent
        };
        await repository.AddAsync(entity, cancellationToken);
        var created = await repository.GetByIdAsync(entity.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier document created", $"carrier document: {entity.DocumentName} • id: {entity.Id}", null, null, cancellationToken);
        return CarrierDocumentMapper.MapToDto(created!);
    }
}
