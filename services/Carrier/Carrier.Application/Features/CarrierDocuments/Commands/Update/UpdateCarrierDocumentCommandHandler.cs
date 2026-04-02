using Carrier.Application.DTOs.CarrierDocument;
using Carrier.Application.Features.CarrierDocuments;
using Carrier.Application.Services;
using Carrier.Domain.AggregatesModel.CarrierAggregate;
using MediatR;

namespace Carrier.Application.Features.CarrierDocuments.Commands.Update;

public class UpdateCarrierDocumentCommandHandler(ICarrierDocumentRepository repository, IActionLogClient actionLogClient)
    : IRequestHandler<UpdateCarrierDocumentCommand, CarrierDocumentDto?>
{
    public async Task<CarrierDocumentDto?> Handle(UpdateCarrierDocumentCommand request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (existing == null) return null;

        var dto = request.Dto;
        existing.DocumentNumber = dto.DocumentNumber;
        existing.DocumentDate = dto.DocumentDate;
        existing.DocumentName = dto.DocumentName;
        existing.ExpirationDate = dto.ExpirationDate;
        existing.Comments = dto.Comments;
        existing.AvailableForClient = dto.AvailableForClient;
        existing.IsSent = dto.IsSent;
        if (dto.FilePath != null)
            existing.FilePath = dto.FilePath;

        await repository.UpdateAsync(existing, cancellationToken);
        var updated = await repository.GetByIdAsync(existing.Id, cancellationToken);
        await actionLogClient.LogAsync("Carrier document updated", $"carrier document: {existing.DocumentName} • id: {existing.Id}", null, null, cancellationToken);
        return CarrierDocumentMapper.MapToDto(updated!);
    }
}
