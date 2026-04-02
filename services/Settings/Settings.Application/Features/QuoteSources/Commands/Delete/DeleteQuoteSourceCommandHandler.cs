using MediatR;
using Settings.Domain.AggregatesModel.QuoteSourceAggregate;

namespace Settings.Application.Features.QuoteSources.Commands.Delete;

public sealed class DeleteQuoteSourceCommandHandler(IQuoteSourceRepository repository) : IRequestHandler<DeleteQuoteSourceCommand, bool>
{
    public async Task<bool> Handle(DeleteQuoteSourceCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        await repository.DeleteAsync(request.Id, cancellationToken);
        return true;
    }
}
