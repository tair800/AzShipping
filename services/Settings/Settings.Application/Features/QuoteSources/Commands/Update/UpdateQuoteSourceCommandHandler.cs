using MediatR;
using Settings.Application.DTOs.QuoteSource;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;

namespace Settings.Application.Features.QuoteSources.Commands.Update;

public sealed class UpdateQuoteSourceCommandHandler(IQuoteSourceRepository repository) : IRequestHandler<UpdateQuoteSourceCommand, QuoteSourceDto?>
{
    public async Task<QuoteSourceDto?> Handle(UpdateQuoteSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return null;
        entity.Name = request.Dto.Name;
        entity.DisplayOrder = request.Dto.DisplayOrder;
        entity.IsActive = request.Dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;
        await repository.UpdateAsync(entity, cancellationToken);
        return new QuoteSourceDto(entity.Id, entity.Name, entity.DisplayOrder, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
