using MediatR;
using Settings.Application.DTOs.QuoteSource;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;

namespace Settings.Application.Features.QuoteSources.Commands.Create;

public sealed class CreateQuoteSourceCommandHandler(IQuoteSourceRepository repository) : IRequestHandler<CreateQuoteSourceCommand, QuoteSourceDto>
{
    public async Task<QuoteSourceDto> Handle(CreateQuoteSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = new QuoteSource
        {
            Id = Guid.NewGuid(),
            Name = request.Dto.Name,
            DisplayOrder = request.Dto.DisplayOrder,
            IsActive = request.Dto.IsActive,
            CreatedAt = DateTime.UtcNow
        };
        await repository.AddAsync(entity, cancellationToken);
        return new QuoteSourceDto(entity.Id, entity.Name, entity.DisplayOrder, entity.IsActive, entity.CreatedAt, entity.UpdatedAt);
    }
}
