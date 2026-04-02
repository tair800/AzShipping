using MediatR;
using Request.Application.DTOs.SaleStatus;
using Request.Application.Features.SaleStatuses;
using Request.Domain.AggregatesModel.SaleStatusAggregate;

namespace Request.Application.Features.SaleStatuses.Commands.Create;

public sealed class CreateSaleStatusCommandHandler(ISaleStatusRepository repository) : IRequestHandler<CreateSaleStatusCommand, SaleStatusDto>
{
    public async Task<SaleStatusDto> Handle(CreateSaleStatusCommand request, CancellationToken cancellationToken)
    {
        var entity = new SaleStatus
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            SortOrder = request.Dto.SortOrder,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return SaleStatusMapper.MapToDto(entity);
    }
}
