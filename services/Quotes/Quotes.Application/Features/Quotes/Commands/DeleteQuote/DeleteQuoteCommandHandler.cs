using MediatR;
using Quotes.Application.Services;
using Quotes.Domain.AggregatesModel.QuoteAggregate;

namespace Quotes.Application.Features.Quotes.Commands.DeleteQuote;

public sealed class DeleteQuoteCommandHandler(IQuoteRepository repository, IActionLogClient actionLogClient) : IRequestHandler<DeleteQuoteCommand, bool>
{
    public async Task<bool> Handle(DeleteQuoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity == null) return false;
        var quoteNumber = entity.QuoteNumber;
        await repository.DeleteAsync(request.Id, cancellationToken);
        await actionLogClient.LogAsync("Quote deleted", $"quote: {quoteNumber} • id: {request.Id}", null, null, cancellationToken);
        return true;
    }
}
