using Clients.Application.DTOs.Currency;
using Clients.Domain.AggregatesModel.CurrencyAggregate;
using MediatR;

namespace Clients.Application.Features.Currencies.Queries.GetAll;

public sealed class GetAllCurrenciesQueryHandler(ICurrencyRepository repository) : IRequestHandler<GetAllCurrenciesQuery, IReadOnlyList<CurrencyDto>>
{
    public async Task<IReadOnlyList<CurrencyDto>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var list = await repository.GetAllAsync(cancellationToken);
        return list.Select(c => new CurrencyDto { Id = c.Id, Code = c.Code, Name = c.Name }).ToList();
    }
}
